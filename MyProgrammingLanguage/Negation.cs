namespace MyProgrammingLanguage
{
    public class Negation : IIntegerReturningStatement
    {
        private IIntegerReturningStatement statement;

        public Negation(IIntegerReturningStatement _statement)
        {
            this.statement = _statement;
        }
        public MyInteger execute(Context context)
        {
            return statement.execute(context).Negate();
        }
    }
}