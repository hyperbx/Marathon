using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Blocks;
using Marathon.Formats.Script.Lua.Decompiler.Targets;
using Marathon.Formats.Script.Lua.Decompiler.Branches;
using Marathon.Formats.Script.Lua.Decompiler.Statements;
using Marathon.Formats.Script.Lua.Decompiler.Operations;
using Marathon.Formats.Script.Lua.Decompiler.Expressions;

namespace Marathon.Formats.Script.Lua.Decompiler
{
    public class Decompiler
    {
        private readonly int _registers, _length;
        private readonly Upvalues _upvalues;
        private readonly LFunction[] _functions;
        private readonly int _params, _vararg;
        private readonly Op.Opcode _tforTarget, _forTarget;

        private static Stack<Branch> _backup;

        private Registers _r;
        private Block _outer;
        private List<Block> _blocks;

        protected Function _f;
        protected LFunction _function;

        public readonly Code Code;
        public readonly Declaration[] DeclarationList;

        /// <summary>
        /// When lines are processed out of order, they are noted here so they can be skipped when encountered normally.
        /// </summary>
        bool[] _skip;

        /// <summary>
        /// Precalculated array of which lines are the targets of jump instructions that go backwards...
        /// Such targets must be at the statement/block level in the outputted code (they cannot be mid-expression).
        /// </summary>
        bool[] _reverseTarget;

        public Decompiler(LFunction function)
        {
            _f = new Function(function);
            _function = function;
            _registers = function.MaximumStackSize;
            _length = function.Code.Length;
            Code = new Code(function);

            if (function.Locals.Length >= function.NumParams)
            {
                DeclarationList = new Declaration[function.Locals.Length];

                for (int i = 0; i < DeclarationList.Length; i++)
                    DeclarationList[i] = new Declaration(function.Locals[i]);
            }
            else
            {
                DeclarationList = new Declaration[function.NumParams];

                for (int i = 0; i < DeclarationList.Length; i++)
                    DeclarationList[i] = new Declaration($"a{i + 1}", 0, _length - 1);
            }

            _upvalues = new Upvalues(function.Upvalues);
            _functions = function.Functions;
            _params = function.NumParams;
            _vararg = function.Vararg;
            _tforTarget = function.Header.Version.GetTForTarget();
            _forTarget = function.Header.Version.GetForTarget();
        }

        public void Decompile()
        {
            _r = new Registers(_registers, _length, DeclarationList, _f);

            FindReverseTargets();
            HandleBranches(true);

            _outer = HandleBranches(false);

            ProcessSequence(1, _length);
        }

        public void Write()
            => Write(new Output());

        public void Write(IOutputProvider @out)
            => Write(new Output(@out));

        public void Write(Output @out)
        {
            HandleInitialDeclarations(@out);

            _outer.Write(@out);
        }

        private void HandleInitialDeclarations(Output @out)
        {
            List<Declaration> initdecls = new(DeclarationList.Length);

            for (int i = _params + (_vararg & 1); i < DeclarationList.Length; i++)
            {
                if (DeclarationList[i].Begin == 0)
                    initdecls.Add(DeclarationList[i]);
            }

            if (initdecls.Count > 0)
            {
                @out.Write("local ");
                @out.Write(initdecls[0].Name);

                for (int i = 1; i < initdecls.Count; i++)
                {
                    @out.Write(", ");
                    @out.Write(initdecls[i].Name);
                }

                @out.WriteLine();
            }
        }

