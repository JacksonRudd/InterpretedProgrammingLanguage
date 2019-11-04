using System;
using System.Collections.Generic;
using MyProgrammingLanguage;

namespace Parser
{
    internal class BlockParser
    {
        public Block GetBlock(CodeFileAbstraction codeFile, FunctionDefinitionSet functionSet)
        {
            Block block = new Block();
            string s;
            Block functionBlock;
            List<string> args;
            while (true)
            {
                s = codeFile.getNextLine();
                if (s.Contains("|"))
                {
                    args = new List<string>(codeFile.AfterFirstPipe().Split(",")); ;
                    functionBlock = GetBlock(codeFile, null);
                    ParseIntReturningExpression(codeFile.getNextLine().Replace("return", ""));
                    functionSet.Add(new FunctionName(s.Split("|")[0]),
                        new IntReturningFunction(
                        functionBlock,
                        ParseIntReturningExpression(codeFile.getNextLine().Replace("return", "")),
                        args
                        ));

                }
                if (s.StartsWith("print:"))
                {
                    block.AddChild(new PrintIntegerReturningStatement(ParseIntReturningExpression(codeFile.AfterFirstColon())));
                    continue;
                }
                if (s.Equals("end") || s.Equals("return")) return block;
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
                    block.AddChild(new WhileLoop(condition, GetBlock(codeFile, functionSet)));
                    continue;
                }
                if (s.StartsWith("if:"))
                {
                    string expression = s.Split(':')[1];
                    IIntegerReturningStatement smaller = ParseIntReturningExpression(expression.Split("<")[0]);
                    IIntegerReturningStatement bigger = ParseIntReturningExpression(expression.Split("<")[1]);
                    IBooleanReturningStatement condition = new CompareLessThan(smaller, bigger);
                    block.AddChild(new IfStatement(condition, GetBlock(codeFile, functionSet)));
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