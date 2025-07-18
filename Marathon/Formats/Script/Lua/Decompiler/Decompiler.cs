using Marathon.Formats.Script.Lua.Types;
using Marathon.Formats.Script.Lua.Decompiler.Blocks;
using Marathon.Formats.Script.Lua.Decompiler.Targets;
using Marathon.Formats.Script.Lua.Decompiler.Branches;
using Marathon.Formats.Script.Lua.Decompiler.Statements;
using Marathon.Formats.Script.Lua.Decompiler.Operations;
using Marathon.Formats.Script.Lua.Decompiler.Expressions;
using System.Collections.Generic;
using System;
using System.Linq;
using Marathon.Formats.Script.Lua.Version;
using Marathon.Formats.Script.Lua.Decompiler.Extractors;

namespace Marathon.Formats.Script.Lua.Decompiler
{
    public class Decompiler
    {
        private readonly int _registerCount;
        private readonly int _codeLength;
        private readonly int _paramCount;
        private readonly int _variadicArgs;

        private readonly Upvalues _upvalues;
        private readonly LFunction[] _functions;

        private readonly Opcode _tforTarget;
        private readonly Opcode _forTarget;

        private static Stack<Branch> _backup;

        private Registers _registers;
        private Block _outerBlock;
        private List<Block> _blocks;

        /// <summary>
        /// When lines are processed out of order, they are noted here so they can be skipped when encountered normally.
        /// </summary>
        private bool[] _skipped;

        /// <summary>
        /// Precalculated array of which lines are the targets of jump instructions that go backwards.
        /// <para>Such targets must be at the statement/block level in the output code (they cannot be mid-expression).</para>
        /// </summary>
        private bool[] _reverseTarget;

        protected Function _function;
        protected LFunction _lFunction;

        public CodeExtractor Code { get; }

        public Declaration[] DeclarationList { get; }

        public Decompiler(LFunction function)
        {
            _function = new Function(function);
            _lFunction = function;
            _registerCount = function.MaximumStackSize;
            _codeLength = function.Code.Length;

            Code = new CodeExtractor(function);

            var i = 0;

            if (function.Locals.Length >= function.ParamCount)
            {
                // FIX: reserve space for variadic arg keyword declaration.
                DeclarationList = new Declaration[function.Locals.Length + function.VariadicArgs];

                for (i = 0; i < DeclarationList.Length; i++)
                    DeclarationList[i] = new Declaration(function.Locals[i]);
            }
            else
            {
                // FIX: reserve space for variadic arg keyword declaration.
                DeclarationList = new Declaration[function.ParamCount + function.VariadicArgs];

                for (i = 0; i < DeclarationList.Length; i++)
                    DeclarationList[i] = new Declaration($"a{i + 1}", 0, _codeLength - 1);
            }

            // FIX: create declaration for variadic args keyword.
            if ((function.VariadicArgs & 1) == 1)
                DeclarationList[i - 1] = new Declaration("arg", 0, _codeLength - 1);

            _upvalues = new Upvalues(function.Upvalues);
            _functions = function.Functions;
            _paramCount = function.ParamCount;
            _variadicArgs = function.VariadicArgs;
            _tforTarget = function.Header.Version.GetTForTarget();
            _forTarget = function.Header.Version.GetForTarget();
        }

        public void Decompile()
        {
            _registers = new Registers(_registerCount, _codeLength, DeclarationList, _function);

            FindReverseTargets();
            HandleBranches(true);

            _outerBlock = HandleBranches(false);

            ProcessSequence(1, _codeLength);
        }

        private void HandleInitialDeclarations(Output in_output)
        {
            var declarations = new List<Declaration>(DeclarationList.Length);

            for (int i = _paramCount + (_variadicArgs & 1); i < DeclarationList.Length; i++)
            {
                if (DeclarationList[i].Begin != 0)
                    continue;

                declarations.Add(DeclarationList[i]);
            }

            if (declarations.Count > 0)
            {
                in_output.Write("local ");
                in_output.Write(declarations[0].Name);

                for (int i = 1; i < declarations.Count; i++)
                {
                    in_output.Write(", ");
                    in_output.Write(declarations[i].Name);
                }

                in_output.WriteLine();
            }
        }

