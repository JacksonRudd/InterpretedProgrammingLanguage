namespace MyProgrammingLanguage
{
    public class Sum : IIntegerReturningStatement
    {
        IIntegerReturningStatement a;
        IIntegerReturningStatement b;

        public Sum(IIntegerReturningStatement a, IIntegerReturningStatement b)
        {
            this.a = a;
            this.b = b;
        }
        public MyInteger execute(Context context)
        {
            return a.execute(context).Plus(b.execute(context));
        }
    }
}