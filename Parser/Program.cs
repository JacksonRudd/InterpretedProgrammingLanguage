using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using MyProgrammingLanguage;

namespace Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("What program would you like to run?");
                CodeFileAbstraction codeFile = new CodeFileAbstraction(new List<string>(File.ReadAllLines(Console.ReadLine() + ".txt")));
                FunctionDefinitionSet functionSet = new FunctionDefinitionSet();
                BlockParser blockParser = new BlockParser();
                blockParser.GetBlock(codeFile, functionSet).execute(new Context());
            }
            
            
        }
    }
}
