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
            CodeFileAbstraction codeFile = new CodeFileAbstraction(new List<string>(File.ReadAllLines("program.txt")));
            BlockParser blockParser = new BlockParser();
            blockParser.GetBlock(codeFile).execute(new Context());
            
        }
    }
}
