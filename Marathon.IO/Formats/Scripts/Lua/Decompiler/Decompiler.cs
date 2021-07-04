using System;
using Marathon.IO.Helpers;
using System.Collections.Generic;
using Marathon.IO.Formats.Scripts.Lua.Helpers;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Blocks;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Targets;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Branches;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Statements;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Operations;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler
{
    public class Decompiler
    {
        private readonly int registers, length;
        public readonly Code code;
        private readonly Upvalues upvalues;
        public readonly Declaration[] declList;

        protected Function f;
        protected LFunction function;
        private readonly LFunction[] functions;
        private readonly int @params, vararg;

        private readonly Op.Opcode tforTarget, forTarget;

        public Decompiler(LFunction function)
        {
            f = new Function(function);
            this.function = function;
            registers = function.maximumStackSize;
            length = function.code.Length;
            code = new Code(function);

            if (function.locals.Length >= function.numParams)
            {
                declList = new Declaration[function.locals.Length];

                for (int i = 0; i < declList.Length; i++)
                    declList[i] = new Declaration(function.locals[i]);
            }
            else
            {
                // TODO: debug info missing...

                declList = new Declaration[function.numParams];

                for (int i = 0; i < declList.Length; i++)
                    declList[i] = new Declaration($"_ARG_{i}_", 0, length - 1);
            }

            upvalues = new Upvalues(function.upvalues);
            functions = function.functions;
            @params = function.numParams;
            vararg = function.vararg;
            tforTarget = function.header.version.getTForTarget();
            forTarget = function.header.version.getForTarget();
        }

        private Registers r;
        private Block outer;

        public void decompile()
        {
            r = new Registers(registers, length, declList, f);

            findReverseTargets();
            handleBranches(true);

            outer = handleBranches(false);

            processSequence(1, length);
        }

        public void print() => print(new Output());

        public void print(IOutputProvider @out) => print(new Output(@out));

        public void print(Output @out)
        {
            handleInitialDeclares(@out);

            outer.print(@out);
        }

        private void handleInitialDeclares(Output @out)
        {
            List<Declaration> initdecls = new List<Declaration>(declList.Length);

            for (int i = @params + (vararg & 1); i < declList.Length; i++)
                if (declList[i].begin == 0)
                    initdecls.Add(declList[i]);

            if (initdecls.Count > 0)
            {
                @out.print("local ");
                @out.print(initdecls[0].name);

                for (int i = 1; i < initdecls.Count; i++)
                {
                    @out.print(", ");
                    @out.print(initdecls[i].name);
                }

                @out.println();
            }
        }

        private List<Operation> processLine(int line)
        {
            List<Operation> operations = new List<Operation>();

            int A = code.A(line),
                C = code.C(line),
                B = code.B(line),
                Bx = code.Bx(line);

            switch (code.op(line))
            {
                case Op.Opcode.MOVE:
                {
                    operations.Add(new RegisterSet(line, A, r.getExpression(B, line)));
                    break;
                }

                case Op.Opcode.LOADK:
                {
                    operations.Add(new RegisterSet(line, A, f.getConstantExpression(Bx)));
                    break;
                }

                case Op.Opcode.LOADBOOL:
                {
                    operations.Add(new RegisterSet(line, A, new ConstantExpression(new Constant(B != 0 ? LBoolean.LTRUE : LBoolean.LFALSE), -1)));
                    break;
                }

                case Op.Opcode.LOADNIL:
                {
                    int maximum;

                    if (function.header.version.usesOldLoadNilEncoding())
                        maximum = B;

                    else
                        maximum = A + B;

                    while (A <= maximum)
                    {
                        operations.Add(new RegisterSet(line, A, Expression.NIL));
                        A++;
                    }

                    break;
                }

                case Op.Opcode.GETUPVAL:
                {
                    operations.Add(new RegisterSet(line, A, upvalues.getExpression(B)));
                    break;
                }

                case Op.Opcode.GETTABUP:
                {
                    if (B == 0 && (C & 0x100) != 0)
                        operations.Add(new RegisterSet(line, A, f.getGlobalExpression(C & 0xFF)));

                    else
                        operations.Add(new RegisterSet(line, A, new TableReference(upvalues.getExpression(B), r.getKExpression(C, line))));

                    break;
                }

                case Op.Opcode.GETGLOBAL:
                {
                    operations.Add(new RegisterSet(line, A, f.getGlobalExpression(Bx)));
                    break;
                }

                case Op.Opcode.GETTABLE:
                {
                    operations.Add(new RegisterSet(line, A, new TableReference(r.getExpression(B, line), r.getKExpression(C, line))));
                    break;
                }

                case Op.Opcode.SETUPVAL:
                {
                    operations.Add(new UpvalueSet(line, upvalues.getName(B), r.getExpression(A, line)));
                    break;
                }

                case Op.Opcode.SETTABUP:
                {
                    if (A == 0 && (B & 0x100) != 0)
                        operations.Add(new GlobalSet(line, f.getGlobalName(B & 0xFF), r.getKExpression(C, line)));

                    else
                        operations.Add(new TableSet(line, upvalues.getExpression(A), r.getKExpression(B, line), r.getKExpression(C, line), true, line));

                    break;
                }

                case Op.Opcode.SETGLOBAL:
                {
                    operations.Add(new GlobalSet(line, f.getGlobalName(Bx), r.getExpression(A, line)));
                    break;
                }

                case Op.Opcode.SETTABLE:
                {
                    operations.Add(new TableSet(line, r.getExpression(A, line), r.getKExpression(B, line), r.getKExpression(C, line), true, line));
                    break;
                }

                case Op.Opcode.NEWTABLE:
                {
                    operations.Add(new RegisterSet(line, A, new TableLiteral(B, C)));
                    break;
                }

                case Op.Opcode.SELF:
                {
                    // We can later determine : syntax was used by comparing subexpressions with ==
                    Expression common = r.getExpression(B, line);

                    operations.Add(new RegisterSet(line, A + 1, common));
                    operations.Add(new RegisterSet(line, A, new TableReference(common, r.getKExpression(C, line))));

                    break;
                }

                case Op.Opcode.ADD:
                {
                    operations.Add(new RegisterSet(line, A, Expression.makeADD(r.getKExpression(B, line), r.getKExpression(C, line))));
                    break;
                }

                case Op.Opcode.SUB:
                {
                    operations.Add(new RegisterSet(line, A, Expression.makeSUB(r.getKExpression(B, line), r.getKExpression(C, line))));
                    break;
                }

                case Op.Opcode.MUL:
                {
                    operations.Add(new RegisterSet(line, A, Expression.makeMUL(r.getKExpression(B, line), r.getKExpression(C, line))));
                    break;
                }

                case Op.Opcode.DIV:
                {
                    operations.Add(new RegisterSet(line, A, Expression.makeDIV(r.getKExpression(B, line), r.getKExpression(C, line))));
                    break;
                }

                case Op.Opcode.MOD:
                {
                    operations.Add(new RegisterSet(line, A, Expression.makeMOD(r.getKExpression(B, line), r.getKExpression(C, line))));
                    break;
                }

                case Op.Opcode.POW:
                {
                    operations.Add(new RegisterSet(line, A, Expression.makePOW(r.getKExpression(B, line), r.getKExpression(C, line))));
                    break;
                }

                case Op.Opcode.UNM:
                {
                    operations.Add(new RegisterSet(line, A, Expression.makeUNM(r.getKExpression(B, line))));
                    break;
                }

                case Op.Opcode.NOT:
                {
                    operations.Add(new RegisterSet(line, A, Expression.makeNOT(r.getKExpression(B, line))));
                    break;
                }

                case Op.Opcode.LEN:
                {
                    operations.Add(new RegisterSet(line, A, Expression.makeLEN(r.getKExpression(B, line))));
                    break;
                }

                case Op.Opcode.CONCAT:
                {
                    Expression value = r.getExpression(C, line);

                    // Remember that CONCAT is right associative.
                    while (C-- > B)
                        value = Expression.makeCONCAT(r.getExpression(C, line), value);

                    operations.Add(new RegisterSet(line, A, value));

                    break;
                }

                case Op.Opcode.JMP:
                case Op.Opcode.EQ:
                case Op.Opcode.LT:
                case Op.Opcode.LE:
                case Op.Opcode.TEST:
                case Op.Opcode.TESTSET:
                case Op.Opcode.TEST50:
                    break; // Nothing to see here - handled with branches instead.

                case Op.Opcode.CALL:
                {
                    bool multiple = C >= 3 || C == 0;

                    if (B == 0) B = registers - A;
                    if (C == 0) C = registers - A + 1;

                    Expression function = r.getExpression(A, line);
                    Expression[] arguments = new Expression[B - 1];

                    for (int register = A + 1; register <= A + B - 1; register++)
                        arguments[register - A - 1] = r.getExpression(register, line);

                    FunctionCall value = new FunctionCall(function, arguments, multiple);

                    if (C == 1)
                        operations.Add(new CallOperation(line, value));

                    else
                    {
                        if (C == 2 && !multiple)
                            operations.Add(new CallOperation(line, value));

                        else
                            for (int register = A; register <= A + C - 2; register++)
                                operations.Add(new RegisterSet(line, register, value));
                    }

                    break;
                }

                case Op.Opcode.TAILCALL:
                {
                    if (B == 0) B = registers - A;

                    Expression function = r.getExpression(A, line);
                    Expression[] arguments = new Expression[B - 1];

                    for (int register = A + 1; register <= A + B - 1; register++)
                        arguments[register - A - 1] = r.getExpression(register, line);

                    FunctionCall value = new FunctionCall(function, arguments, true);

                    operations.Add(new ReturnOperation(line, value));

                    skip[line + 1] = true;

                    break;
                }

                case Op.Opcode.RETURN:
                {
                    if (B == 0) B = registers - A + 1;

                    Expression[] values = new Expression[B - 1];

                    for (int register = A; register <= A + B - 2; register++)
                        values[register - A] = r.getExpression(register, line);

                    operations.Add(new ReturnOperation(line, values));

                    break;
                }

                case Op.Opcode.FORLOOP:
                case Op.Opcode.FORPREP:
                case Op.Opcode.TFORPREP:
                case Op.Opcode.TFORCALL:
                case Op.Opcode.TFORLOOP:
                    break; // Nothing to see here - handled with branches instead.

                case Op.Opcode.SETLIST50:
                case Op.Opcode.SETLISTO:
                {
                    Expression table = r.getValue(A, line);
                    int n = Bx % 32;

                    for (int i = 1; i <= n + 1; i++)
                        operations.Add(new TableSet(line, table, new ConstantExpression(new Constant(Bx - n + i), -1), r.getExpression(A + i, line), false, r.getUpdated(A + i, line)));

                    break;
                }

                case Op.Opcode.SETLIST:
                {
                    if (C == 0)
                    {
                        C = code.codepoint(line + 1);
                        skip[line + 1] = true;
                    }

                    if (B == 0)
                        B = registers - A - 1;

                    Expression table = r.getValue(A, line);

                    for (int i = 1; i <= B; i++)
                        operations.Add(new TableSet(line, table, new ConstantExpression(new Constant((C - 1) * 50 + i), -1), r.getExpression(A + i, line), false, r.getUpdated(A + i, line)));

                    break;
                }

                case Op.Opcode.CLOSE:
                    break;

                case Op.Opcode.CLOSURE:
                {
                    LFunction f = functions[Bx];

                    operations.Add(new RegisterSet(line, A, new ClosureExpression(f, declList, line + 1)));

                    if (function.header.version.usesInlineUpvalueDeclarations())
                        // Skip upvalue declarations
                        for (int i = 0; i < f.numUpvalues; i++)
                            skip[line + 1 + i] = true;

                    break;
                }

                case Op.Opcode.VARARG:
                {
                    bool multiple = B != 2;

                    if (B == 1) throw new Exception();
                    if (B == 0) B = registers - A + 1;

                    Expression value = new Vararg(B - 1, multiple);

                    for (int register = A; register <= A + B - 2; register++)
                        operations.Add(new RegisterSet(line, register, value));

                    break;
                }

                default:
                    throw new Exception($"Illegal instruction: {code.op(line)}");
            }

            return operations;
        }

        /// <summary>
        /// When lines are processed out of order, they are noted here so they can be skipped when encountered normally.
        /// </summary>
        bool[] skip;

        /// <summary>
        /// Precalculated array of which lines are the targets of jump instructions that go backwards...
        /// Such targets must be at the statement/block level in the outputted code (they cannot be mid-expression).
        /// </summary>
        bool[] reverseTarget;

        private void findReverseTargets()
        {
            reverseTarget = new bool[length + 1];
            ArrayHelper.Fill(reverseTarget, false);

            for (int line = 1; line <= length; line++)
                if (code.op(line) == Op.Opcode.JMP && code.sBx(line) < 0)
                    reverseTarget[line + 1 + code.sBx(line)] = true;
        }

        private Assignment processOperation(Operation operation, int line, int nextLine, Block block)
        {
            Assignment assign = null;
            bool wasMultiple = false;
            Statement stmt = operation.process(r, block);

            if (stmt != null)
            {
                if (stmt is Assignment)
                {
                    assign = (Assignment)stmt;

                    if (!assign.getFirstValue().isMultiple())
                        block.addStatement(stmt);

                    else
                        wasMultiple = true;
                }
                else
                    block.addStatement(stmt);

                if (assign != null)
                {
                    while (nextLine < block.end && isMoveIntoTarget(nextLine))
                    {
                        Target target = getMoveIntoTargetTarget(nextLine, line + 1);
                        Expression value = getMoveIntoTargetValue(nextLine, line + 1);

                        assign.addFirst(target, value);

                        skip[nextLine] = true;

                        nextLine++;
                    }

                    if (wasMultiple && !assign.getFirstValue().isMultiple())
                        block.addStatement(stmt);
                }
            }

            return assign;
        }

        private void processSequence(int begin, int end)
        {
            int blockIndex = 1;

            Helpers.Stack<Block> blockStack = new Helpers.Stack<Block>();
            blockStack.push(blocks[0]);

            skip = new bool[end + 1];

            for (int line = begin; line <= end; line++)
            {
                Operation blockHandler = null;

                while (blockStack.peek().end <= line)
                {
                    Block seqBlock = blockStack.pop();
                    blockHandler = seqBlock.process(this);

                    if (blockHandler != null)
                        break;
                }

                if (blockHandler == null)
                    while (blockIndex < blocks.Count && blocks[blockIndex].begin <= line)
                        blockStack.push(blocks[blockIndex++]);

                Block block = blockStack.peek();

                r.startLine(line);

                if (skip[line])
                {
                    List<Declaration> skipNewLocals = r.getNewLocals(line);

                    if (skipNewLocals.Count != 0)
                    {
                        Assignment skipAssign = new Assignment();
                        skipAssign.declare(skipNewLocals[0].begin);

                        foreach (Declaration decl in skipNewLocals)
                            skipAssign.addLast(new VariableTarget(decl), r.getValue(decl.register, line));

                        blockStack.peek().addStatement(skipAssign);
                    }

                    continue;
                }

                List<Operation> operations = processLine(line);
                List<Declaration> newLocals = r.getNewLocals(blockHandler == null ? line : line - 1);
                Assignment assign = null;

                if (blockHandler == null)
                {
                    if (code.op(line) == Op.Opcode.LOADNIL)
                    {
                        assign = new Assignment();
                        int count = 0;

                        foreach (Operation operation in operations)
                        {
                            RegisterSet set = (RegisterSet)operation;
                            operation.process(r, block);

                            if (r.isAssignable(set.register, set.line))
                            {
                                assign.addLast(r.getTarget(set.register, set.line), set.value);
                                count++;
                            }
                        }

                        if (count > 0)
                            block.addStatement(assign);
                    }

                    else if (code.op(line) == Op.Opcode.TFORPREP)
                        newLocals.Clear();

                    else
                    {
                        foreach (Operation operation in operations)
                        {
                            Assignment temp = processOperation(operation, line, line + 1, block);

                            if (temp != null)
                                assign = temp;
                        }

                        if (assign != null && assign.getFirstValue().isMultiple())
                            block.addStatement(assign);
                    }
                }
                else
                    assign = processOperation(blockHandler, line, line, block);

                if (assign != null)
                    if (newLocals.Count != 0)
                    {
                        assign.declare(newLocals[0].begin);

                        foreach (Declaration decl in newLocals)
                            assign.addLast(new VariableTarget(decl), r.getValue(decl.register, line + 1));
                    }

                if (blockHandler == null)
                    if (assign == null && newLocals.Count != 0 && code.op(line) != Op.Opcode.FORPREP)
                        if (code.op(line) != Op.Opcode.JMP || code.op(line + 1 + code.sBx(line)) != tforTarget)
                        {
                            assign = new Assignment();
                            assign.declare(newLocals[0].begin);

                            foreach (Declaration decl in newLocals)
                                assign.addLast(new VariableTarget(decl), r.getValue(decl.register, line));

                            blockStack.peek().addStatement(assign);
                        }

                if (blockHandler != null)
                {
                    line--;
                    continue;
                }
            }
        }

        private bool isMoveIntoTarget(int line)
        {
            switch (code.op(line))
            {
                case Op.Opcode.MOVE:
                    return r.isAssignable(code.A(line), line) && !r.isLocal(code.B(line), line);

                case Op.Opcode.SETUPVAL:
                case Op.Opcode.SETGLOBAL:
                    return !r.isLocal(code.A(line), line);

                case Op.Opcode.SETTABLE:
                {
                    int C = code.C(line);

                    if (f.isConstant(C))
                        return false;

                    else
                        return !r.isLocal(C, line);
                }

                default:
                    return false;
            }
        }

        private Target getMoveIntoTargetTarget(int line, int previous)
        {
            switch (code.op(line))
            {
                case Op.Opcode.MOVE:
                    return r.getTarget(code.A(line), line);

                case Op.Opcode.SETUPVAL:
                    return new UpvalueTarget(upvalues.getName(code.B(line)));

                case Op.Opcode.SETGLOBAL:
                    return new GlobalTarget(f.getGlobalName(code.Bx(line)));

                case Op.Opcode.SETTABLE:
                    return new TableTarget(r.getExpression(code.A(line), previous), r.getKExpression(code.B(line), previous));

                default:
                    throw new Exception();
            }
        }

        private Expression getMoveIntoTargetValue(int line, int previous)
        {
            int A = code.A(line),
                B = code.B(line),
                C = code.C(line);

            switch (code.op(line))
            {
                case Op.Opcode.MOVE:
                    return r.getValue(B, previous);

                case Op.Opcode.SETUPVAL:
                case Op.Opcode.SETGLOBAL:
                    return r.getExpression(A, previous);

                case Op.Opcode.SETTABLE:
                {
                    if (f.isConstant(C))
                        throw new Exception();

                    else
                        return r.getExpression(C, previous);
                }

                default:
                    throw new Exception();
            }
        }

        private List<Block> blocks;

        private OuterBlock handleBranches(bool first)
        {
            List<Block> oldBlocks = blocks;
            blocks = new List<Block>();

            OuterBlock outer = new OuterBlock(function, length);
            blocks.Add(outer);

            bool[] isBreak = new bool[length + 1],
                   loopRemoved = new bool[length + 1];

            if (!first)
            {
                foreach (Block block in oldBlocks)
                {
                    if (block is AlwaysLoop)
                        blocks.Add(block);

                    if (block is Break)
                    {
                        blocks.Add(block);

                        isBreak[block.begin] = true;
                    }
                }

                List<Block> delete = new List<Block>();

                foreach (Block block in blocks)
                    if (block is AlwaysLoop)
                        foreach (Block block2 in blocks)
                            if (block != block2)
                                if (block.begin == block2.begin)
                                    if (block.end < block2.end)
                                    {
                                        delete.Add(block);

                                        loopRemoved[block.end - 1] = true;
                                    }
                                    else
                                    {
                                        delete.Add(block2);

                                        loopRemoved[block2.end - 1] = true;
                                    }

                foreach (Block block in delete)
                    blocks.Remove(block);

                skip = new bool[length + 1];

                Helpers.Stack<Branch> stack = new Helpers.Stack<Branch>();

                bool reduce = false,
                     testset = false;

                int testsetend = -1;

                for (int line = 1; line <= length; line++)
                {
                    if (!skip[line])
                    {
                        switch (code.op(line))
                        {
                            case Op.Opcode.EQ:
                            {
                                EQNode node = new EQNode(code.B(line), code.C(line), code.A(line) != 0, line, line + 2, line + 2 + code.sBx(line + 1));

                                stack.push(node);

                                skip[line + 1] = true;

                                if (code.op(node.end) == Op.Opcode.LOADBOOL)
                                    if (code.C(node.end) != 0)
                                    {
                                        node.isCompareSet = true;
                                        node.setTarget = code.A(node.end);
                                    }
                                    else if (code.op(node.end - 1) == Op.Opcode.LOADBOOL)
                                        if (code.C(node.end - 1) != 0)
                                        {
                                            node.isCompareSet = true;
                                            node.setTarget = code.A(node.end);
                                        }

                                continue;
                            }

                            case Op.Opcode.LT:
                            {
                                LTNode node = new LTNode(code.B(line), code.C(line), code.A(line) != 0, line, line + 2, line + 2 + code.sBx(line + 1));

                                stack.push(node);

                                skip[line + 1] = true;

                                if (code.op(node.end) == Op.Opcode.LOADBOOL)
                                    if (code.C(node.end) != 0)
                                    {
                                        node.isCompareSet = true;
                                        node.setTarget = code.A(node.end);
                                    }
                                    else if (code.op(node.end - 1) == Op.Opcode.LOADBOOL)
                                        if (code.C(node.end - 1) != 0)
                                        {
                                            node.isCompareSet = true;
                                            node.setTarget = code.A(node.end);
                                        }

                                continue;
                            }

                            case Op.Opcode.LE:
                            {
                                LENode node = new LENode(code.B(line), code.C(line), code.A(line) != 0, line, line + 2, line + 2 + code.sBx(line + 1));

                                stack.push(node);

                                skip[line + 1] = true;

                                if (code.op(node.end) == Op.Opcode.LOADBOOL)
                                    if (code.C(node.end) != 0)
                                    {
                                        node.isCompareSet = true;
                                        node.setTarget = code.A(node.end);
                                    }
                                    else if (code.op(node.end - 1) == Op.Opcode.LOADBOOL)
                                        if (code.C(node.end - 1) != 0)
                                        {
                                            node.isCompareSet = true;
                                            node.setTarget = code.A(node.end);
                                        }

                                continue;
                            }

                            case Op.Opcode.TEST:
                            {
                                stack.push(new TestNode(code.A(line), code.C(line) != 0, line, line + 2, line + 2 + code.sBx(line + 1)));

                                skip[line + 1] = true;

                                continue;
                            }

                            case Op.Opcode.TESTSET:
                            {
                                testset = true;
                                testsetend = line + 2 + code.sBx(line + 1);

                                stack.push(new TestSetNode(code.A(line), code.B(line), code.C(line) != 0, line, line + 2, line + 2 + code.sBx(line + 1)));

                                skip[line + 1] = true;

                                continue;
                            }

                            case Op.Opcode.TEST50:
                            {
                                if (code.A(line) == code.B(line))
                                    stack.push(new TestNode(code.A(line), code.C(line) != 0, line, line + 2, line + 2 + code.sBx(line + 1)));

                                else
                                {
                                    testset = true;
                                    testsetend = line + 2 + code.sBx(line + 1);

                                    stack.push(new TestSetNode(code.A(line), code.B(line), code.C(line) != 0, line, line + 2, line + 2 + code.sBx(line + 1)));
                                }

                                skip[line + 1] = true;

                                continue;
                            }

                            case Op.Opcode.JMP:
                            {
                                reduce = true;

                                int tline = line + 1 + code.sBx(line);

                                if (tline >= 2 && code.op(tline - 1) == Op.Opcode.LOADBOOL && code.C(tline - 1) != 0)
                                {
                                    stack.push(new TrueNode(code.A(tline - 1), false, line, line + 1, tline));

                                    skip[line + 1] = true;
                                }
                                else if (code.op(tline) == tforTarget && !skip[tline])
                                {
                                    int A = code.A(tline),
                                        C = code.C(tline);

                                    if (C == 0) throw new Exception();

                                    r.setInternalLoopVariable(A, tline, line + 1); // TODO: end?
                                    r.setInternalLoopVariable(A + 1, tline, line + 1);
                                    r.setInternalLoopVariable(A + 2, tline, line + 1);

                                    for (int index = 1; index <= C; index++)
                                        r.setInternalLoopVariable(A + 2 + index, line, tline + 2); // TODO: end?

                                    skip[tline] = true;
                                    skip[tline + 1] = true;

                                    blocks.Add(new TForBlock(function, line + 1, tline + 2, A, C, r));
                                }
                                else if (code.op(tline) == forTarget && !skip[tline])
                                {
                                    int A = code.A(tline);

                                    r.setInternalLoopVariable(A, tline, line + 1); // TODO: end?
                                    r.setInternalLoopVariable(A + 1, tline, line + 1);
                                    r.setInternalLoopVariable(A + 2, tline, line + 1);

                                    skip[tline] = true;
                                    skip[tline + 1] = true;

                                    blocks.Add(new ForBlock(function, line + 1, tline + 1, A, r));
                                }
                                else if (code.sBx(line) == 2 && code.op(line + 1) == Op.Opcode.LOADBOOL && code.C(line + 1) != 0)
                                    // This is the tail of a boolean set with a compare node and assign node.
                                    blocks.Add(new BooleanIndicator(function, line));

                                else if (code.op(tline) == Op.Opcode.JMP && code.sBx(tline) + tline == line)
                                {
                                    if (first)
                                        blocks.Add(new AlwaysLoop(function, line, tline + 1));

                                    skip[tline] = true;
                                }
                                else
                                {
                                    if (first || loopRemoved[line] || reverseTarget[line + 1])
                                        if (tline > line)
                                        {
                                            isBreak[line] = true;

                                            blocks.Add(new Break(function, line, tline));
                                        }
                                        else
                                        {
                                            Block enclosing = enclosingBreakableBlock(line);

                                            if (enclosing != null && enclosing.breakable() && code.op(enclosing.end) == Op.Opcode.JMP && code.sBx(enclosing.end) + enclosing.end + 1 == tline)
                                            {
                                                isBreak[line] = true;

                                                blocks.Add(new Break(function, line, enclosing.end));
                                            }
                                            else
                                                blocks.Add(new AlwaysLoop(function, tline, line + 1));
                                        }
                                }

                                break;
                            }

                            case Op.Opcode.FORPREP:
                            {
                                reduce = true;

                                blocks.Add(new ForBlock(function, line + 1, line + 2 + code.sBx(line), code.A(line), r));

                                skip[line + 1 + code.sBx(line)] = true;

                                r.setInternalLoopVariable(code.A(line), line, line + 2 + code.sBx(line));
                                r.setInternalLoopVariable(code.A(line) + 1, line, line + 2 + code.sBx(line));
                                r.setInternalLoopVariable(code.A(line) + 2, line, line + 2 + code.sBx(line));
                                r.setInternalLoopVariable(code.A(line) + 3, line, line + 2 + code.sBx(line));

                                break;
                            }

                            case Op.Opcode.FORLOOP:
                            // Should be skipped by preceding FORPREP.
                            throw new Exception();

                            case Op.Opcode.TFORPREP:
                            {
                                reduce = true;

                                int tline = line + 1 + code.sBx(line),
                                    A = code.A(tline),
                                    C = code.C(tline);

                                r.setInternalLoopVariable(A, tline, line + 1); // TODO: end?
                                r.setInternalLoopVariable(A + 1, tline, line + 1);
                                r.setInternalLoopVariable(A + 2, tline, line + 1);

                                for (int index = 1; index <= C; index++)
                                    r.setInternalLoopVariable(A + 2 + index, line, tline + 2); // TODO: end?

                                skip[tline] = true;
                                skip[tline + 1] = true;

                                blocks.Add(new TForBlock(function, line + 1, tline + 2, A, C, r));

                                break;
                            }

                            default:
                            {
                                reduce = isStatement(line);

                                break;
                            }
                        }

                        if ((line + 1) <= length && reverseTarget[line + 1])
                            reduce = true;

                        if (testset && testsetend == line + 1)
                            reduce = true;

                        if (stack.isEmpty())
                            reduce = false;

                        if (reduce)
                        {
                            reduce = false;

                            Helpers.Stack<Branch> conditions = new Helpers.Stack<Branch>();
                            Helpers.Stack<Helpers.Stack<Branch>> backups = new Helpers.Stack<Helpers.Stack<Branch>>();

                            while (!stack.isEmpty())
                            {
                                bool isAssignNode = stack.peek() is TestSetNode;
                                int assignEnd = stack.peek().end;
                                bool compareCorrect = false;

                                if (stack.peek() is TrueNode)
                                {
                                    isAssignNode = true;
                                    compareCorrect = true;

                                    if (code.C(assignEnd) != 0)
                                        assignEnd += 2;

                                    else
                                        assignEnd += 1;
                                }
                                else if (stack.peek().isCompareSet)
                                {
                                    if (code.op(stack.peek().begin) != Op.Opcode.LOADBOOL || code.C(stack.peek().begin) == 0)
                                    {
                                        isAssignNode = true;

                                        if (code.C(assignEnd) != 0)
                                            assignEnd += 2;

                                        else
                                            assignEnd += 1;

                                        compareCorrect = true;
                                    }
                                }
                                else if (assignEnd - 3 >= 1 && code.op(assignEnd - 2) == Op.Opcode.LOADBOOL && code.C(assignEnd - 2) != 0 && code.op(assignEnd - 3) == Op.Opcode.JMP && code.sBx(assignEnd - 3) == 2)
                                {
                                    if (stack.peek() is TestNode)
                                    {
                                        TestNode node = (TestNode)stack.peek();

                                        if (node.test == code.A(assignEnd - 2))
                                            isAssignNode = true;
                                    }
                                }
                                else if (assignEnd - 2 >= 1 && code.op(assignEnd - 1) == Op.Opcode.LOADBOOL && code.C(assignEnd - 1) != 0 && code.op(assignEnd - 2) == Op.Opcode.JMP && code.sBx(assignEnd - 2) == 2)
                                {
                                    if (stack.peek() is TestNode)
                                    {
                                        isAssignNode = true;
                                        assignEnd += 1;
                                    }
                                }
                                else if (assignEnd - 1 >= 1 && code.op(assignEnd) == Op.Opcode.LOADBOOL && code.C(assignEnd) != 0 && code.op(assignEnd - 1) == Op.Opcode.JMP && code.sBx(assignEnd - 1) == 2)
                                {
                                    if (stack.peek() is TestNode)
                                    {
                                        isAssignNode = true;
                                        assignEnd += 2;
                                    }
                                }
                                else if (assignEnd - 1 >= 1 && r.isLocal(getAssignment(assignEnd - 1), assignEnd - 1) && assignEnd > stack.peek().line)
                                {
                                    Declaration decl = r.getDeclaration(getAssignment(assignEnd - 1), assignEnd - 1);

                                    if (decl.begin == assignEnd - 1 && decl.end > assignEnd - 1)
                                        isAssignNode = true;
                                }

                                if (!compareCorrect && assignEnd - 1 == stack.peek().begin && code.op(stack.peek().begin) == Op.Opcode.LOADBOOL && code.C(stack.peek().begin) != 0)
                                {
                                    backup = null;

                                    int begin = stack.peek().begin;

                                    assignEnd = begin + 2;

                                    int target = code.A(begin);

                                    conditions.push(popCompareSetCondition(stack, assignEnd));
                                    conditions.peek().setTarget = target;
                                    conditions.peek().end = assignEnd;
                                    conditions.peek().begin = begin;
                                }
                                else if (isAssignNode)
                                {
                                    backup = null;

                                    int target = stack.peek().setTarget,
                                        begin = stack.peek().begin;

                                    conditions.push(popSetCondition(stack, assignEnd));
                                    conditions.peek().setTarget = target;
                                    conditions.peek().end = assignEnd;
                                    conditions.peek().begin = begin;
                                }
                                else
                                {
                                    backup = new Helpers.Stack<Branch>();

                                    conditions.push(popCondition(stack));

                                    backup.reverse();
                                }

                                backups.push(backup);
                            }

                            while (!conditions.isEmpty())
                            {
                                Branch cond = conditions.pop();
                                Helpers.Stack<Branch> backup = backups.pop();
                                int _breakTarget = breakTarget(cond.begin);
                                bool breakable = _breakTarget >= 1;

                                if (breakable && code.op(_breakTarget) == Op.Opcode.JMP && function.header.version != Version.LUA50)
                                    _breakTarget += 1 + code.sBx(_breakTarget);

                                if (breakable && _breakTarget == cond.end)
                                {
                                    Block immediateEnclosing = enclosingBlock(cond.begin);

                                    for (int iline = Math.Max(cond.end, immediateEnclosing.end - 1); iline >= Math.Max(cond.begin, immediateEnclosing.begin); iline--)
                                        if (code.op(iline) == Op.Opcode.JMP && iline + 1 + code.sBx(iline) == _breakTarget)
                                        {
                                            cond.end = iline;
                                            break;
                                        }
                                }

                                // A branch has a tail if the instruction just before the target is JMP.
                                bool hasTail = cond.end >= 2 && code.op(cond.end - 1) == Op.Opcode.JMP;

                                // This is the target of the tail JMP.
                                int tail = hasTail ? cond.end + code.sBx(cond.end - 1) : -1,
                                    originalTail = tail;

                                Block enclosing = enclosingUnprotectedBlock(cond.begin);

                                // Checking enclosing unprotected block to undo JMP redirects.
                                if (enclosing != null)
                                {
                                    if (enclosing.getLoopback() == cond.end)
                                    {
                                        cond.end = enclosing.end - 1;
                                        hasTail = cond.end >= 2 && code.op(cond.end - 1) == Op.Opcode.JMP;
                                        tail = hasTail ? cond.end + code.sBx(cond.end - 1) : -1;
                                    }

                                    if (hasTail && enclosing.getLoopback() == tail)
                                        tail = enclosing.end - 1;
                                }

                                if (cond.isSet)
                                {
                                    bool empty = cond.begin == cond.end;

                                    if (code.op(cond.begin) == Op.Opcode.JMP && code.sBx(cond.begin) == 2 && code.op(cond.begin + 1) == Op.Opcode.LOADBOOL && code.C(cond.begin + 1) != 0)
                                        empty = true;

                                    blocks.Add(new SetBlock(function, cond, cond.setTarget, line, cond.begin, cond.end, empty, r));
                                }
                                else if (code.op(cond.begin) == Op.Opcode.LOADBOOL && code.C(cond.begin) != 0)
                                {
                                    int begin = cond.begin,
                                        target = code.A(begin);

                                    if (code.B(begin) == 0)
                                        cond = cond.invert();

                                    blocks.Add(new CompareBlock(function, begin, begin + 2, target, cond));
                                }
                                else if (cond.end < cond.begin)
                                {
                                    if (isBreak[cond.end - 1])
                                    {
                                        skip[cond.end - 1] = true;

                                        blocks.Add(new WhileBlock(function, cond.invert(), originalTail, r));
                                    }
                                    else
                                        blocks.Add(new RepeatBlock(function, cond, r));
                                }
                                else if (hasTail)
                                {
                                    Op.Opcode endOp = code.op(cond.end - 2);
                                    bool isEndCondJump = endOp == Op.Opcode.EQ || endOp == Op.Opcode.LE || endOp == Op.Opcode.LT || endOp == Op.Opcode.TEST || endOp == Op.Opcode.TESTSET || endOp == Op.Opcode.TEST50;

                                    if (tail > cond.end || (tail == cond.end && !isEndCondJump))
                                    {
                                        Op.Opcode op = code.op(tail - 1);

                                        int sbx = code.sBx(tail - 1),
                                            loopback2 = tail + sbx;

                                        bool isBreakableLoopEnd = function.header.version.isBreakableLoopEnd(op);

                                        if (isBreakableLoopEnd && loopback2 <= cond.begin && !isBreak[tail - 1])
                                            // Ends with break...
                                            blocks.Add(new IfThenEndBlock(function, cond, backup, r));

                                        else
                                        {
                                            skip[cond.end - 1] = true; // Skip the JMP over the else block.

                                            bool emptyElse = tail == cond.end;
                                            IfThenElseBlock ifthen = new IfThenElseBlock(function, cond, originalTail, emptyElse, r);

                                            blocks.Add(ifthen);

                                            if (!emptyElse)
                                            {
                                                ElseEndBlock elseend = new ElseEndBlock(function, cond.end, tail);

                                                blocks.Add(elseend);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        int loopback = tail;
                                        bool existsStatement = false;

                                        for (int sl = loopback; sl < cond.begin; sl++)
                                            if (!skip[sl] && isStatement(sl))
                                            {
                                                existsStatement = true;
                                                break;
                                            }

                                        // TODO: check for 5.2-style if cond then break end
                                        if (loopback >= cond.begin || existsStatement)
                                            blocks.Add(new IfThenEndBlock(function, cond, backup, r));

                                        else
                                        {
                                            skip[cond.end - 1] = true;

                                            blocks.Add(new WhileBlock(function, cond, originalTail, r));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                // Find variables whose scope isn't controlled by existing blocks...
                foreach (Declaration decl in declList)
                {
                    if (!decl.forLoop && !decl.forLoopExplicit)
                    {
                        bool needsDoEnd = true;

                        foreach (Block block in blocks)
                            if (block.contains(decl.begin))
                                if (block.scopeEnd() == decl.end)
                                {
                                    needsDoEnd = false;
                                    break;
                                }

                        if (needsDoEnd)
                            /* Without accounting for the order of declarations, we might create another do..end block later
                               that would eliminate the need for this one. But order of decls should fix this. */
                            blocks.Add(new DoEndBlock(function, decl.begin, decl.end + 1));
                    }
                }
            }

            List<Block> iter = blocks;
            int iterIndex = 0;

            while (iterIndex != iter.Count - 1)
            {
                Block block = iter[iterIndex + 1];

                if (skip[block.begin] && block is Break)
                    iter.Remove(iter[iterIndex]);

                iterIndex++;
            }

            blocks.Sort();

            backup = null;

            return outer;
        }

        private int breakTarget(int line)
        {
            int tline = int.MaxValue;

            foreach (Block block in blocks)
            {
                if (block.breakable() && block.contains(line))
                    tline = Math.Min(tline, block.end);
            }

            if (tline == int.MaxValue) return -1;

            return tline;
        }

        private Block enclosingBlock(int line)
        {
            Block outer = blocks[0], // Assumes the outer block is first.
                  enclosing = outer;

            for (int i = 1; i < blocks.Count; i++)
            {
                Block next = blocks[i];

                if (next.isContainer() && enclosing.contains(next) && next.contains(line) && !next.loopRedirectAdjustment)
                    enclosing = next;
            }

            return enclosing;
        }

        private Block enclosingBlock(Block block)
        {
            Block outer = blocks[0], // Assumes the outer block is first.
                  enclosing = outer;

            for (int i = 1; i < blocks.Count; i++)
            {
                Block next = blocks[i];

                if (next == block) continue;

                if (next.contains(block) && enclosing.contains(next))
                    enclosing = next;
            }

            return enclosing;
        }

        private Block enclosingBreakableBlock(int line)
        {
            Block outer = blocks[0],
                  enclosing = outer;

            for (int i = 1; i < blocks.Count; i++)
            {
                Block next = blocks[i];

                if (enclosing.contains(next) && next.contains(line) && next.breakable() && !next.loopRedirectAdjustment)
                    enclosing = next;
            }

            return enclosing == outer ? null : enclosing;
        }

        private Block enclosingUnprotectedBlock(int line)
        {
            Block outer = blocks[0], // Assumes the outer block is first.
                  enclosing = outer;

            for (int i = 1; i < blocks.Count; i++)
            {
                Block next = blocks[i];

                if (enclosing.contains(next) && next.contains(line) && next.isUnprotected() && !next.loopRedirectAdjustment)
                    enclosing = next;
            }

            return enclosing == outer ? null : enclosing;
        }

        private static Helpers.Stack<Branch> backup;

        public Branch popCondition(Helpers.Stack<Branch> stack)
        {
            Branch branch = stack.pop();

            if (backup != null) backup.push(branch);

            if (branch is TestSetNode)
                throw new Exception();

            int begin = branch.begin;

            if (code.op(branch.begin) == Op.Opcode.JMP)
                begin += 1 + code.sBx(branch.begin);

            while (!stack.isEmpty())
            {
                Branch next = stack.peek();

                if (next is TestSetNode)
                    break;

                if (next.end == begin)
                    branch = new OrBranch(popCondition(stack).invert(), branch);

                else if (next.end == branch.end)
                    branch = new AndBranch(popCondition(stack), branch);

                else
                    break;
            }

            return branch;
        }

        public Branch popSetCondition(Helpers.Stack<Branch> stack, int assignEnd)
        {
            stack.push(new AssignNode(assignEnd - 1, assignEnd, assignEnd));

            //Invert argument doesn't matter because begin == end
            Branch rtn = _helper_popSetCondition(stack, false, assignEnd);

            return rtn;
        }

        public Branch popCompareSetCondition(Helpers.Stack<Branch> stack, int assignEnd)
        {
            Branch top = stack.pop();
            bool invert = false;

            if (code.B(top.begin) == 0)
                invert = true;

            top.begin = assignEnd;
            top.end = assignEnd;

            stack.push(top);

            Branch rtn = _helper_popSetCondition(stack, invert, assignEnd);

            return rtn;
        }

        private Branch _helper_popSetCondition(Helpers.Stack<Branch> stack, bool invert, int assignEnd)
        {
            Branch branch = stack.pop();

            int begin = branch.begin,
                end = branch.end;

            if (invert)
                branch = branch.invert();

            if (code.op(begin) == Op.Opcode.LOADBOOL)
            {
                if (code.C(begin) != 0)
                    begin += 2;

                else
                    begin += 1;
            }

            if (code.op(end) == Op.Opcode.LOADBOOL)
            {
                if (code.C(end) != 0)
                    end += 2;

                else
                    end += 1;
            }

            int target = branch.setTarget;

            while (!stack.isEmpty())
            {
                Branch next = stack.peek();
                bool ninvert;
                int nend = next.end;

                if (code.op(next.end) == Op.Opcode.LOADBOOL)
                {
                    ninvert = code.B(next.end) != 0;

                    if (code.C(next.end) != 0)
                        nend += 2;

                    else
                        nend += 1;
                }
                else if (next is TestSetNode)
                {
                    TestSetNode node = (TestSetNode)next;
                    ninvert = node._invert;
                }
                else if (next is TestNode)
                {
                    TestNode node = (TestNode)next;
                    ninvert = node._invert;
                }
                else
                {
                    ninvert = false;

                    if (nend >= assignEnd)
                        break;
                }

                int addr;

                if (ninvert == invert)
                    addr = end;

                else
                    addr = begin;

                if (addr == nend)
                {
                    if (addr != nend)
                        ninvert = !ninvert;

                    if (ninvert)
                        branch = new OrBranch(_helper_popSetCondition(stack, ninvert, assignEnd), branch);

                    else
                        branch = new AndBranch(_helper_popSetCondition(stack, ninvert, assignEnd), branch);

                    branch.end = nend;
                }
                else
                {
                    if (!(branch is TestSetNode))
                    {
                        stack.push(branch);

                        branch = popCondition(stack);
                    }

                    break;
                }
            }

            branch.isSet = true;
            branch.setTarget = target;

            return branch;
        }

        private bool isStatement(int line) => isStatement(line, -1);

        private bool isStatement(int line, int testRegister)
        {
            switch (code.op(line))
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
                    return r.isLocal(code.A(line), line) || code.A(line) == testRegister;

                case Op.Opcode.LOADNIL:
                {
                    for (int register = code.A(line); register <= code.B(line); register++)
                        if (r.isLocal(register, line))
                            return true;

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
                    return r.isLocal(code.A(line), line) || r.isLocal(code.A(line) + 1, line);

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
                    int a = code.A(line),
                        c = code.C(line);

                    if (c == 1)
                        return true;

                    if (c == 0)
                        c = registers - a + 1;

                    for (int register = a; register < a + c - 1; register++)
                        if (r.isLocal(register, line))
                            return true;

                    return c == 2 && a == testRegister;
                }

                case Op.Opcode.VARARG:
                {
                    int a = code.A(line),
                        b = code.B(line);

                    if (b == 0)
                        b = registers - a + 1;

                    for (int register = a; register < a + b - 1; register++)
                        if (r.isLocal(register, line))
                            return true;

                    return false;
                }

                default:
                    throw new Exception($"Illegal opcode: {code.op(line)}");
            }
        }

        /// <summary>
        /// Returns the single register assigned to at the line or -1 if no register or multiple registers is/are assigned to.
        /// </summary>
        private int getAssignment(int line)
        {
            switch (code.op(line))
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
                    return code.A(line);

                case Op.Opcode.LOADNIL:
                {
                    if (code.A(line) == code.B(line))
                        return code.A(line);

                    else
                        return -1;
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
                    if (code.C(line) == 2)
                        return code.A(line);

                    else
                        return -1;
                }

                case Op.Opcode.VARARG:
                {
                    if (code.C(line) == 2)
                        return code.B(line);

                    else
                        return -1;
                }

                default:
                    throw new Exception($"Illegal opcode: {code.op(line)}");
            }
        }
    }
}