        private List<Operation> ProcessLine(int line)
        {
            LinkedList<Operation> operations = new();

            int A  = Code.A(line),
                C  = Code.C(line),
                B  = Code.B(line),
                Bx = Code.Bx(line);

            switch (Code.Op(line))
            {
                case Op.Opcode.MOVE:
                    operations.AddLast(new RegisterSet(line, A, _r.GetExpression(B, line)));
                    break;

                case Op.Opcode.LOADK:
                    operations.AddLast(new RegisterSet(line, A, _f.GetConstantExpression(Bx)));
                    break;

                case Op.Opcode.LOADBOOL:
                    operations.AddLast(new RegisterSet(line, A, new ConstantExpression(new Constant(B != 0 ? LBoolean.LTRUE : LBoolean.LFALSE), -1)));
                    break;

                case Op.Opcode.LOADNIL:
                {
                    int maximum;

                    if (_function.Header.Version.UsesOldLoadNilEncoding())
                    {
                        maximum = B;
                    }
                    else
                    {
                        maximum = A + B;
                    }

                    while (A <= maximum)
                    {
                        operations.AddLast(new RegisterSet(line, A, Expression.NIL));
                        A++;
                    }

                    break;
                }

                case Op.Opcode.GETUPVAL:
                    operations.AddLast(new RegisterSet(line, A, _upvalues.GetExpression(B)));
                    break;

                case Op.Opcode.GETTABUP:
                {
                    if (B == 0 && (C & 0x100) != 0)
                    {
                        operations.AddLast(new RegisterSet(line, A, _f.GetGlobalExpression(C & 0xFF)));
                    }
                    else
                    {
                        operations.AddLast(new RegisterSet(line, A, new TableReference(_upvalues.GetExpression(B), _r.GetConstantExpression(C, line))));
                    }

                    break;
                }

                case Op.Opcode.GETGLOBAL:
                    operations.AddLast(new RegisterSet(line, A, _f.GetGlobalExpression(Bx)));
                    break;

                case Op.Opcode.GETTABLE:
                    operations.AddLast(new RegisterSet(line, A, new TableReference(_r.GetExpression(B, line), _r.GetConstantExpression(C, line))));
                    break;

                case Op.Opcode.SETUPVAL:
                    operations.AddLast(new UpvalueSet(line, _upvalues.GetName(B), _r.GetExpression(A, line)));
                    break;

                case Op.Opcode.SETTABUP:
                {
                    if (A == 0 && (B & 0x100) != 0)
                    {
                        operations.AddLast(new GlobalSet(line, _f.GetGlobalName(B & 0xFF), _r.GetConstantExpression(C, line)));
                    }
                    else
                    {
                        operations.AddLast(new TableSet(line, _upvalues.GetExpression(A), _r.GetConstantExpression(B, line), _r.GetConstantExpression(C, line), true, line));
                    }

                    break;
                }

                case Op.Opcode.SETGLOBAL:
                    operations.AddLast(new GlobalSet(line, _f.GetGlobalName(Bx), _r.GetExpression(A, line)));
                    break;

                case Op.Opcode.SETTABLE:
                    operations.AddLast(new TableSet(line, _r.GetExpression(A, line), _r.GetConstantExpression(B, line), _r.GetConstantExpression(C, line), true, line));
                    break;

                case Op.Opcode.NEWTABLE:
                    operations.AddLast(new RegisterSet(line, A, new TableLiteral(B, C)));
                    break;

                case Op.Opcode.SELF:
                {
                    // We can later determine : syntax was used by comparing subexpressions with == operators.
                    Expression common = _r.GetExpression(B, line);

                    operations.AddLast(new RegisterSet(line, A + 1, common));
                    operations.AddLast(new RegisterSet(line, A, new TableReference(common, _r.GetConstantExpression(C, line))));

                    break;
                }

                case Op.Opcode.ADD:
                    operations.AddLast(new RegisterSet(line, A, Expression.MakeADD(_r.GetConstantExpression(B, line), _r.GetConstantExpression(C, line))));
                    break;

                case Op.Opcode.SUB:
                    operations.AddLast(new RegisterSet(line, A, Expression.MakeSUB(_r.GetConstantExpression(B, line), _r.GetConstantExpression(C, line))));
                    break;

                case Op.Opcode.MUL:
                    operations.AddLast(new RegisterSet(line, A, Expression.MakeMUL(_r.GetConstantExpression(B, line), _r.GetConstantExpression(C, line))));
                    break;

                case Op.Opcode.DIV:
                    operations.AddLast(new RegisterSet(line, A, Expression.MakeDIV(_r.GetConstantExpression(B, line), _r.GetConstantExpression(C, line))));
                    break;

                case Op.Opcode.MOD:
                    operations.AddLast(new RegisterSet(line, A, Expression.MakeMOD(_r.GetConstantExpression(B, line), _r.GetConstantExpression(C, line))));
                    break;

                case Op.Opcode.POW:
                    operations.AddLast(new RegisterSet(line, A, Expression.MakePOW(_r.GetConstantExpression(B, line), _r.GetConstantExpression(C, line))));
                    break;

                case Op.Opcode.UNM:
                    operations.AddLast(new RegisterSet(line, A, Expression.MakeUNM(_r.GetConstantExpression(B, line))));
                    break;

                case Op.Opcode.NOT:
                    operations.AddLast(new RegisterSet(line, A, Expression.MakeNOT(_r.GetConstantExpression(B, line))));
                    break;

                case Op.Opcode.LEN:
                    operations.AddLast(new RegisterSet(line, A, Expression.MakeLEN(_r.GetConstantExpression(B, line))));
                    break;

                case Op.Opcode.CONCAT:
                {
                    Expression value = _r.GetExpression(C, line);

                    // Remember that CONCAT is right associative.
                    while (C-- > B)
                        value = Expression.MakeCONCAT(_r.GetExpression(C, line), value);

                    operations.AddLast(new RegisterSet(line, A, value));

                    break;
                }

                case Op.Opcode.JMP:
                case Op.Opcode.EQ:
                case Op.Opcode.LT:
                case Op.Opcode.LE:
                case Op.Opcode.TEST:
                case Op.Opcode.TESTSET:
                case Op.Opcode.TEST50:
                    break;

                case Op.Opcode.CALL:
                {
                    bool multiple = C >= 3 || C == 0;

                    if (B == 0)
                        B = _registers - A;

                    if (C == 0)
                        C = _registers - A + 1;

                    Expression function = _r.GetExpression(A, line);
                    Expression[] arguments = new Expression[B - 1];

                    for (int register = A + 1; register <= A + B - 1; register++)
                        arguments[register - A - 1] = _r.GetExpression(register, line);

                    FunctionCall value = new(function, arguments, multiple);

                    if (C == 1)
                    {
                        operations.AddLast(new CallOperation(line, value));
                    }
                    else
                    {
                        if (C == 2 && !multiple)
                        {
                            operations.AddLast(new RegisterSet(line, A, value));
                        }
                        else
                        {
                            for (int register = A; register <= A + C - 2; register++)
                                operations.AddLast(new RegisterSet(line, register, value));
                        }
                    }

                    break;
                }

                case Op.Opcode.TAILCALL:
                {
                    if (B == 0) B = _registers - A;

                    Expression function = _r.GetExpression(A, line);
                    Expression[] arguments = new Expression[B - 1];

                    for (int register = A + 1; register <= A + B - 1; register++)
                        arguments[register - A - 1] = _r.GetExpression(register, line);

                    FunctionCall value = new(function, arguments, true);

                    operations.AddLast(new ReturnOperation(line, value));

                    _skip[line + 1] = true;

                    break;
                }

                case Op.Opcode.RETURN:
                {
                    if (B == 0) B = _registers - A + 1;

                    Expression[] values = new Expression[B - 1];

                    for (int register = A; register <= A + B - 2; register++)
                        values[register - A] = _r.GetExpression(register, line);

                    operations.AddLast(new ReturnOperation(line, values));

                    break;
                }

                case Op.Opcode.FORLOOP:
                case Op.Opcode.FORPREP:
                case Op.Opcode.TFORPREP:
                case Op.Opcode.TFORCALL:
                case Op.Opcode.TFORLOOP:
                    break;

                case Op.Opcode.SETLIST50:
                case Op.Opcode.SETLISTO:
                {
                    Expression table = _r.GetValue(A, line);
                    int n = Bx % 32;

                    for (int i = 1; i <= n + 1; i++)
                        operations.AddLast(new TableSet(line, table, new ConstantExpression(new Constant(Bx - n + i), -1), _r.GetExpression(A + i, line), false, _r.GetUpdated(A + i, line)));

                    break;
                }

                case Op.Opcode.SETLIST:
                {
                    if (C == 0)
                    {
                        C = Code.Codepoint(line + 1);
                        _skip[line + 1] = true;
                    }

                    if (B == 0)
                        B = _registers - A - 1;

                    Expression table = _r.GetValue(A, line);

                    for (int i = 1; i <= B; i++)
                        operations.AddLast(new TableSet(line, table, new ConstantExpression(new Constant((C - 1) * 50 + i), -1), _r.GetExpression(A + i, line), false, _r.GetUpdated(A + i, line)));

                    break;
                }

                case Op.Opcode.CLOSE:
                    break;

                case Op.Opcode.CLOSURE:
                {
                    LFunction f = _functions[Bx];

                    operations.AddLast(new RegisterSet(line, A, new ClosureExpression(f, line + 1)));

                    if (_function.Header.Version.UsesInlineUpvalueDeclarations())
                    {
                        // Skip upvalue declarations.
                        for (int i = 0; i < f.NumUpvalues; i++)
                            _skip[line + 1 + i] = true;
                    }

                    break;
                }

                case Op.Opcode.VARARG:
                {
                    bool multiple = B != 2;

                    if (B == 1)
                        throw new Exception();

                    if (B == 0)
                        B = _registers - A + 1;

                    Expression value = new Vararg(B - 1, multiple);

                    for (int register = A; register <= A + B - 2; register++)
                        operations.AddLast(new RegisterSet(line, register, value));

                    break;
                }

                default:
                    throw new Exception($"Illegal instruction: {Code.Op(line)}");
            }

            return new List<Operation>(operations);
        }

