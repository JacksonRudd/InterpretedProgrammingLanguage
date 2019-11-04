namespace MyProgrammingLanguage
{
    public class PrintIntegerReturningStatement : INullStatement
    {
        private IIntegerReturningStatement statment;

        public PrintIntegerReturningStatement(IIntegerReturningStatement statment)
        {
            this.statment = statment;
        }
        public void execute(Context context)
        {
            statment.execute(context).Print();
        }
    }
}