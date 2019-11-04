namespace MyProgrammingLanguage
{
    public class IntVariableEvaluation : IIntegerReturningStatement
    {
        private VariableName variableName;

        public IntVariableEvaluation(VariableName variableName)
        {
            this.variableName = variableName;
        }
        public MyInteger execute(Context context)
        {
            return context.GetValue(variableName);
        }
    }
}