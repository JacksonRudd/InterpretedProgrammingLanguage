using System.Collections.Generic;

namespace MyProgrammingLanguage
{
    public class Context
    {
        private Dictionary<string, MyInteger> variableLookup = new Dictionary<string, MyInteger>();
        public Context AddVariableAssignment(VariableName varName, MyInteger value)
        {
            variableLookup[varName.variableName] = value;
            return this;
        }

        public MyInteger GetValue(VariableName variableName)
        {
            MyInteger myVal;
            variableLookup.TryGetValue(variableName.variableName, out myVal);
            return myVal;

        }
    }
}