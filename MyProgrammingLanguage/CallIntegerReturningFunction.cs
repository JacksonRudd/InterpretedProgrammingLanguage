using System.Collections.Generic;

namespace MyProgrammingLanguage
{
    public class CallIntegerReturningFunction : IIntegerReturningStatement
    {
        private readonly List<IIntegerReturningStatement> _args;
        private readonly FunctionDefinitionSet _functionSet;
        private readonly FunctionName _name;

        public CallIntegerReturningFunction(List<IIntegerReturningStatement> args, FunctionDefinitionSet functionSet, FunctionName name)
        {
            _args = args;
            _functionSet = functionSet;
            _name = name;
        }
        public MyInteger execute(Context context)
        {
            IntReturningFunction function = _functionSet.Lookup(
                _name
            );
            Context myContext = new Context();

            for (int i = 0; i < function.variableNames.Count; i++)
            {
                myContext.AddVariableAssignment(function.variableNames[i], _args[i].execute(context));
            }
            return function.execute(myContext);
        }
    }
}