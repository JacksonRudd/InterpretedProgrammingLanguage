using System.Collections.Generic;

namespace Parser
{
    internal class CodeFileAbstraction
    {
        private List<string> list;
        private int index = -1;
        public CodeFileAbstraction(List<string> list)
        {
            this.list = list;
        }

        public string AfterFirstColon()
        {
            return GetCurrentLine().Split(new[] {':'}, 2)[1];
        }
        public string AfterFirstPipe()
        {
            return GetCurrentLine().Split(new[] { '|' }, 2)[1];
        }
        public string GetCurrentLine()
        {
            return list[index].Replace(" ", "").Replace("\t", ""); ;

        }
        public string getNextLine()
        {
            index = index + 1;
            return GetCurrentLine();
        }
    }
}