        private List<Operation> ProcessLine(int in_line)
        {
            var operations = new LinkedList<Operation>();
            var A = Code.A(in_line);
            var C = Code.C(in_line);
            var B = Code.B(in_line);
            var Bx = Code.Bx(in_line);

            switch (Code.Op(in_line))
            {
                case Opcode.MOVE:
                    operations.AddLast(new RegisterSet(in_line, A, _registers.GetExpression(B, in_line)));
                    break;

                case Opcode.LOADK:
                    operations.AddLast(new RegisterSet(in_line, A, _function.GetConstantExpression(Bx)));
                    break;

                case Opcode.LOADBOOL:
                    operations.AddLast(new RegisterSet(in_line, A, new ConstantExpression(new Constant(B != 0 ? LBoolean.True : LBoolean.False), -1)));
                    break;

                case Opcode.LOADNIL:
                {
                    int maximum;

                    if (_lFunction.Header.Version.UsesOldLoadNilEncoding())
                    {
                        maximum = B;
                    }
                    else
                    {
                        maximum = A + B;
                    }

                    while (A <= maximum)
                    {
                        operations.AddLast(new RegisterSet(in_line, A, Expression.Nil));
                        A++;
                    }

                    break;
                }

                case Opcode.GETUPVAL:
                    operations.AddLast(new RegisterSet(in_line, A, _upvalues.GetExpression(B)));
                    break;

                case Opcode.GETTABUP:
                {
                    if (B == 0 && (C & 0x100) != 0)
                    {
                        operations.AddLast(new RegisterSet(in_line, A, _function.GetGlobalExpression(C & 0xFF)));
                    }
                    else
                    {
                        operations.AddLast(new RegisterSet(in_line, A, new TableReference(_upvalues.GetExpression(B), _registers.GetConstantExpression(C, in_line))));
                    }

                    break;
                }

                case Opcode.GETGLOBAL:
                    operations.AddLast(new RegisterSet(in_line, A, _function.GetGlobalExpression(Bx)));
                    break;

                case Opcode.GETTABLE:
                    operations.AddLast(new RegisterSet(in_line, A, new TableReference(_registers.GetExpression(B, in_line), _registers.GetConstantExpression(C, in_line))));
                    break;

                case Opcode.SETUPVAL:
                    operations.AddLast(new UpvalueSet(in_line, _upvalues.GetName(B), _registers.GetExpression(A, in_line)));
                    break;

                case Opcode.SETTABUP:
                {
                    if (A == 0 && (B & 0x100) != 0)
                    {
                        operations.AddLast(new GlobalSet(in_line, _function.GetGlobalName(B & 0xFF), _registers.GetConstantExpression(C, in_line)));
                    }
                    else
                    {
                        operations.AddLast(new TableSet(in_line, _upvalues.GetExpression(A), _registers.GetConstantExpression(B, in_line), _registers.GetConstantExpression(C, in_line), true, in_line));
                    }

                    break;
                }

                case Opcode.SETGLOBAL:
                    operations.AddLast(new GlobalSet(in_line, _function.GetGlobalName(Bx), _registers.GetExpression(A, in_line)));
                    break;

                case Opcode.SETTABLE:
                    operations.AddLast(new TableSet(in_line, _registers.GetExpression(A, in_line), _registers.GetConstantExpression(B, in_line), _registers.GetConstantExpression(C, in_line), true, in_line));
                    break;

                case Opcode.NEWTABLE:
                    operations.AddLast(new RegisterSet(in_line, A, new TableLiteral(B, C)));
                    break;

                case Opcode.SELF:
                {
                    // We can later determine ":" syntax was used by comparing sub-expressions with "==" operators.
                    var common = _registers.GetExpression(B, in_line);

                    operations.AddLast(new RegisterSet(in_line, A + 1, common));
                    operations.AddLast(new RegisterSet(in_line, A, new TableReference(common, _registers.GetConstantExpression(C, in_line))));

                    break;
                }

                case Opcode.ADD:
                    operations.AddLast(new RegisterSet(in_line, A, Expression.MakeAdd(_registers.GetConstantExpression(B, in_line), _registers.GetConstantExpression(C, in_line))));
                    break;

                case Opcode.SUB:
                    operations.AddLast(new RegisterSet(in_line, A, Expression.MakeSub(_registers.GetConstantExpression(B, in_line), _registers.GetConstantExpression(C, in_line))));
                    break;

                case Opcode.MUL:
                    operations.AddLast(new RegisterSet(in_line, A, Expression.MakeMul(_registers.GetConstantExpression(B, in_line), _registers.GetConstantExpression(C, in_line))));
                    break;

                case Opcode.DIV:
                    operations.AddLast(new RegisterSet(in_line, A, Expression.MakeDiv(_registers.GetConstantExpression(B, in_line), _registers.GetConstantExpression(C, in_line))));
                    break;

                case Opcode.MOD:
                    operations.AddLast(new RegisterSet(in_line, A, Expression.MakeMod(_registers.GetConstantExpression(B, in_line), _registers.GetConstantExpression(C, in_line))));
                    break;

                case Opcode.POW:
                    operations.AddLast(new RegisterSet(in_line, A, Expression.MakePow(_registers.GetConstantExpression(B, in_line), _registers.GetConstantExpression(C, in_line))));
                    break;

                case Opcode.UNM:
                    operations.AddLast(new RegisterSet(in_line, A, Expression.MakeUnm(_registers.GetConstantExpression(B, in_line))));
                    break;

                case Opcode.NOT:
                    operations.AddLast(new RegisterSet(in_line, A, Expression.MakeNot(_registers.GetConstantExpression(B, in_line))));
                    break;

                case Opcode.LEN:
                    operations.AddLast(new RegisterSet(in_line, A, Expression.MakeLen(_registers.GetConstantExpression(B, in_line))));
                    break;

                case Opcode.CONCAT:
                {
                    Expression value = _registers.GetExpression(C, in_line);

                    // Remember that CONCAT is right associative.
                    while (C-- > B)
                        value = Expression.MakeConcat(_registers.GetExpression(C, in_line), value);

                    operations.AddLast(new RegisterSet(in_line, A, value));

                    break;
                }

                case Opcode.JMP:
                case Opcode.EQ:
                case Opcode.LT:
                case Opcode.LE:
                case Opcode.TEST:
                case Opcode.TESTSET:
                case Opcode.TEST50:
                    break;

                case Opcode.CALL:
                {
                    var isMultiple = C >= 3 || C == 0;

                    if (B == 0)
                        B = _registerCount - A;

                    if (C == 0)
                        C = _registerCount - A + 1;

                    var function = _registers.GetExpression(A, in_line);
                    var arguments = new Expression[B - 1];

                    for (int register = A + 1; register <= A + B - 1; register++)
                        arguments[register - A - 1] = _registers.GetExpression(register, in_line);

                    var value = new FunctionCall(function, arguments, isMultiple);

                    if (C == 1)
                    {
                        operations.AddLast(new CallOperation(in_line, value));
                    }
                    else
                    {
                        if (C == 2 && !isMultiple)
                        {
                            operations.AddLast(new RegisterSet(in_line, A, value));
                        }
                        else
                        {
                            for (int register = A; register <= A + C - 2; register++)
                                operations.AddLast(new RegisterSet(in_line, register, value));
                        }
                    }

                    break;
                }

                case Opcode.TAILCALL:
                {
                    if (B == 0)
                        B = _registerCount - A;

                    var function = _registers.GetExpression(A, in_line);
                    var arguments = new Expression[B - 1];

                    for (int register = A + 1; register <= A + B - 1; register++)
                        arguments[register - A - 1] = _registers.GetExpression(register, in_line);

                    var value = new FunctionCall(function, arguments, true);

                    operations.AddLast(new ReturnOperation(in_line, value));

                    _skipped[in_line + 1] = true;

                    break;
                }

                case Opcode.RETURN:
                {
                    if (B == 0)
                        B = _registerCount - A + 1;

                    var values = new Expression[B - 1];

                    for (int register = A; register <= A + B - 2; register++)
                        values[register - A] = _registers.GetExpression(register, in_line);

                    operations.AddLast(new ReturnOperation(in_line, values));

                    break;
                }

                case Opcode.FORLOOP:
                case Opcode.FORPREP:
                case Opcode.TFORPREP:
                case Opcode.TFORCALL:
                case Opcode.TFORLOOP:
                    break;

                case Opcode.SETLIST50:
                case Opcode.SETLISTO:
                {
                    var table = _registers.GetValue(A, in_line);
                    var tableCount = Bx % 32;

                    for (int i = 1; i <= tableCount + 1; i++)
                        operations.AddLast(new TableSet(in_line, table, new ConstantExpression(new Constant(Bx - tableCount + i), -1), _registers.GetExpression(A + i, in_line), false, _registers.GetUpdated(A + i, in_line)));

                    break;
                }

                case Opcode.SETLIST:
                {
                    if (C == 0)
                    {
                        C = Code.Codepoint(in_line + 1);
                        _skipped[in_line + 1] = true;
                    }

                    if (B == 0)
                        B = _registerCount - A - 1;

                    var table = _registers.GetValue(A, in_line);

                    for (int i = 1; i <= B; i++)
                        operations.AddLast(new TableSet(in_line, table, new ConstantExpression(new Constant((C - 1) * 50 + i), -1), _registers.GetExpression(A + i, in_line), false, _registers.GetUpdated(A + i, in_line)));

                    break;
                }

                case Opcode.CLOSE:
                    break;

                case Opcode.CLOSURE:
                {
                    var function = _functions[Bx];

                    operations.AddLast(new RegisterSet(in_line, A, new ClosureExpression(function, in_line + 1)));

                    if (_lFunction.Header.Version.UsesInlineUpvalueDeclarations())
                    {
                        // Skip upvalue declarations.
                        for (int i = 0; i < function.UpvalueCount; i++)
                            _skipped[in_line + 1 + i] = true;
                    }

                    break;
                }

                case Opcode.VARARG:
                {
                    var isMultiple = B != 2;

                    if (B == 1)
                        throw new NotSupportedException();

                    if (B == 0)
                        B = _registerCount - A + 1;

                    var value = new VariadicArgs(B - 1, isMultiple);

                    for (int register = A; register <= A + B - 2; register++)
                        operations.AddLast(new RegisterSet(in_line, register, value));

                    break;
                }

                default:
                    throw new Exception($"Illegal instruction: {Code.Op(in_line)}");
            }

            return [.. operations];
        }