        private void FindReverseTargets()
        {
            _reverseTarget = new bool[_length + 1];

            Array.Fill(_reverseTarget, false);

            for (int line = 1; line <= _length; line++)
            {
                if (Code.Op(line) == Op.Opcode.JMP && Code.sBx(line) < 0)
                    _reverseTarget[line + 1 + Code.sBx(line)] = true;
            }
        }

        private Assignment ProcessOperation(Operation operation, int line, int nextLine, Block block)
        {
            Assignment assign = null;
            Statement statement = operation.Process(_r, block);

            bool wasMultiple = false;

            if (statement != null)
            {
                if (statement is Assignment assignment)
                {
                    assign = assignment;

                    if (!assign.GetFirstValue().IsMultiple())
                    {
                        block.AddStatement(statement);
                    }
                    else
                    {
                        wasMultiple = true;
                    }
                }
                else
                {
                    block.AddStatement(statement);
                }

                if (assign != null)
                {
                    while (nextLine < block.End && IsMoveIntoTarget(nextLine))
                    {
                        Target target = GetMoveIntoTargetTarget(nextLine, line + 1);
                        Expression value = GetMoveIntoTargetValue(nextLine, line + 1);

                        assign.AddFirst(target, value);

                        _skip[nextLine] = true;

                        nextLine++;
                    }

                    if (wasMultiple && !assign.GetFirstValue().IsMultiple())
                        block.AddStatement(statement);
                }
            }

            return assign;
        }

        private void ProcessSequence(int begin, int end)
        {
            int blockIndex = 1;

            Stack<Block> blockStack = new();
            blockStack.Push(_blocks[0]);

            _skip = new bool[end + 1];

            for (int line = begin; line <= end; line++)
            {
                Operation blockHandler = null;

                while (blockStack.Peek().End <= line)
                {
                    Block seqBlock = blockStack.Pop();
                    blockHandler = seqBlock.Process(this);

                    if (blockHandler != null)
                        break;
                }

                if (blockHandler == null)
                {
                    while (blockIndex < _blocks.Count && _blocks[blockIndex].Begin <= line)
                        blockStack.Push(_blocks[blockIndex++]);
                }

                Block block = blockStack.Peek();

                _r.StartLine(line);

                if (_skip[line])
                {
                    List<Declaration> skipNewLocals = _r.GetNewLocals(line);

                    if (skipNewLocals.Count != 0)
                    {
                        Assignment skipAssign = new();
                        skipAssign.Declare(skipNewLocals[0].Begin);

                        foreach (Declaration decl in skipNewLocals)
                            skipAssign.AddLast(new VariableTarget(decl), _r.GetValue(decl.Register, line));

                        blockStack.Peek().AddStatement(skipAssign);
                    }

                    continue;
                }

                List<Operation> operations = ProcessLine(line);
                List<Declaration> newLocals = _r.GetNewLocals(blockHandler == null ? line : line - 1);
                Assignment assign = null;

                if (blockHandler == null)
                {
                    if (Code.Op(line) == Op.Opcode.LOADNIL)
                    {
                        assign = new Assignment();
                        int count = 0;

                        foreach (Operation operation in operations)
                        {
                            RegisterSet set = (RegisterSet)operation;
                            operation.Process(_r, block);

                            if (_r.IsAssignable(set.Register, set.Line))
                            {
                                assign.AddLast(_r.GetTarget(set.Register, set.Line), set.Value);
                                count++;
                            }
                        }

                        if (count > 0)
                            block.AddStatement(assign);
                    }
                    else if (Code.Op(line) == Op.Opcode.TFORPREP)
                    {
                        // Lua 5.0 has no assignments for FORPREP.
                        newLocals.Clear();
                    }
                    else
                    {
                        foreach (Operation operation in operations)
                        {
                            Assignment temp = ProcessOperation(operation, line, line + 1, block);

                            if (temp != null)
                                assign = temp;
                        }

                        if (assign != null && assign.GetFirstValue().IsMultiple())
                            block.AddStatement(assign);
                    }
                }
                else
                {
                    assign = ProcessOperation(blockHandler, line, line, block);
                }

                if (assign != null)
                {
                    if (newLocals.Count != 0)
                    {
                        assign.Declare(newLocals[0].Begin);

                        foreach (Declaration decl in newLocals)
                            assign.AddLast(new VariableTarget(decl), _r.GetValue(decl.Register, line + 1));
                    }
                }

                if (blockHandler == null)
                {
                    if (assign == null && newLocals.Count != 0 && Code.Op(line) != Op.Opcode.FORPREP)
                    {
                        if (Code.Op(line) != Op.Opcode.JMP || Code.Op(line + 1 + Code.sBx(line)) != _tforTarget)
                        {
                            assign = new Assignment();
                            assign.Declare(newLocals[0].Begin);

                            foreach (Declaration decl in newLocals)
                                assign.AddLast(new VariableTarget(decl), _r.GetValue(decl.Register, line));

                            blockStack.Peek().AddStatement(assign);
                        }
                    }
                }

                if (blockHandler != null)
                {
                    line--;

                    continue;
                }
            }
        }

        private bool IsMoveIntoTarget(int line)
        {
            switch (Code.Op(line))
            {
                case Op.Opcode.MOVE:
                    return _r.IsAssignable(Code.A(line), line) && !_r.IsLocal(Code.B(line), line);

                case Op.Opcode.SETUPVAL:
                case Op.Opcode.SETGLOBAL:
                    return !_r.IsLocal(Code.A(line), line);

                case Op.Opcode.SETTABLE:
                {
                    int C = Code.C(line);

                    if (_f.IsConstant(C))
                    {
                        return false;
                    }
                    else
                    {
                        return !_r.IsLocal(C, line);
                    }
                }

                default:
                    return false;
            }
        }

