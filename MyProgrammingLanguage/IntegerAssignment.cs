namespace MyProgrammingLanguage
{
    public class IntegerAssignment : INullStatement
    {
        private VariableName varName;
        private IIntegerReturningStatement value;

        public IntegerAssignment(VariableName varName, IIntegerReturningStatement value)
        {
            this.varName = varName;
            this.value = value;
        }

        public void execute(Context context)
        {
            context.AddVariableAssignment(varName, value.execute(context));
        }
    }
}