        private void FindReverseTargets()
        {
            _reverseTarget = new bool[_codeLength + 1];

            Array.Fill(_reverseTarget, false);

            for (int line = 1; line <= _codeLength; line++)
            {
                if (Code.Op(line) == Opcode.JMP && Code.sBx(line) < 0)
                    _reverseTarget[line + 1 + Code.sBx(line)] = true;
            }
        }

        private Assignment ProcessOperation(Operation in_operation, int in_line, int in_nextLine, Block in_block)
        {
            var statement = in_operation.Process(_registers, in_block);
            var wasMultiple = false;

            if (statement == null)
                return null;

            Assignment assignment = null;

            if (statement is Assignment out_assignment)
            {
                assignment = out_assignment;

                if (assignment.GetFirstValue().IsMultiple())
                {
                    wasMultiple = true;
                }
                else
                {
                    in_block.AddStatement(statement);
                }
            }
            else
            {
                in_block.AddStatement(statement);
            }

            if (assignment != null)
            {
                while (in_nextLine < in_block.End && IsMoveIntoTarget(in_nextLine))
                {
                    var target = GetMoveIntoTargetTarget(in_nextLine, in_line + 1);
                    var value = GetMoveIntoTargetValue(in_nextLine, in_line + 1);

                    assignment.AddFirst(target, value);

                    _skipped[in_nextLine] = true;

                    in_nextLine++;
                }

                if (wasMultiple && !assignment.GetFirstValue().IsMultiple())
                    in_block.AddStatement(statement);
            }

            return assignment;
        }

