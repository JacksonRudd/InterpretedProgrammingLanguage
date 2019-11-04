using System;
using System.Collections.Generic;
using System.Linq;
using MyProgrammingLanguage;

namespace Parser
{
    internal class BlockParser
    {
        public Block GetBlock(CodeFileAbstraction codeFile, FunctionDefinitionSet functionSet)
        {
            Block block = new Block();
            StringAbstraction s;
            Block functionBlock;
            List<StringAbstraction> args;
            while (true)
            {
                s = codeFile.getNextLine();
                var a = s.Value();
                if (s.Contains("|"))
                {
                    args = new List<StringAbstraction>(codeFile.GetCurrentLine().AfterFirstPipe().Split(",")); ;
                    functionBlock = GetBlock(codeFile, null);
                    functionSet.Add(new FunctionName(s.Split("|")[0].Value()),
                        new IntReturningFunction(
                        functionBlock,
                        ParseIntReturningExpression(codeFile.GetCurrentLine().Replace("return", ""), functionSet),
                        args.Select(x => new VariableName(x.Value())).ToList()

                        ));
                    continue;
                }
               
                if (s.StartsWith("print:"))
                {
                    block.AddChild(new PrintIntegerReturningStatement(ParseIntReturningExpression(codeFile.GetCurrentLine().AfterFirstColon(), functionSet)));
                    continue;
                }
                if (s.Value().Equals("end") || s.StartsWith("return")) return block;
                if (s.Contains("="))
                {
                    var split = s.Split("=");
                    IIntegerReturningStatement toSetTo = ParseIntReturningExpression(split[1], functionSet);
                    block.AddChild(new IntegerAssignment(new VariableName(split[0].Value().ToString()), toSetTo));
                    continue;
                }
                if (s.StartsWith("while:"))
                {
                    StringAbstraction expression = codeFile.GetCurrentLine().AfterFirstColon();
                    IIntegerReturningStatement smaller = ParseIntReturningExpression(expression.Split("<")[0], functionSet);
                    IIntegerReturningStatement bigger = ParseIntReturningExpression(expression.Split("<")[1], functionSet);
                    IBooleanReturningStatement condition = new CompareLessThan(smaller, bigger);
                    block.AddChild(new WhileLoop(condition, GetBlock(codeFile, functionSet)));
                    continue;
                }
                if (s.StartsWith("if:"))
                {
                    StringAbstraction expression = s.Split(":")[1];
                    IIntegerReturningStatement smaller = ParseIntReturningExpression(expression.Split("<")[0], functionSet);
                    IIntegerReturningStatement bigger = ParseIntReturningExpression(expression.Split("<")[1], functionSet);
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


        private static IIntegerReturningStatement ParseIntReturningExpression(StringAbstraction s, FunctionDefinitionSet functionSet)
        {
            int n;
            bool isNumeric = Int32.TryParse(s.Value(), out n);
            if (isNumeric) return new MyInteger(n);
            if (s.Contains(">"))
            {
                var args = new List<StringAbstraction>(s.AfterFirstArrow().Split(",")); ;
                IntReturningFunction function = functionSet.Lookup(
                    new FunctionName(
                        s.Split(">")[0].Value()
                        )
                    );
                Context myContext = new Context();

                for (int i = 0; i < function.variableNames.Count; i++)
                {
                    myContext.AddVariableAssignment(function.variableNames[i], ParseIntReturningExpression(args[i],functionSet).execute(myContext));
                }

                return new CallIntegerReturningFunction(function, myContext);
            }
            if (s.StartsWith("sum:"))
            {
                List<StringAbstraction> args = s.AfterFirstColon().Split(",");
                IIntegerReturningStatement a = ParseIntReturningExpression(args[0],functionSet);
                IIntegerReturningStatement b = ParseIntReturningExpression(args[1],functionSet);

                return new Sum(a, b);

            }
            if (s.Contains("(")) return IntReturningFunctionCallParser(s.Value());
            return new IntVariableEvaluation(new VariableName(s.Value()));
        }
    }
}