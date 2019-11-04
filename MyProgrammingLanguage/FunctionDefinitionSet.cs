using System.Collections.Generic;

namespace MyProgrammingLanguage
{
    public class FunctionDefinitionSet
    {
        private Dictionary<string, IntReturningFunction> _dictionary = new Dictionary<string, IntReturningFunction>();

        public void Add(FunctionName functionname, IntReturningFunction intReturningFunction)
        {
            _dictionary[functionname.name] = intReturningFunction;
        }

        public IntReturningFunction Lookup(FunctionName functionname)
        {
            return _dictionary[functionname.name] ;
        }
    }
}