        private Target GetMoveIntoTargetTarget(int line, int previous)
        {
            return Code.Op(line) switch
            {
                Op.Opcode.MOVE => _r.GetTarget(Code.A(line), line),
                Op.Opcode.SETUPVAL => new UpvalueTarget(_upvalues.GetName(Code.B(line))),
                Op.Opcode.SETGLOBAL => new GlobalTarget(_f.GetGlobalName(Code.Bx(line))),
                Op.Opcode.SETTABLE => new TableTarget(_r.GetExpression(Code.A(line), previous), _r.GetConstantExpression(Code.B(line), previous)),
                _ => throw new Exception()
            };
        }

        private Expression GetMoveIntoTargetValue(int line, int previous)
        {
            int A = Code.A(line),
                B = Code.B(line),
                C = Code.C(line);

            switch (Code.Op(line))
            {
                case Op.Opcode.MOVE:
                    return _r.GetValue(B, previous);

                case Op.Opcode.SETUPVAL:
                case Op.Opcode.SETGLOBAL:
                    return _r.GetExpression(A, previous);

                case Op.Opcode.SETTABLE:
                {
                    if (_f.IsConstant(C))
                    {
                        throw new Exception();
                    }
                    else
                    {
                        return _r.GetExpression(C, previous);
                    }
                }

                default:
                    throw new Exception();
            }
        }

