using Marathon.IO.Formats.Scripts.Lua.Decompiler.Expressions;
using Marathon.IO.Formats.Scripts.Lua.Decompiler.Targets;
using System.Collections.Generic;

namespace Marathon.IO.Formats.Scripts.Lua.Decompiler.Statements
{
    public class Assignment : Statement
    {
        private readonly List<Target> targets = new List<Target>(5);
        private readonly List<Expression> values = new List<Expression>(5);

        private bool allnil = true,
                     _declare = false;

        private int declareStart = 0;

        public Assignment() { }

        public Target getFirstTarget() => targets[0];

        public Expression getFirstValue() => values[0];

        public bool assignsTarget(Declaration decl)
        {
            foreach (Target target in targets)
                if (target.isDeclaration(decl))
                    return true;

            return false;
        }

        public int getArity() => targets.Count;

        public Assignment(Target target, Expression value)
        {
            targets.Add(target);
            values.Add(value);
            allnil = allnil && value.isNil();
        }

        public void addFirst(Target target, Expression value)
        {
            targets.Insert(0, target);
            values.Insert(0, value);
            allnil = allnil && value.isNil();
        }

        public void addLast(Target target, Expression value)
        {
            if (targets.Contains(target))
            {
                int index = targets.IndexOf(target);
                value = values[index];
                targets.RemoveAt(index);
            }

            targets.Add(target);
            values.Add(value);
            allnil = allnil && value.isNil();
        }

        public bool assignListEquals(List<Declaration> decls)
        {
            if (decls.Count != targets.Count)
                return false;

            foreach (Target target in targets)
            {
                bool found = false;

                foreach (Declaration decl in decls)
                {
                    if (target.isDeclaration(decl))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                    return false;
            }

            return true;
        }

        public void declare(int declareStart)
        {
            _declare = true;
            this.declareStart = declareStart;
        }

        public override void print(Output @out)
        {
            if (targets.Count != 0)
            {
                if (_declare)
                    @out.print("local ");

                bool functionSugar = false;

                if (targets.Count == 1 && values.Count == 1 && values[0].isClosure() && targets[0].isFunctionName())
                {
                    Expression closure = values[0];

                    // This check only works in Lua version 0x51.
                    if (!_declare || declareStart >= closure.closureUpvalueLine())
                        functionSugar = true;

                    if (targets[0].isLocal() && closure.isUpvalueOf(targets[0].getIndex()))
                        functionSugar = true;
                }

                if (!functionSugar)
                {
                    targets[0].print(@out);

                    for (int i = 1; i < targets.Count; i++)
                    {
                        @out.print(", ");
                        targets[i].print(@out);
                    }

                    if (!_declare || !allnil)
                    {
                        @out.print(" = ");
                        Expression.printSequence(@out, values, false, false);
                    }
                }
                else
                    values[0].printClosure(@out, targets[0]);

                if (comment != null)
                {
                    @out.print(" -- ");
                    @out.print(comment);
                }
            }
        }
    }
}
