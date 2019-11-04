using System;
using System.Collections.Generic;
using System.Linq;

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

        
        public StringAbstraction GetCurrentLine()
        {
            return new StringAbstraction(list[index]);

        }
        public StringAbstraction getNextLine()
        {
            index = index + 1;
            return GetCurrentLine();
        }
    }

    public class StringAbstraction
    {
        private string me;

        public StringAbstraction(string me)
        {
            this.me = me.Replace(" ", "").Replace("\t", ""); ;
        }
        public StringAbstraction AfterFirstColon()
        {
            return new StringAbstraction(me.Split(new[] { ':' }, 2)[1]);
        }

        internal StringAbstraction Replace(string v1, string v2)
        {
            return new StringAbstraction(me.Replace(v1, v2));
        }

        public StringAbstraction AfterFirstPipe()
        {
            return new StringAbstraction(me.Split(new[] { '|' }, 2)[1]);
        }
        public StringAbstraction AfterFirstArrow()
        {
            return new StringAbstraction(me.Split(new[] { '>' }, 2)[1]);
        }

        internal bool Contains(string v)
        {
            return me.Contains(v);
        }

        public bool StartsWith(string sum)
        {
            return me.StartsWith(sum);
        }

        internal List<StringAbstraction> Split(string v)
        {
            return new List<string>(me.Split(v)).Select(x => new StringAbstraction(x)).ToList();
        }

        public string Value()
        {
            return me;
        }

        public StringAbstraction BeforeFirstOccuranceOf(string o)
        {
            return this.Split(o)[0];
        }
        public StringAbstraction AfterFirstOccuranceOf(string o)
        {
            return this.Split(o)[1];
        }

        public override string ToString()
        {
            return Value().ToString();
        }
    }
}