        private void ProcessSequence(int in_begin, int in_end)
        {
            var blockIndex = 1;
            var blockStack = new Stack<Block>();

            blockStack.Push(_blocks[0]);

            _skipped = new bool[in_end + 1];

            for (int line = in_begin; line <= in_end; line++)
            {
                Operation blockHandler = null;

                while (blockStack.Peek().End <= line)
                {
                    var seqBlock = blockStack.Pop();

                    blockHandler = seqBlock.Process(this);

                    if (blockHandler != null)
                        break;
                }

                if (blockHandler == null)
                {
                    while (blockIndex < _blocks.Count && _blocks[blockIndex].Begin <= line)
                        blockStack.Push(_blocks[blockIndex++]);
                }

                var block = blockStack.Peek();

                _registers.StartLine(line);

                if (_skipped[line])
                {
                    var skipNewLocals = _registers.GetNewLocals(line);

                    if (skipNewLocals.Count != 0)
                    {
                        var skipAssign = new Assignment();

                        skipAssign.Declare(skipNewLocals[0].Begin);

                        foreach (var declaration in skipNewLocals)
                            skipAssign.AddLast(new VariableTarget(declaration), _registers.GetValue(declaration.Register, line));

                        blockStack.Peek().AddStatement(skipAssign);
                    }

                    continue;
                }

                var operations = ProcessLine(line);
                var newLocals = _registers.GetNewLocals(blockHandler == null ? line : line - 1);

                Assignment assignment = null;

                if (blockHandler == null)
                {
                    if (Code.Op(line) == Opcode.LOADNIL)
                    {
                        assignment = new Assignment();

                        var count = 0;

                        foreach (var operation in operations)
                        {
                            var set = (RegisterSet)operation;

                            operation.Process(_registers, block);

                            if (_registers.IsAssignable(set.Register, set.Line))
                            {
                                assignment.AddLast(_registers.GetTarget(set.Register, set.Line), set.Value);
                                count++;
                            }
                        }

                        if (count > 0)
                            block.AddStatement(assignment);
                    }
                    else if (Code.Op(line) == Opcode.TFORPREP)
                    {
                        // Lua 5.0 has no assignments for FORPREP.
                        newLocals.Clear();
                    }
                    else
                    {
                        foreach (var operation in operations)
                        {
                            var temp = ProcessOperation(operation, line, line + 1, block);

                            if (temp != null)
                                assignment = temp;
                        }

                        if (assignment != null && assignment.GetFirstValue().IsMultiple())
                            block.AddStatement(assignment);
                    }
                }
                else
                {
                    assignment = ProcessOperation(blockHandler, line, line, block);
                }

                if (assignment != null && newLocals.Count != 0)
                {
                    assignment.Declare(newLocals[0].Begin);

                    foreach (var declaration in newLocals)
                        assignment.AddLast(new VariableTarget(declaration), _registers.GetValue(declaration.Register, line + 1));
                }

                if (blockHandler == null)
                {
                    if (assignment == null && newLocals.Count != 0 && Code.Op(line) != Opcode.FORPREP)
                    {
                        if (Code.Op(line) != Opcode.JMP || Code.Op(line + 1 + Code.sBx(line)) != _tforTarget)
                        {
                            assignment = new Assignment();
                            assignment.Declare(newLocals[0].Begin);

                            foreach (var declaration in newLocals)
                                assignment.AddLast(new VariableTarget(declaration), _registers.GetValue(declaration.Register, line));

                            blockStack.Peek().AddStatement(assignment);
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

        private bool IsMoveIntoTarget(int in_line)
        {
            switch (Code.Op(in_line))
            {
                case Opcode.MOVE:
                    return _registers.IsAssignable(Code.A(in_line), in_line) && !_registers.IsLocal(Code.B(in_line), in_line);

                case Opcode.SETUPVAL:
                case Opcode.SETGLOBAL:
                    return !_registers.IsLocal(Code.A(in_line), in_line);

                case Opcode.SETTABLE:
                {
                    var C = Code.C(in_line);

                    if (_function.IsConstant(C))
                    {
                        return false;
                    }
                    else
                    {
                        return !_registers.IsLocal(C, in_line);
                    }
                }

                default:
                    return false;
            }
        }

        private Target GetMoveIntoTargetTarget(int in_line, int in_previous)
        {
            return Code.Op(in_line) switch
            {
                Opcode.MOVE => _registers.GetTarget(Code.A(in_line), in_line),
                Opcode.SETUPVAL => new UpvalueTarget(_upvalues.GetName(Code.B(in_line))),
                Opcode.SETGLOBAL => new GlobalTarget(_function.GetGlobalName(Code.Bx(in_line))),
                Opcode.SETTABLE => new TableTarget(_registers.GetExpression(Code.A(in_line), in_previous), _registers.GetConstantExpression(Code.B(in_line), in_previous)),
                _ => throw new Exception()
            };
        }

        private Expression GetMoveIntoTargetValue(int in_line, int in_previous)
        {
            var A = Code.A(in_line);
            var B = Code.B(in_line);
            var C = Code.C(in_line);

            switch (Code.Op(in_line))
            {
                case Opcode.MOVE:
                    return _registers.GetValue(B, in_previous);

                case Opcode.SETUPVAL:
                case Opcode.SETGLOBAL:
                    return _registers.GetExpression(A, in_previous);

                case Opcode.SETTABLE:
                {
                    if (_function.IsConstant(C))
                    {
                        throw new Exception();
                    }
                    else
                    {
                        return _registers.GetExpression(C, in_previous);
                    }
                }

                default:
                    throw new Exception();
            }
        }

        private OuterBlock HandleBranches(bool in_isFirst)
        {
            var oldBlocks = new List<Block>();
            var outer = new OuterBlock(_lFunction, _codeLength);
            var isBreak = new bool[_codeLength + 1];
            var loopRemoved = new bool[_codeLength + 1];

            _blocks = [outer];

            if (!in_isFirst)
            {
                foreach (var block in oldBlocks)
                {
                    if (block is AlwaysLoop)
                        _blocks.Add(block);

                    if (block is Break)
                    {
                        _blocks.Add(block);

                        isBreak[block.Begin] = true;
                    }
                }

                var delete = new LinkedList<Block>();

                foreach (var loop in _blocks)
                {
                    if (loop is not AlwaysLoop)
                        continue;

                    foreach (var child in _blocks)
                    {
                        if (loop == child)
                            continue;

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

                foreach (var block in delete)
                    _blocks.Remove(block);

                _skipped = new bool[_codeLength + 1];

                var stack = new Stack<Branch>();

                var testSet = false;
                var testSetEnd = -1;

                for (int line = 1; line <= _codeLength; line++)
                {
                    if (!_skipped[line])
                    {
                        bool reduce;

                        void PushCommonNodeToStack(Branch in_node)
                        {
                            stack.Push(in_node);

                            _skipped[line + 1] = true;

                            if (Code.Op(in_node.End) != Opcode.LOADBOOL)
                                return;

                            if (Code.C(in_node.End) != 0)
                            {
                                in_node.IsCompareSet = true;
                                in_node.SetTarget = Code.A(in_node.End);
                            }
                            else if (Code.Op(in_node.End - 1) == Opcode.LOADBOOL)
                            {
                                if (Code.C(in_node.End - 1) != 0)
                                {
                                    in_node.IsCompareSet = true;
                                    in_node.SetTarget = Code.A(in_node.End);
                                }
                            }
                        }

                        switch (Code.Op(line))
                        {
                            case Opcode.EQ:
                                PushCommonNodeToStack(new EQNode(Code.B(line), Code.C(line), Code.A(line) != 0, line, line + 2, line + 2 + Code.sBx(line + 1)));
                                continue;

                            case Opcode.LT:
                                PushCommonNodeToStack(new LTNode(Code.B(line), Code.C(line), Code.A(line) != 0, line, line + 2, line + 2 + Code.sBx(line + 1)));
                                continue;

                            case Opcode.LE:
                                PushCommonNodeToStack(new LENode(Code.B(line), Code.C(line), Code.A(line) != 0, line, line + 2, line + 2 + Code.sBx(line + 1)));
                                continue;

                            case Opcode.TEST:
                            {
                                stack.Push(new TestNode(Code.A(line), Code.C(line) != 0, line, line + 2, line + 2 + Code.sBx(line + 1)));

                                _skipped[line + 1] = true;

                                continue;
                            }

                            case Opcode.TESTSET:
                            {
                                testSet = true;
                                testSetEnd = line + 2 + Code.sBx(line + 1);

                                stack.Push(new TestSetNode(Code.A(line), Code.B(line), Code.C(line) != 0, line, line + 2, line + 2 + Code.sBx(line + 1)));

                                _skipped[line + 1] = true;

                                continue;
                            }

                            case Opcode.TEST50:
                            {
                                if (Code.A(line) == Code.B(line))
                                {
                                    stack.Push(new TestNode(Code.A(line), Code.C(line) != 0, line, line + 2, line + 2 + Code.sBx(line + 1)));
                                }
                                else
                                {
                                    testSet = true;
                                    testSetEnd = line + 2 + Code.sBx(line + 1);

                                    stack.Push(new TestSetNode(Code.A(line), Code.B(line), Code.C(line) != 0, line, line + 2, line + 2 + Code.sBx(line + 1)));
                                }

                                _skipped[line + 1] = true;

                                continue;
                            }

                            case Opcode.JMP:
                            {
                                reduce = true;

                                var targetLine = line + 1 + Code.sBx(line);

                                if (targetLine >= 2 && Code.Op(targetLine - 1) == Opcode.LOADBOOL && Code.C(targetLine - 1) != 0)
                                {
                                    stack.Push(new TrueNode(Code.A(targetLine - 1), false, line, line + 1, targetLine));

                                    _skipped[line + 1] = true;
                                }
                                else if (Code.Op(targetLine) == _tforTarget && !_skipped[targetLine])
                                {
                                    int A = Code.A(targetLine),
                                        C = Code.C(targetLine);

                                    if (C == 0) throw new Exception();

                                    _registers.SetInternalLoopVariable(A, targetLine, line + 1);
                                    _registers.SetInternalLoopVariable(A + 1, targetLine, line + 1);
                                    _registers.SetInternalLoopVariable(A + 2, targetLine, line + 1);

                                    for (int index = 1; index <= C; index++)
                                        _registers.SetInternalLoopVariable(A + 2 + index, line, targetLine + 2);

                                    _skipped[targetLine] = true;
                                    _skipped[targetLine + 1] = true;

                                    _blocks.Add(new TForBlock(_lFunction, line + 1, targetLine + 2, A, C, _registers));
                                }
                                else if (Code.Op(targetLine) == _forTarget && !_skipped[targetLine])
                                {
                                    int A = Code.A(targetLine);

                                    _registers.SetInternalLoopVariable(A, targetLine, line + 1);
                                    _registers.SetInternalLoopVariable(A + 1, targetLine, line + 1);
                                    _registers.SetInternalLoopVariable(A + 2, targetLine, line + 1);

                                    _skipped[targetLine] = true;
                                    _skipped[targetLine + 1] = true;

                                    _blocks.Add(new ForBlock(_lFunction, line + 1, targetLine + 1, A, _registers));
                                }
                                else if (Code.sBx(line) == 2 && Code.Op(line + 1) == Opcode.LOADBOOL && Code.C(line + 1) != 0)
                                {
                                    // This is the tail of a boolean set with a compare node and assign node.
                                    _blocks.Add(new BooleanIndicator(_lFunction, line));
                                }
                                else if (Code.Op(targetLine) == Opcode.JMP && Code.sBx(targetLine) + targetLine == line)
                                {
                                    if (in_isFirst)
                                        _blocks.Add(new AlwaysLoop(_lFunction, line, targetLine + 1));

                                    _skipped[targetLine] = true;
                                }
                                else
                                {
                                    if (in_isFirst || loopRemoved[line] || _reverseTarget[line + 1])
                                    {
                                        if (targetLine > line)
                                        {
                                            isBreak[line] = true;

                                            _blocks.Add(new Break(_lFunction, line, targetLine));
                                        }
                                        else
                                        {
                                            var enclosing = EnclosingBreakableBlock(line);

                                            if (enclosing != null && enclosing.Breakable() && Code.Op(enclosing.End) == Opcode.JMP && Code.sBx(enclosing.End) + enclosing.End + 1 == targetLine)
                                            {
                                                isBreak[line] = true;

                                                _blocks.Add(new Break(_lFunction, line, enclosing.End));
                                            }
                                            else
                                            {
                                                _blocks.Add(new AlwaysLoop(_lFunction, targetLine, line + 1));
                                            }
                                        }
                                    }
                                }

                                break;
                            }

                            case Opcode.FORPREP:
                            {
                                reduce = true;

                                _blocks.Add(new ForBlock(_lFunction, line + 1, line + 2 + Code.sBx(line), Code.A(line), _registers));

                                _skipped[line + 1 + Code.sBx(line)] = true;

                                _registers.SetInternalLoopVariable(Code.A(line), line, line + 2 + Code.sBx(line));
                                _registers.SetInternalLoopVariable(Code.A(line) + 1, line, line + 2 + Code.sBx(line));
                                _registers.SetInternalLoopVariable(Code.A(line) + 2, line, line + 2 + Code.sBx(line));
                                _registers.SetInternalLoopVariable(Code.A(line) + 3, line, line + 2 + Code.sBx(line));

                                break;
                            }

                            // Should be skipped by preceding FORPREP.
                            case Opcode.FORLOOP:
                                throw new Exception();

                            case Opcode.TFORPREP:
                            {
                                reduce = true;

                                var targetLine = line + 1 + Code.sBx(line);
                                var A = Code.A(targetLine);
                                var C = Code.C(targetLine);

                                _registers.SetInternalLoopVariable(A, targetLine, line + 1);
                                _registers.SetInternalLoopVariable(A + 1, targetLine, line + 1);
                                _registers.SetInternalLoopVariable(A + 2, targetLine, line + 1);

                                for (int index = 1; index <= C; index++)
                                    _registers.SetInternalLoopVariable(A + 2 + index, line, targetLine + 2);

                                _skipped[targetLine] = true;
                                _skipped[targetLine + 1] = true;

                                _blocks.Add(new TForBlock(_lFunction, line + 1, targetLine + 2, A, C, _registers));

                                break;
                            }

                            default:
                                reduce = IsStatement(line);
                                break;
                        }

                        if ((line + 1) <= _codeLength && _reverseTarget[line + 1])
                            reduce = true;

                        if (testSet && testSetEnd == line + 1)
                            reduce = true;

                        if (stack.Count == 0)
                            reduce = false;

                        if (reduce)
                        {
                            reduce = false;

                            var conditions = new Stack<Branch>();
                            var backups = new Stack<Stack<Branch>>();

                            do
                            {
                                var isAssignNode = stack.Peek() is TestSetNode;
                                var assignEnd = stack.Peek().End;
                                var compareCorrect = false;

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
                                    if (Code.Op(stack.Peek().Begin) != Opcode.LOADBOOL || Code.C(stack.Peek().Begin) == 0)
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
                                else if (assignEnd - 3 >= 1 && Code.Op(assignEnd - 2) == Opcode.LOADBOOL && Code.C(assignEnd - 2) != 0 && Code.Op(assignEnd - 3) == Opcode.JMP && Code.sBx(assignEnd - 3) == 2)
                                {
                                    if (stack.Peek() is TestNode out_node)
                                    {
                                        if (out_node.Register == Code.A(assignEnd - 2))
                                            isAssignNode = true;
                                    }
                                }
                                else if (assignEnd - 2 >= 1 && Code.Op(assignEnd - 1) == Opcode.LOADBOOL && Code.C(assignEnd - 1) != 0 && Code.Op(assignEnd - 2) == Opcode.JMP && Code.sBx(assignEnd - 2) == 2)
                                {
                                    if (stack.Peek() is TestNode)
                                    {
                                        isAssignNode = true;
                                        assignEnd += 1;
                                    }
                                }
                                else if (assignEnd - 1 >= 1 && Code.Op(assignEnd) == Opcode.LOADBOOL && Code.C(assignEnd) != 0 && Code.Op(assignEnd - 1) == Opcode.JMP && Code.sBx(assignEnd - 1) == 2)
                                {
                                    if (stack.Peek() is TestNode)
                                    {
                                        isAssignNode = true;
                                        assignEnd += 2;
                                    }
                                }
                                else if (assignEnd - 1 >= 1 && _registers.IsLocal(GetAssignment(assignEnd - 1), assignEnd - 1) && assignEnd > stack.Peek().Line)
                                {
                                    var declaration = _registers.GetDeclaration(GetAssignment(assignEnd - 1), assignEnd - 1);

                                    if (declaration.Begin == assignEnd - 1 && declaration.End > assignEnd - 1)
                                        isAssignNode = true;
                                }

                                if (!compareCorrect && assignEnd - 1 == stack.Peek().Begin && Code.Op(stack.Peek().Begin) == Opcode.LOADBOOL && Code.C(stack.Peek().Begin) != 0)
                                {
                                    _backup = null;

                                    var begin = stack.Peek().Begin;
                                    var target = Code.A(begin);

                                    assignEnd = begin + 2;

                                    conditions.Push(PopCompareSetCondition(stack, assignEnd));
                                    conditions.Peek().SetTarget = target;
                                    conditions.Peek().End = assignEnd;
                                    conditions.Peek().Begin = begin;
                                }
                                else if (isAssignNode)
                                {
                                    _backup = null;

                                    var begin = stack.Peek().Begin;
                                    var target = stack.Peek().SetTarget;

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
                                var condition = conditions.Pop();
                                var backup = backups.Pop();
                                var breakTarget = BreakTarget(condition.Begin);
                                var isBreakable = breakTarget >= 1;

                                if (isBreakable && Code.Op(breakTarget) == Opcode.JMP && _lFunction.Header.Version.Version != LuaVersion.Lua50)
                                    breakTarget += 1 + Code.sBx(breakTarget);

                                if (isBreakable && breakTarget == condition.End)
                                {
                                    var immediateEnclosing = EnclosingBlock(condition.Begin);

                                    for (int i = Math.Max(condition.End, immediateEnclosing.End - 1); i >= Math.Max(condition.Begin, immediateEnclosing.Begin); i--)
                                    {
                                        if (Code.Op(i) == Opcode.JMP && i + 1 + Code.sBx(i) == breakTarget)
                                        {
                                            condition.End = i;
                                            break;
                                        }
                                    }
                                }

                                // A branch has a tail if the instruction just before the target is JMP.
                                var hasTail = condition.End >= 2 && Code.Op(condition.End - 1) == Opcode.JMP;

                                // This is the target of the tail JMP.
                                var tail = hasTail ? condition.End + Code.sBx(condition.End - 1) : -1;
                                var originalTail = tail;

                                var enclosing = EnclosingUnprotectedBlock(condition.Begin);

                                // Checking enclosing unprotected block to undo JMP redirects.
                                if (enclosing != null)
                                {
                                    if (enclosing.GetLoopback() == condition.End)
                                    {
                                        condition.End = enclosing.End - 1;
                                        hasTail = condition.End >= 2 && Code.Op(condition.End - 1) == Opcode.JMP;
                                        tail = hasTail ? condition.End + Code.sBx(condition.End - 1) : -1;
                                    }

                                    if (hasTail && enclosing.GetLoopback() == tail)
                                        tail = enclosing.End - 1;
                                }

                                if (condition.IsSet)
                                {
                                    var isEmpty = condition.Begin == condition.End;

                                    if (Code.Op(condition.Begin) == Opcode.JMP && Code.sBx(condition.Begin) == 2 && Code.Op(condition.Begin + 1) == Opcode.LOADBOOL && Code.C(condition.Begin + 1) != 0)
                                        isEmpty = true;

                                    _blocks.Add(new SetBlock(_lFunction, condition, condition.SetTarget, line, condition.Begin, condition.End, isEmpty, _registers));
                                }
                                else if (Code.Op(condition.Begin) == Opcode.LOADBOOL && Code.C(condition.Begin) != 0)
                                {
                                    var begin = condition.Begin;
                                    var target = Code.A(begin);

                                    if (Code.B(begin) == 0)
                                        condition = condition.Invert();

                                    _blocks.Add(new CompareBlock(_lFunction, begin, begin + 2, target, condition));
                                }
                                else if (condition.End < condition.Begin)
                                {
                                    if (isBreak[condition.End - 1])
                                    {
                                        _skipped[condition.End - 1] = true;

                                        _blocks.Add(new WhileBlock(_lFunction, condition.Invert(), originalTail, _registers));
                                    }
                                    else
                                    {
                                        _blocks.Add(new RepeatBlock(_lFunction, condition, _registers));
                                    }
                                }
                                else if (hasTail)
                                {
                                    var endOpcode = Code.Op(condition.End - 2);
                                    var isEndCondJump = endOpcode == Opcode.EQ || endOpcode == Opcode.LE || endOpcode == Opcode.LT || endOpcode == Opcode.TEST || endOpcode == Opcode.TESTSET || endOpcode == Opcode.TEST50;

                                    if (tail > condition.End || (tail == condition.End && !isEndCondJump))
                                    {
                                        var opcode = Code.Op(tail - 1);

                                        var sBx = Code.sBx(tail - 1);
                                        var loopback2 = tail + sBx;
                                        var isBreakableLoopEnd = _lFunction.Header.Version.IsBreakableLoopEnd(opcode);

                                        if (isBreakableLoopEnd && loopback2 <= condition.Begin && !isBreak[tail - 1])
                                        {
                                            // Ends with break.
                                            _blocks.Add(new IfThenEndBlock(_lFunction, condition, backup, _registers));
                                        }
                                        else
                                        {
                                            // Skip the JMP over the else block.
                                            _skipped[condition.End - 1] = true;

                                            var isEmptyElse = tail == condition.End;

                                            _blocks.Add(new IfThenElseBlock(_lFunction, condition, originalTail, isEmptyElse, _registers));

                                            if (!isEmptyElse)
                                                _blocks.Add(new ElseEndBlock(_lFunction, condition.End, tail));
                                        }
                                    }
                                    else
                                    {
                                        var loopback = tail;
                                        var statementExists = false;

                                        for (int i = loopback; i < condition.Begin; i++)
                                        {
                                            if (!_skipped[i] && IsStatement(i))
                                            {
                                                statementExists = true;
                                                break;
                                            }
                                        }

                                        // TODO: check for 5.2-style "if cond then break end".
                                        if (loopback >= condition.Begin || statementExists)
                                        {
                                            _blocks.Add(new IfThenEndBlock(_lFunction, condition, backup, _registers));
                                        }
                                        else
                                        {
                                            _skipped[condition.End - 1] = true;

                                            _blocks.Add(new WhileBlock(_lFunction, condition, originalTail, _registers));
                                        }
                                    }
                                }
                                else
                                {
                                    _blocks.Add(new IfThenEndBlock(_lFunction, condition, backup, _registers));
                                }
                            }
                            while (conditions.Count != 0);
                        }
                    }
                }

                // Find variables whose scope isn't controlled by existing blocks.
                foreach (var declaration in DeclarationList)
                {
                    if (!declaration.IsForLoop && !declaration.IsForLoopExplicit)
                    {
                        var needsDoEnd = true;

                        foreach (var block in _blocks)
                        {
                            if (!block.Contains(declaration.Begin))
                                continue;

                            if (block.ScopeEnd() == declaration.End)
                            {
                                needsDoEnd = false;
                                break;
                            }
                        }

                        if (needsDoEnd)
                        {
                            /* Without accounting for the order of declarations, we might create another "do end" block
                               later that would eliminate the need for this one. But order of decls should fix this. */
                            _blocks.Add(new DoEndBlock(_lFunction, declaration.Begin, declaration.End + 1));
                        }
                    }
                }
            }

            var iter = _blocks;
            int iterIndex = 0;

            while (iterIndex != iter.Count - 1)
            {
                var block = iter[iterIndex + 1];

                if (_skipped[block.Begin] && block is Break)
                    iter.Remove(iter[iterIndex]);

                iterIndex++;
            }

            _blocks.Sort();
            _backup = null;

            return outer;
        }

        private int BreakTarget(int in_line)
        {
            var targetLine = int.MaxValue;

            foreach (var block in _blocks)
            {
                if (block.Breakable() && block.Contains(in_line))
                    targetLine = Math.Min(targetLine, block.End);
            }

            if (targetLine == int.MaxValue)
                return -1;

            return targetLine;
        }

        private Block EnclosingBlock(int in_line)
        {
            var outer = _blocks[0]; // Assumes the outer block is first.
            var enclosing = outer;

            for (int i = 1; i < _blocks.Count; i++)
            {
                var next = _blocks[i];

                if (next.IsContainer() && enclosing.Contains(next) && next.Contains(in_line) && !next.LoopRedirectAdjustment)
                    enclosing = next;
            }

            return enclosing;
        }

        private Block EnclosingBreakableBlock(int in_line)
        {
            var outer = _blocks[0];
            var enclosing = outer;

            for (int i = 1; i < _blocks.Count; i++)
            {
                var next = _blocks[i];

                if (enclosing.Contains(next) && next.Contains(in_line) && next.Breakable() && !next.LoopRedirectAdjustment)
                    enclosing = next;
            }

            return enclosing == outer ? null : enclosing;
        }

        private Block EnclosingUnprotectedBlock(int in_line)
        {
            var outer = _blocks[0]; // Assumes the outer block is first.
            var enclosing = outer;

            for (int i = 1; i < _blocks.Count; i++)
            {
                var next = _blocks[i];

                if (enclosing.Contains(next) && next.Contains(in_line) && next.IsUnprotected() && !next.LoopRedirectAdjustment)
                    enclosing = next;
            }

            return enclosing == outer ? null : enclosing;
        }

        public Branch PopCondition(Stack<Branch> in_stack)
        {
            var branch = in_stack.Pop();

            _backup?.Push(branch);

            if (branch is TestSetNode)
                throw new Exception();

            var begin = branch.Begin;

            if (Code.Op(branch.Begin) == Opcode.JMP)
                begin += 1 + Code.sBx(branch.Begin);

            while (in_stack.Count != 0)
            {
                var next = in_stack.Peek();

                if (next is TestSetNode)
                    break;

                if (next.End == begin)
                {
                    branch = new OrBranch(PopCondition(in_stack).Invert(), branch);
                }
                else if (next.End == branch.End)
                {
                    branch = new AndBranch(PopCondition(in_stack), branch);
                }
                else
                {
                    break;
                }
            }

            return branch;
        }

        public Branch PopSetCondition(Stack<Branch> in_stack, int in_assignEnd)
        {
            in_stack.Push(new AssignNode(in_assignEnd - 1, in_assignEnd, in_assignEnd));

            // Invert argument doesn't matter because begin is equal to end.
            return PopSetCondition(in_stack, false, in_assignEnd);
        }

        public Branch PopCompareSetCondition(Stack<Branch> in_stack, int in_assignEnd)
        {
            var top = in_stack.Pop();
            var invert = false;

            if (Code.B(top.Begin) == 0)
                invert = true;

            top.Begin = in_assignEnd;
            top.End = in_assignEnd;

            in_stack.Push(top);

            return PopSetCondition(in_stack, invert, in_assignEnd);
        }

        private Branch PopSetCondition(Stack<Branch> in_stack, bool in_isInverted, int in_assignEnd)
        {
            var branch = in_stack.Pop();
            var begin = branch.Begin;
            var end = branch.End;

            if (in_isInverted)
                branch = branch.Invert();

            if (Code.Op(begin) == Opcode.LOADBOOL)
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

            if (Code.Op(end) == Opcode.LOADBOOL)
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

            var target = branch.SetTarget;

            while (in_stack.Count != 0)
            {
                var next = in_stack.Peek();
                var nextEnd = next.End;

                bool isNextBlockInverted;

                if (Code.Op(next.End) == Opcode.LOADBOOL)
                {
                    isNextBlockInverted = Code.B(next.End) != 0;

                    if (Code.C(next.End) != 0)
                    {
                        nextEnd += 2;
                    }
                    else
                    {
                        nextEnd += 1;
                    }
                }
                else if (next is TestSetNode out_testSetNode)
                {
                    isNextBlockInverted = out_testSetNode.IsInverted;
                }
                else if (next is TestNode out_testNode)
                {
                    isNextBlockInverted = out_testNode.IsInverted;
                }
                else
                {
                    isNextBlockInverted = false;

                    if (nextEnd >= in_assignEnd)
                        break;
                }

                int addr;

                if (isNextBlockInverted == in_isInverted)
                {
                    addr = end;
                }
                else
                {
                    addr = begin;
                }

                if (addr == nextEnd)
                {
                    if (addr != nextEnd)
                        isNextBlockInverted = !isNextBlockInverted;

                    if (isNextBlockInverted)
                    {
                        branch = new OrBranch(PopSetCondition(in_stack, isNextBlockInverted, in_assignEnd), branch);
                    }
                    else
                    {
                        branch = new AndBranch(PopSetCondition(in_stack, isNextBlockInverted, in_assignEnd), branch);
                    }

                    branch.End = nextEnd;
                }
                else
                {
                    if (branch is not TestSetNode)
                    {
                        in_stack.Push(branch);
                        branch = PopCondition(in_stack);
                    }

                    break;
                }
            }

            branch.IsSet = true;
            branch.SetTarget = target;

            return branch;
        }

        private bool IsStatement(int in_line)
        {
            return IsStatement(in_line, -1);
        }

        private bool IsStatement(int in_line, int in_testRegister)
        {
            switch (Code.Op(in_line))
            {
                case Opcode.MOVE:
                case Opcode.LOADK:
                case Opcode.LOADBOOL:
                case Opcode.GETUPVAL:
                case Opcode.GETTABUP:
                case Opcode.GETGLOBAL:
                case Opcode.GETTABLE:
                case Opcode.NEWTABLE:
                case Opcode.ADD:
                case Opcode.SUB:
                case Opcode.MUL:
                case Opcode.DIV:
                case Opcode.MOD:
                case Opcode.POW:
                case Opcode.UNM:
                case Opcode.NOT:
                case Opcode.LEN:
                case Opcode.CONCAT:
                case Opcode.CLOSURE:
                    return _registers.IsLocal(Code.A(in_line), in_line) || Code.A(in_line) == in_testRegister;

                case Opcode.LOADNIL:
                {
                    for (int register = Code.A(in_line); register <= Code.B(in_line); register++)
                    {
                        if (_registers.IsLocal(register, in_line))
                            return true;
                    }

                    return false;
                }

                case Opcode.SETGLOBAL:
                case Opcode.SETUPVAL:
                case Opcode.SETTABUP:
                case Opcode.SETTABLE:
                case Opcode.JMP:
                case Opcode.TAILCALL:
                case Opcode.RETURN:
                case Opcode.FORLOOP:
                case Opcode.FORPREP:
                case Opcode.TFORPREP:
                case Opcode.TFORCALL:
                case Opcode.TFORLOOP:
                case Opcode.CLOSE:
                    return true;

                case Opcode.SELF:
                    return _registers.IsLocal(Code.A(in_line), in_line) || _registers.IsLocal(Code.A(in_line) + 1, in_line);

                case Opcode.EQ:
                case Opcode.LT:
                case Opcode.LE:
                case Opcode.TEST:
                case Opcode.TESTSET:
                case Opcode.TEST50:
                case Opcode.SETLIST:
                case Opcode.SETLISTO:
                case Opcode.SETLIST50:
                    return false;

                case Opcode.CALL:
                {
                    var A = Code.A(in_line);
                    var C = Code.C(in_line);

                    if (C == 1)
                        return true;

                    if (C == 0)
                        C = _registerCount - A + 1;

                    for (int register = A; register < A + C - 1; register++)
                    {
                        if (_registers.IsLocal(register, in_line))
                            return true;
                    }

                    return C == 2 && A == in_testRegister;
                }

                case Opcode.VARARG:
                {
                    var A = Code.A(in_line);
                    var B = Code.B(in_line);

                    if (B == 0)
                        B = _registerCount - A + 1;

                    for (int register = A; register < A + B - 1; register++)
                    {
                        if (_registers.IsLocal(register, in_line))
                            return true;
                    }

                    return false;
                }

                default:
                    throw new Exception($"Illegal opcode: {Code.Op(in_line)}");
            }
        }

        /// <summary>
        /// Returns the single register assigned to at the line or -1 if no register or multiple registers is or are assigned to.
        /// </summary>
        private int GetAssignment(int in_line)
        {
            switch (Code.Op(in_line))
            {
                case Opcode.MOVE:
                case Opcode.LOADK:
                case Opcode.LOADBOOL:
                case Opcode.GETUPVAL:
                case Opcode.GETTABUP:
                case Opcode.GETGLOBAL:
                case Opcode.GETTABLE:
                case Opcode.NEWTABLE:
                case Opcode.ADD:
                case Opcode.SUB:
                case Opcode.MUL:
                case Opcode.DIV:
                case Opcode.MOD:
                case Opcode.POW:
                case Opcode.UNM:
                case Opcode.NOT:
                case Opcode.LEN:
                case Opcode.CONCAT:
                case Opcode.CLOSURE:
                    return Code.A(in_line);

                case Opcode.LOADNIL:
                {
                    if (Code.A(in_line) == Code.B(in_line))
                    {
                        return Code.A(in_line);
                    }
                    else
                    {
                        return -1;
                    }
                }

                case Opcode.SETGLOBAL:
                case Opcode.SETUPVAL:
                case Opcode.SETTABUP:
                case Opcode.SETTABLE:
                case Opcode.JMP:
                case Opcode.TAILCALL:
                case Opcode.RETURN:
                case Opcode.FORLOOP:
                case Opcode.FORPREP:
                case Opcode.TFORCALL:
                case Opcode.TFORLOOP:
                case Opcode.CLOSE:
                case Opcode.SELF:
                case Opcode.EQ:
                case Opcode.LT:
                case Opcode.LE:
                case Opcode.TEST:
                case Opcode.TESTSET:
                case Opcode.SETLIST:
                case Opcode.SETLIST50:
                case Opcode.SETLISTO:
                    return -1;

                case Opcode.CALL:
                {
                    if (Code.C(in_line) == 2)
                    {
                        return Code.A(in_line);
                    }
                    else
                    {
                        return -1;
                    }
                }

                case Opcode.VARARG:
                {
                    if (Code.C(in_line) == 2)
                    {
                        return Code.B(in_line);
                    }
                    else
                    {
                        return -1;
                    }
                }

                default:
                    throw new Exception($"Illegal opcode: {Code.Op(in_line)}");
            }
        }

        public void Write(IOutputProvider in_output)
        {
            Write(new Output(in_output));
        }

        public void Write(Output in_output)
        {
            HandleInitialDeclarations(in_output);

            _outerBlock.Write(in_output);
        }
    }
}
