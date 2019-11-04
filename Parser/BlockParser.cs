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
                    ParseFunctionDefinition(codeFile, functionSet, s);
                    continue;
                }
               
                if (s.StartsWith("print:"))
                {
                    ParsePrint(codeFile, functionSet, block);
                    continue;
                }
                if (s.Value().Equals("end") || s.StartsWith("return")) return block;
                if (s.Contains("="))
                {
                    ParseVariableDeclaration(functionSet, s, block);
                    continue;
                }
                if (s.StartsWith("while:"))
                {
                    ParseWhileLoop(codeFile, functionSet, block);
                    continue;
                }
                if (s.StartsWith("if:"))
                {
                    ParseIf(codeFile, functionSet, block);
                    continue;
                }
            }
        }

        private void ParseFunctionDefinition(CodeFileAbstraction codeFile, FunctionDefinitionSet functionSet, StringAbstraction s)
        {
            List<StringAbstraction> args = new List<StringAbstraction>(codeFile.GetCurrentLine().AfterFirstPipe().Split(","));
            Block functionBlock = GetBlock(codeFile, functionSet);
            functionSet.Add(new FunctionName(s.BeforeFirstOccuranceOf("|").Value()),
                new IntReturningFunction(
                    functionBlock,
                    ParseIntReturningExpression(codeFile.GetCurrentLine().Replace("return", ""), functionSet),
                    args.Select(x => new VariableName(x.Value())).ToList()
                ));
        }

        private static void ParsePrint(CodeFileAbstraction codeFile, FunctionDefinitionSet functionSet, Block block)
        {
            block.AddChild(
                new PrintIntegerReturningStatement(ParseIntReturningExpression(codeFile.GetCurrentLine().AfterFirstColon(),
                    functionSet)));
        }

        private static void ParseVariableDeclaration(FunctionDefinitionSet functionSet, StringAbstraction s, Block block)
        {
            var split = s.Split("=");
            IIntegerReturningStatement toSetTo = ParseIntReturningExpression(s.AfterFirstOccuranceOf("="), functionSet);
            block.AddChild(new IntegerAssignment(new VariableName(s.BeforeFirstOccuranceOf("=").ToString()), toSetTo));
        }

        private void ParseIf(CodeFileAbstraction codeFile, FunctionDefinitionSet functionSet, Block block)
        {
            var condition = ParseLessThanCondition(codeFile, functionSet);
            block.AddChild(new IfStatement(condition, GetBlock(codeFile, functionSet)));
        }

        private void ParseWhileLoop(CodeFileAbstraction codeFile, FunctionDefinitionSet functionSet, Block block)
        {
            var condition = ParseLessThanCondition(codeFile, functionSet);
            block.AddChild(new WhileLoop(condition, GetBlock(codeFile, functionSet)));
        }

        private static IBooleanReturningStatement ParseLessThanCondition(CodeFileAbstraction codeFile,
            FunctionDefinitionSet functionSet)
        {
            StringAbstraction expression = codeFile.GetCurrentLine().AfterFirstColon();
            IIntegerReturningStatement smaller = ParseIntReturningExpression(expression.BeforeFirstOccuranceOf("<"), functionSet);
            IIntegerReturningStatement bigger  = ParseIntReturningExpression( expression.AfterFirstOccuranceOf("<"), functionSet);
            IBooleanReturningStatement condition = new CompareLessThan(smaller, bigger);
            return condition;
        }


        private static IIntegerReturningStatement ParseIntReturningExpression(StringAbstraction s, FunctionDefinitionSet functionSet)
        {
            
            if (s.IsNumeric()) return s.GetIntValue();
            if (s.Contains(">"))
            {
                return ParseFunctionEvalulation(s, functionSet);
            }
            if (s.StartsWith("sum:"))
            {
                return ParseSum(s, functionSet);
            }
            if(s.StartsWith("-")) return new Negation(new IntVariableEvaluation(new VariableName( s.AfterFirstOccuranceOf("-").Value())));
            return new IntVariableEvaluation(new VariableName(s.Value()));
        }

        private static IIntegerReturningStatement ParseSum(StringAbstraction s, FunctionDefinitionSet functionSet)
        {
            List<StringAbstraction> args = s.AfterFirstColon().Split(",");
            IIntegerReturningStatement a = ParseIntReturningExpression(args[0], functionSet);
            IIntegerReturningStatement b = ParseIntReturningExpression(args[1], functionSet);
            return new Sum(a, b);
        }

        private static IIntegerReturningStatement ParseFunctionEvalulation(StringAbstraction s,
            FunctionDefinitionSet functionSet)
        {
            var args = new List<StringAbstraction>(s.AfterFirstArrow().Split(","));
            return new CallIntegerReturningFunction(args.Select(x => ParseIntReturningExpression(x, functionSet)).ToList(),
                functionSet, new FunctionName(s.Split(">")[0].Value()));
        }
    }
}