        private OuterBlock HandleBranches(bool first)
        {
            List<Block> oldBlocks = _blocks = new();

            OuterBlock outer = new(_function, _length);
            _blocks.Add(outer);

            bool[] isBreak = new bool[_length + 1],
                   loopRemoved = new bool[_length + 1];

            if (!first)
            {
                foreach (Block block in oldBlocks)
                {
                    if (block is AlwaysLoop)
                        _blocks.Add(block);

                    if (block is Break)
                    {
                        _blocks.Add(block);

                        isBreak[block.Begin] = true;
                    }
                }

                LinkedList<Block> delete = new();

                foreach (Block loop in _blocks)
                {
                    if (loop is AlwaysLoop)
                    {
                        foreach (Block child in _blocks)
                        {
                            if (loop != child)
                            {
                                if (loop.Begin == child.Begin)
                                {
                                    if (loop.End < child.End)
                                    {
                                        delete.AddLast(loop);

                                        loopRemoved[loop.End - 1] = true;
                                    }
                                    else
                                    {
                                        delete.AddLast(child);

                                        loopRemoved[child.End - 1] = true;
                                    }
                                }
                            }
                        }
                    }
                }

                foreach (Block block in delete)
                    _blocks.Remove(block);

                _skip = new bool[_length + 1];

                Stack<Branch> stack = new();

                bool testset = false;
                int testsetend = -1;

                for (int line = 1; line <= _length; line++)
                {
                    if (!_skip[line])
                    {
                        bool reduce;

                        void PushCommonNodeToStack(Branch node)
                        {
                            stack.Push(node);

                            _skip[line + 1] = true;

                            if (Code.Op(node.End) == Op.Opcode.LOADBOOL)
                            {
                                if (Code.C(node.End) != 0)
                                {
                                    node.IsCompareSet = true;
                                    node.SetTarget = Code.A(node.End);
                                }
                                else if (Code.Op(node.End - 1) == Op.Opcode.LOADBOOL)
                                {
                                    if (Code.C(node.End - 1) != 0)
                                    {
                                        node.IsCompareSet = true;
                                        node.SetTarget = Code.A(node.End);
                                    }
                                }
                            }
                        }

                        switch (Code.Op(line))
                        {
                            case Op.Opcode.EQ:
                                PushCommonNodeToStack(new EQNode(Code.B(line), Code.C(line), Code.A(line) != 0, line, line + 2, line + 2 + Code.sBx(line + 1)));
                                continue;

                            case Op.Opcode.LT:
                                PushCommonNodeToStack(new LTNode(Code.B(line), Code.C(line), Code.A(line) != 0, line, line + 2, line + 2 + Code.sBx(line + 1)));
                                continue;

                            case Op.Opcode.LE:
                                PushCommonNodeToStack(new LENode(Code.B(line), Code.C(line), Code.A(line) != 0, line, line + 2, line + 2 + Code.sBx(line + 1)));
                                continue;

                            case Op.Opcode.TEST:
                            {
                                stack.Push(new TestNode(Code.A(line), Code.C(line) != 0, line, line + 2, line + 2 + Code.sBx(line + 1)));

                                _skip[line + 1] = true;

                                continue;
                            }

                            case Op.Opcode.TESTSET:
                            {
                                testset = true;
                                testsetend = line + 2 + Code.sBx(line + 1);

                                stack.Push(new TestSetNode(Code.A(line), Code.B(line), Code.C(line) != 0, line, line + 2, line + 2 + Code.sBx(line + 1)));

                                _skip[line + 1] = true;

                                continue;
                            }

                            case Op.Opcode.TEST50:
                            {
                                if (Code.A(line) == Code.B(line))
                                {
                                    stack.Push(new TestNode(Code.A(line), Code.C(line) != 0, line, line + 2, line + 2 + Code.sBx(line + 1)));
                                }
                                else
                                {
                                    testset = true;
                                    testsetend = line + 2 + Code.sBx(line + 1);

                                    stack.Push(new TestSetNode(Code.A(line), Code.B(line), Code.C(line) != 0, line, line + 2, line + 2 + Code.sBx(line + 1)));
                                }

                                _skip[line + 1] = true;

                                continue;
                            }

                            case Op.Opcode.JMP:
                            {
                                reduce = true;

                                int tline = line + 1 + Code.sBx(line);

                                if (tline >= 2 && Code.Op(tline - 1) == Op.Opcode.LOADBOOL && Code.C(tline - 1) != 0)
                                {
                                    stack.Push(new TrueNode(Code.A(tline - 1), false, line, line + 1, tline));

                                    _skip[line + 1] = true;
                                }
                                else if (Code.Op(tline) == _tforTarget && !_skip[tline])
                                {
                                    int A = Code.A(tline),
                                        C = Code.C(tline);

                                    if (C == 0) throw new Exception();

                                    _r.SetInternalLoopVariable(A, tline, line + 1);
                                    _r.SetInternalLoopVariable(A + 1, tline, line + 1);
                                    _r.SetInternalLoopVariable(A + 2, tline, line + 1);

                                    for (int index = 1; index <= C; index++)
                                        _r.SetInternalLoopVariable(A + 2 + index, line, tline + 2);

                                    _skip[tline] = true;
                                    _skip[tline + 1] = true;

                                    _blocks.Add(new TForBlock(_function, line + 1, tline + 2, A, C, _r));
                                }
                                else if (Code.Op(tline) == _forTarget && !_skip[tline])
                                {
                                    int A = Code.A(tline);

                                    _r.SetInternalLoopVariable(A, tline, line + 1);
                                    _r.SetInternalLoopVariable(A + 1, tline, line + 1);
                                    _r.SetInternalLoopVariable(A + 2, tline, line + 1);

                                    _skip[tline] = true;
                                    _skip[tline + 1] = true;

                                    _blocks.Add(new ForBlock(_function, line + 1, tline + 1, A, _r));
                                }
                                else if (Code.sBx(line) == 2 && Code.Op(line + 1) == Op.Opcode.LOADBOOL && Code.C(line + 1) != 0)
                                {
                                    // This is the tail of a boolean set with a compare node and assign node.
                                    _blocks.Add(new BooleanIndicator(_function, line));
                                }
                                else if (Code.Op(tline) == Op.Opcode.JMP && Code.sBx(tline) + tline == line)
                                {
                                    if (first)
                                        _blocks.Add(new AlwaysLoop(_function, line, tline + 1));

                                    _skip[tline] = true;
                                }
                                else
                                {
                                    if (first || loopRemoved[line] || _reverseTarget[line + 1])
                                    {
                                        if (tline > line)
                                        {
                                            isBreak[line] = true;

                                            _blocks.Add(new Break(_function, line, tline));
                                        }
                                        else
                                        {
                                            Block enclosing = EnclosingBreakableBlock(line);

                                            if (enclosing != null && enclosing.Breakable() && Code.Op(enclosing.End) == Op.Opcode.JMP && Code.sBx(enclosing.End) + enclosing.End + 1 == tline)
                                            {
                                                isBreak[line] = true;

                                                _blocks.Add(new Break(_function, line, enclosing.End));
                                            }
                                            else
                                            {
                                                _blocks.Add(new AlwaysLoop(_function, tline, line + 1));
                                            }
                                        }
                                    }
                                }

                                break;
                            }

                            case Op.Opcode.FORPREP:
                            {
                                reduce = true;

                                _blocks.Add(new ForBlock(_function, line + 1, line + 2 + Code.sBx(line), Code.A(line), _r));

                                _skip[line + 1 + Code.sBx(line)] = true;

                                _r.SetInternalLoopVariable(Code.A(line), line, line + 2 + Code.sBx(line));
                                _r.SetInternalLoopVariable(Code.A(line) + 1, line, line + 2 + Code.sBx(line));
                                _r.SetInternalLoopVariable(Code.A(line) + 2, line, line + 2 + Code.sBx(line));
                                _r.SetInternalLoopVariable(Code.A(line) + 3, line, line + 2 + Code.sBx(line));

                                break;
                            }

                            case Op.Opcode.FORLOOP:
                            {
                                // Should be skipped by preceding FORPREP.
                                throw new Exception();
                            }

                            case Op.Opcode.TFORPREP:
                            {
                                reduce = true;

                                int tline = line + 1 + Code.sBx(line),
                                    A = Code.A(tline),
                                    C = Code.C(tline);

                                _r.SetInternalLoopVariable(A, tline, line + 1);
                                _r.SetInternalLoopVariable(A + 1, tline, line + 1);
                                _r.SetInternalLoopVariable(A + 2, tline, line + 1);

                                for (int index = 1; index <= C; index++)
                                    _r.SetInternalLoopVariable(A + 2 + index, line, tline + 2);

                                _skip[tline] = true;
                                _skip[tline + 1] = true;

                                _blocks.Add(new TForBlock(_function, line + 1, tline + 2, A, C, _r));

                                break;
                            }

                            default:
                                reduce = IsStatement(line);
                                break;
                        }

                        if ((line + 1) <= _length && _reverseTarget[line + 1])
                            reduce = true;

                        if (testset && testsetend == line + 1)
                            reduce = true;

                        if (stack.Count == 0)
                            reduce = false;

                        if (reduce)
                        {
                            reduce = false;

                            Stack<Branch> conditions = new();
                            Stack<Stack<Branch>> backups = new();

                            do
                            {
                                bool isAssignNode = stack.Peek() is TestSetNode;
                                int assignEnd = stack.Peek().End;
                                bool compareCorrect = false;

                                if (stack.Peek() is TrueNode)
                                {
                                    isAssignNode = true;
                                    compareCorrect = true;

                                    if (Code.C(assignEnd) != 0)
                                    {
                                        assignEnd += 2;
                                    }
                                    else
                                    {
                                        assignEnd += 1;
                                    }
                                }
                                else if (stack.Peek().IsCompareSet)
                                {
                                    if (Code.Op(stack.Peek().Begin) != Op.Opcode.LOADBOOL || Code.C(stack.Peek().Begin) == 0)
                                    {
                                        isAssignNode = true;

                                        if (Code.C(assignEnd) != 0)
                                        {
                                            assignEnd += 2;
                                        }
                                        else
                                        {
                                            assignEnd += 1;
                                        }

                                        compareCorrect = true;
                                    }
                                }
                                else if (assignEnd - 3 >= 1 && Code.Op(assignEnd - 2) == Op.Opcode.LOADBOOL && Code.C(assignEnd - 2) != 0 && Code.Op(assignEnd - 3) == Op.Opcode.JMP && Code.sBx(assignEnd - 3) == 2)
                                {
                                    if (stack.Peek() is TestNode)
                                    {
                                        TestNode node = (TestNode)stack.Peek();

                                        if (node.Test == Code.A(assignEnd - 2))
                                            isAssignNode = true;
                                    }
                                }
                                else if (assignEnd - 2 >= 1 && Code.Op(assignEnd - 1) == Op.Opcode.LOADBOOL && Code.C(assignEnd - 1) != 0 && Code.Op(assignEnd - 2) == Op.Opcode.JMP && Code.sBx(assignEnd - 2) == 2)
                                {
                                    if (stack.Peek() is TestNode)
                                    {
                                        isAssignNode = true;
                                        assignEnd += 1;
                                    }
                                }
                                else if (assignEnd - 1 >= 1 && Code.Op(assignEnd) == Op.Opcode.LOADBOOL && Code.C(assignEnd) != 0 && Code.Op(assignEnd - 1) == Op.Opcode.JMP && Code.sBx(assignEnd - 1) == 2)
                                {
                                    if (stack.Peek() is TestNode)
                                    {
                                        isAssignNode = true;
                                        assignEnd += 2;
                                    }
                                }
                                else if (assignEnd - 1 >= 1 && _r.IsLocal(GetAssignment(assignEnd - 1), assignEnd - 1) && assignEnd > stack.Peek().Line)
                                {
                                    Declaration decl = _r.GetDeclaration(GetAssignment(assignEnd - 1), assignEnd - 1);

                                    if (decl.Begin == assignEnd - 1 && decl.End > assignEnd - 1)
                                        isAssignNode = true;
                                }

                                if (!compareCorrect && assignEnd - 1 == stack.Peek().Begin && Code.Op(stack.Peek().Begin) == Op.Opcode.LOADBOOL && Code.C(stack.Peek().Begin) != 0)
                                {
                                    _backup = null;

                                    int begin = stack.Peek().Begin;

                                    assignEnd = begin + 2;

                                    int target = Code.A(begin);

                                    conditions.Push(PopCompareSetCondition(stack, assignEnd));
                                    conditions.Peek().SetTarget = target;
                                    conditions.Peek().End = assignEnd;
                                    conditions.Peek().Begin = begin;
                                }
                                else if (isAssignNode)
                                {
                                    _backup = null;

                                    int target = stack.Peek().SetTarget,
                                        begin = stack.Peek().Begin;

                                    conditions.Push(PopSetCondition(stack, assignEnd));
                                    conditions.Peek().SetTarget = target;
                                    conditions.Peek().End = assignEnd;
                                    conditions.Peek().Begin = begin;
                                }
                                else
                                {
                                    _backup = new Stack<Branch>();

                                    conditions.Push(PopCondition(stack));

                                    _backup.Reverse();
                                }

                                backups.Push(_backup);
                            }
                            while (stack.Count != 0);

                            do
                            {
                                Branch cond = conditions.Pop();
                                Stack<Branch> backup = backups.Pop();
                                int _breakTarget = BreakTarget(cond.Begin);
                                bool breakable = _breakTarget >= 1;

                                if (breakable && Code.Op(_breakTarget) == Op.Opcode.JMP && _function.Header.Version != Version.LUA50)
                                    _breakTarget += 1 + Code.sBx(_breakTarget);

                                if (breakable && _breakTarget == cond.End)
                                {
                                    Block immediateEnclosing = EnclosingBlock(cond.Begin);

                                    for (int iline = Math.Max(cond.End, immediateEnclosing.End - 1); iline >= Math.Max(cond.Begin, immediateEnclosing.Begin); iline--)
                                    {
                                        if (Code.Op(iline) == Op.Opcode.JMP && iline + 1 + Code.sBx(iline) == _breakTarget)
                                        {
                                            cond.End = iline;

                                            break;
                                        }
                                    }
                                }

                                // A branch has a tail if the instruction just before the target is JMP.
                                bool hasTail = cond.End >= 2 && Code.Op(cond.End - 1) == Op.Opcode.JMP;

                                // This is the target of the tail JMP.
                                int tail = hasTail ? cond.End + Code.sBx(cond.End - 1) : -1,
                                    originalTail = tail;

                                Block enclosing = EnclosingUnprotectedBlock(cond.Begin);

                                // Checking enclosing unprotected block to undo JMP redirects.
                                if (enclosing != null)
                                {
                                    if (enclosing.GetLoopback() == cond.End)
                                    {
                                        cond.End = enclosing.End - 1;
                                        hasTail = cond.End >= 2 && Code.Op(cond.End - 1) == Op.Opcode.JMP;
                                        tail = hasTail ? cond.End + Code.sBx(cond.End - 1) : -1;
                                    }

                                    if (hasTail && enclosing.GetLoopback() == tail)
                                        tail = enclosing.End - 1;
                                }

                                if (cond.IsSet)
                                {
                                    bool empty = cond.Begin == cond.End;

                                    if (Code.Op(cond.Begin) == Op.Opcode.JMP && Code.sBx(cond.Begin) == 2 && Code.Op(cond.Begin + 1) == Op.Opcode.LOADBOOL && Code.C(cond.Begin + 1) != 0)
                                        empty = true;

                                    _blocks.Add(new SetBlock(_function, cond, cond.SetTarget, line, cond.Begin, cond.End, empty, _r));
                                }
                                else if (Code.Op(cond.Begin) == Op.Opcode.LOADBOOL && Code.C(cond.Begin) != 0)
                                {
                                    int begin = cond.Begin,
                                        target = Code.A(begin);

                                    if (Code.B(begin) == 0)
                                        cond = cond.Invert();

                                    _blocks.Add(new CompareBlock(_function, begin, begin + 2, target, cond));
                                }
                                else if (cond.End < cond.Begin)
                                {
                                    if (isBreak[cond.End - 1])
                                    {
                                        _skip[cond.End - 1] = true;

                                        _blocks.Add(new WhileBlock(_function, cond.Invert(), originalTail, _r));
                                    }
                                    else
                                    {
                                        _blocks.Add(new RepeatBlock(_function, cond, _r));
                                    }
                                }
                                else if (hasTail)
                                {
                                    Op.Opcode endOp = Code.Op(cond.End - 2);
                                    bool isEndCondJump = endOp == Op.Opcode.EQ || endOp == Op.Opcode.LE || endOp == Op.Opcode.LT || endOp == Op.Opcode.TEST || endOp == Op.Opcode.TESTSET || endOp == Op.Opcode.TEST50;

                                    if (tail > cond.End || (tail == cond.End && !isEndCondJump))
                                    {
                                        Op.Opcode op = Code.Op(tail - 1);

                                        int sbx = Code.sBx(tail - 1),
                                            loopback2 = tail + sbx;

                                        bool isBreakableLoopEnd = _function.Header.Version.IsBreakableLoopEnd(op);

                                        if (isBreakableLoopEnd && loopback2 <= cond.Begin && !isBreak[tail - 1])
                                        {
                                            // Ends with break...
                                            _blocks.Add(new IfThenEndBlock(_function, cond, backup, _r));
                                        }
                                        else
                                        {
                                            _skip[cond.End - 1] = true; // Skip the JMP over the else block.

                                            bool emptyElse = tail == cond.End;
                                            IfThenElseBlock ifthen = new(_function, cond, originalTail, emptyElse, _r);

                                            _blocks.Add(ifthen);

                                            if (!emptyElse)
                                            {
                                                ElseEndBlock elseend = new(_function, cond.End, tail);

                                                _blocks.Add(elseend);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        int loopback = tail;
                                        bool existsStatement = false;

                                        for (int sl = loopback; sl < cond.Begin; sl++)
                                        {
                                            if (!_skip[sl] && IsStatement(sl))
                                            {
                                                existsStatement = true;
                                                break;
                                            }
                                        }

                                        // TODO: check for 5.2-style if cond then break end
                                        if (loopback >= cond.Begin || existsStatement)
                                        {
                                            _blocks.Add(new IfThenEndBlock(_function, cond, backup, _r));
                                        }
                                        else
                                        {
                                            _skip[cond.End - 1] = true;

                                            _blocks.Add(new WhileBlock(_function, cond, originalTail, _r));
                                        }
                                    }
                                }
                                else
                                {
                                    _blocks.Add(new IfThenEndBlock(_function, cond, backup, _r));
                                }
                            }
                            while (conditions.Count != 0);
                        }
                    }
                }

                // Find variables whose scope isn't controlled by existing blocks...
                foreach (Declaration decl in DeclarationList)
                {
                    if (!decl.ForLoop && !decl.ForLoopExplicit)
                    {
                        bool needsDoEnd = true;

                        foreach (Block block in _blocks)
                        {
                            if (block.Contains(decl.Begin))
                            {
                                if (block.ScopeEnd() == decl.End)
                                {
                                    needsDoEnd = false;
                                    break;
                                }
                            }
                        }

                        if (needsDoEnd)
                        {
                            /* Without accounting for the order of declarations, we might create another do..end block later
                               that would eliminate the need for this one. But order of decls should fix this. */
                            _blocks.Add(new DoEndBlock(_function, decl.Begin, decl.End + 1));
                        }
                    }
                }
            }

            List<Block> iter = _blocks;
            int iterIndex = 0;

            while (iterIndex != iter.Count - 1)
            {
                Block block = iter[iterIndex + 1];

                if (_skip[block.Begin] && block is Break)
                    iter.Remove(iter[iterIndex]);

                iterIndex++;
            }

            _blocks.Sort();
            _backup = null;

            return outer;
        }

        private int BreakTarget(int line)
        {
            int targetLine = int.MaxValue;

            foreach (Block block in _blocks)
            {
                if (block.Breakable() && block.Contains(line))
                    targetLine = Math.Min(targetLine, block.End);
            }

            if (targetLine == int.MaxValue)
                return -1;

            return targetLine;
        }

        private Block EnclosingBlock(int line)
        {
            Block outer = _blocks[0], // Assumes the outer block is first.
                  enclosing = outer;

            for (int i = 1; i < _blocks.Count; i++)
            {
                Block next = _blocks[i];

                if (next.IsContainer() && enclosing.Contains(next) && next.Contains(line) && !next.LoopRedirectAdjustment)
                    enclosing = next;
            }

            return enclosing;
        }

        private Block EnclosingBreakableBlock(int line)
        {
            Block outer = _blocks[0],
                  enclosing = outer;

            for (int i = 1; i < _blocks.Count; i++)
            {
                Block next = _blocks[i];

                if (enclosing.Contains(next) && next.Contains(line) && next.Breakable() && !next.LoopRedirectAdjustment)
                    enclosing = next;
            }

            return enclosing == outer ? null : enclosing;
        }

        private Block EnclosingUnprotectedBlock(int line)
        {
            Block outer = _blocks[0], // Assumes the outer block is first.
                  enclosing = outer;

            for (int i = 1; i < _blocks.Count; i++)
            {
                Block next = _blocks[i];

                if (enclosing.Contains(next) && next.Contains(line) && next.IsUnprotected() && !next.LoopRedirectAdjustment)
                    enclosing = next;
            }

            return enclosing == outer ? null : enclosing;
        }

        public Branch PopCondition(Stack<Branch> stack)
        {
            Branch branch = stack.Pop();

            if (_backup != null)
                _backup.Push(branch);

            if (branch is TestSetNode)
                throw new Exception();

            int begin = branch.Begin;

            if (Code.Op(branch.Begin) == Op.Opcode.JMP)
                begin += 1 + Code.sBx(branch.Begin);

            while (stack.Count != 0)
            {
                Branch next = stack.Peek();

                if (next is TestSetNode)
                    break;

                if (next.End == begin)
                {
                    branch = new OrBranch(PopCondition(stack).Invert(), branch);
                }
                else if (next.End == branch.End)
                {
                    branch = new AndBranch(PopCondition(stack), branch);
                }
                else
                {
                    break;
                }
            }

            return branch;
        }

        public Branch PopSetCondition(Stack<Branch> stack, int assignEnd)
        {
            stack.Push(new AssignNode(assignEnd - 1, assignEnd, assignEnd));

            // Invert argument doesn't matter because begin is equal to end.
            Branch result = PopSetCondition(stack, false, assignEnd);

            return result;
        }

        public Branch PopCompareSetCondition(Stack<Branch> stack, int assignEnd)
        {
            Branch top = stack.Pop();
            bool invert = false;

            if (Code.B(top.Begin) == 0)
                invert = true;

            top.Begin = assignEnd;
            top.End = assignEnd;

            stack.Push(top);

            Branch rtn = PopSetCondition(stack, invert, assignEnd);

            return rtn;
        }

        private Branch PopSetCondition(Stack<Branch> stack, bool invert, int assignEnd)
        {
            Branch branch = stack.Pop();

            int begin = branch.Begin,
                end = branch.End;

            if (invert)
                branch = branch.Invert();

            if (Code.Op(begin) == Op.Opcode.LOADBOOL)
            {
                if (Code.C(begin) != 0)
                {
                    begin += 2;
                }
                else
                {
                    begin += 1;
                }
            }

            if (Code.Op(end) == Op.Opcode.LOADBOOL)
            {
                if (Code.C(end) != 0)
                {
                    end += 2;
                }
                else
                {
                    end += 1;
                }
            }

            int target = branch.SetTarget;

            while (stack.Count != 0)
            {
                Branch next = stack.Peek();
                bool ninvert;
                int nend = next.End;

                if (Code.Op(next.End) == Op.Opcode.LOADBOOL)
                {
                    ninvert = Code.B(next.End) != 0;

                    if (Code.C(next.End) != 0)
                    {
                        nend += 2;
                    }
                    else
                    {
                        nend += 1;
                    }
                }
                else if (next is TestSetNode testSetNode)
                {
                    ninvert = testSetNode._Invert;
                }
                else if (next is TestNode testNode)
                {
                    ninvert = testNode._Invert;
                }
                else
                {
                    ninvert = false;

                    if (nend >= assignEnd)
                        break;
                }

                int addr;

                if (ninvert == invert)
                {
                    addr = end;
                }
                else
                {
                    addr = begin;
                }

                if (addr == nend)
                {
                    if (addr != nend)
                        ninvert = !ninvert;

                    if (ninvert)
                    {
                        branch = new OrBranch(PopSetCondition(stack, ninvert, assignEnd), branch);
                    }
                    else
                    {
                        branch = new AndBranch(PopSetCondition(stack, ninvert, assignEnd), branch);
                    }

                    branch.End = nend;
                }
                else
                {
                    if (branch is not TestSetNode)
                    {
                        stack.Push(branch);
                        branch = PopCondition(stack);
                    }

                    break;
                }
            }

            branch.IsSet = true;
            branch.SetTarget = target;

            return branch;
        }

        private bool IsStatement(int line) => IsStatement(line, -1);

        private bool IsStatement(int line, int testRegister)
        {
            switch (Code.Op(line))
            {
                case Op.Opcode.MOVE:
                case Op.Opcode.LOADK:
                case Op.Opcode.LOADBOOL:
                case Op.Opcode.GETUPVAL:
                case Op.Opcode.GETTABUP:
                case Op.Opcode.GETGLOBAL:
                case Op.Opcode.GETTABLE:
                case Op.Opcode.NEWTABLE:
                case Op.Opcode.ADD:
                case Op.Opcode.SUB:
                case Op.Opcode.MUL:
                case Op.Opcode.DIV:
                case Op.Opcode.MOD:
                case Op.Opcode.POW:
                case Op.Opcode.UNM:
                case Op.Opcode.NOT:
                case Op.Opcode.LEN:
                case Op.Opcode.CONCAT:
                case Op.Opcode.CLOSURE:
                    return _r.IsLocal(Code.A(line), line) || Code.A(line) == testRegister;

                case Op.Opcode.LOADNIL:
                {
                    for (int register = Code.A(line); register <= Code.B(line); register++)
                    {
                        if (_r.IsLocal(register, line))
                            return true;
                    }

                    return false;
                }

                case Op.Opcode.SETGLOBAL:
                case Op.Opcode.SETUPVAL:
                case Op.Opcode.SETTABUP:
                case Op.Opcode.SETTABLE:
                case Op.Opcode.JMP:
                case Op.Opcode.TAILCALL:
                case Op.Opcode.RETURN:
                case Op.Opcode.FORLOOP:
                case Op.Opcode.FORPREP:
                case Op.Opcode.TFORPREP:
                case Op.Opcode.TFORCALL:
                case Op.Opcode.TFORLOOP:
                case Op.Opcode.CLOSE:
                    return true;

                case Op.Opcode.SELF:
                    return _r.IsLocal(Code.A(line), line) || _r.IsLocal(Code.A(line) + 1, line);

                case Op.Opcode.EQ:
                case Op.Opcode.LT:
                case Op.Opcode.LE:
                case Op.Opcode.TEST:
                case Op.Opcode.TESTSET:
                case Op.Opcode.TEST50:
                case Op.Opcode.SETLIST:
                case Op.Opcode.SETLISTO:
                case Op.Opcode.SETLIST50:
                    return false;

                case Op.Opcode.CALL:
                {
                    int a = Code.A(line),
                        c = Code.C(line);

                    if (c == 1)
                        return true;

                    if (c == 0)
                        c = _registers - a + 1;

                    for (int register = a; register < a + c - 1; register++)
                    {
                        if (_r.IsLocal(register, line))
                            return true;
                    }

                    return c == 2 && a == testRegister;
                }

                case Op.Opcode.VARARG:
                {
                    int a = Code.A(line),
                        b = Code.B(line);

                    if (b == 0)
                        b = _registers - a + 1;

                    for (int register = a; register < a + b - 1; register++)
                    {
                        if (_r.IsLocal(register, line))
                            return true;
                    }

                    return false;
                }

                default:
                    throw new Exception($"Illegal opcode: {Code.Op(line)}");
            }
        }

        /// <summary>
        /// Returns the single register assigned to at the line or -1 if no register or multiple registers is/are assigned to.
        /// </summary>
        private int GetAssignment(int line)
        {
            switch (Code.Op(line))
            {
                case Op.Opcode.MOVE:
                case Op.Opcode.LOADK:
                case Op.Opcode.LOADBOOL:
                case Op.Opcode.GETUPVAL:
                case Op.Opcode.GETTABUP:
                case Op.Opcode.GETGLOBAL:
                case Op.Opcode.GETTABLE:
                case Op.Opcode.NEWTABLE:
                case Op.Opcode.ADD:
                case Op.Opcode.SUB:
                case Op.Opcode.MUL:
                case Op.Opcode.DIV:
                case Op.Opcode.MOD:
                case Op.Opcode.POW:
                case Op.Opcode.UNM:
                case Op.Opcode.NOT:
                case Op.Opcode.LEN:
                case Op.Opcode.CONCAT:
                case Op.Opcode.CLOSURE:
                    return Code.A(line);

                case Op.Opcode.LOADNIL:
                {
                    if (Code.A(line) == Code.B(line))
                    {
                        return Code.A(line);
                    }
                    else
                    {
                        return -1;
                    }
                }

                case Op.Opcode.SETGLOBAL:
                case Op.Opcode.SETUPVAL:
                case Op.Opcode.SETTABUP:
                case Op.Opcode.SETTABLE:
                case Op.Opcode.JMP:
                case Op.Opcode.TAILCALL:
                case Op.Opcode.RETURN:
                case Op.Opcode.FORLOOP:
                case Op.Opcode.FORPREP:
                case Op.Opcode.TFORCALL:
                case Op.Opcode.TFORLOOP:
                case Op.Opcode.CLOSE:
                case Op.Opcode.SELF:
                case Op.Opcode.EQ:
                case Op.Opcode.LT:
                case Op.Opcode.LE:
                case Op.Opcode.TEST:
                case Op.Opcode.TESTSET:
                case Op.Opcode.SETLIST:
                case Op.Opcode.SETLIST50:
                case Op.Opcode.SETLISTO:
                    return -1;

                case Op.Opcode.CALL:
                {
                    if (Code.C(line) == 2)
                    {
                        return Code.A(line);
                    }
                    else
                    {
                        return -1;
                    }
                }

                case Op.Opcode.VARARG:
                {
                    if (Code.C(line) == 2)
                    {
                        return Code.B(line);
                    }
                    else
                    {
                        return -1;
                    }
                }

                default:
                    throw new Exception($"Illegal opcode: {Code.Op(line)}");
            }
        }
    }
}
