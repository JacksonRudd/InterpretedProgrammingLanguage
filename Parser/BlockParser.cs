using System;
using MyProgrammingLanguage;

namespace Parser
{
    internal class BlockParser
    {
        public Block GetBlock(CodeFileAbstraction codeFile)
        {
            Block block = new Block();
            string s;
            while (true)
            {
                s = codeFile.getNextLine();
                if (s.StartsWith("print:"))
                {
                    block.AddChild(new PrintIntegerReturningStatement(ParseIntReturningExpression(codeFile.AfterFirstColon())));
                    continue;
                }
                if (s.Equals("end")) return block;
                if (s.Contains("="))
                {
                    var split = s.Split("=");
                    IIntegerReturningStatement toSetTo = ParseIntReturningExpression(split[1]);
                    block.AddChild(new IntegerAssignment(new VariableName(split[0]), toSetTo));
                    continue;
                }
                if (s.StartsWith("while:"))
                {
                    string expression = codeFile.AfterFirstColon();
                    IIntegerReturningStatement smaller = ParseIntReturningExpression(expression.Split("<")[0]);
                    IIntegerReturningStatement bigger = ParseIntReturningExpression(expression.Split("<")[1]);
                    IBooleanReturningStatement condition = new CompareLessThan(smaller, bigger);
                    block.AddChild(new WhileLoop(condition, GetBlock(codeFile)));
                    continue;
                }
                if (s.StartsWith("if:"))
                {
                    string expression = s.Split(':')[1];
                    IIntegerReturningStatement smaller = ParseIntReturningExpression(expression.Split("<")[0]);
                    IIntegerReturningStatement bigger = ParseIntReturningExpression(expression.Split("<")[1]);
                    IBooleanReturningStatement condition = new CompareLessThan(smaller, bigger);
                    block.AddChild(new IfStatement(condition, GetBlock(codeFile)));
                    continue;
                }


            }
        }

        private static IIntegerReturningStatement IntReturningFunctionCallParser(string s)
        {
            

            throw new NotImplementedException();
        }


        private static IIntegerReturningStatement ParseIntReturningExpression(string s)
        {
            int n;
            bool isNumeric = Int32.TryParse(s, out n);
            if (isNumeric) return new MyInteger(n);
            if (s.StartsWith("sum:"))
            {
                string args = s.Split(':')[1];
                IIntegerReturningStatement a = ParseIntReturningExpression(args.Split(",")[0]);
                IIntegerReturningStatement b = ParseIntReturningExpression(args.Split(",")[1]);

                return new Sum(a, b);

            }
            if (s.Contains("(")) return IntReturningFunctionCallParser(s);
            return new IntVariableEvaluation(new VariableName(s));
        }
    }
}