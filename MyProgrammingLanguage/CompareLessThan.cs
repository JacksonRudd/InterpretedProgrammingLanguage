namespace MyProgrammingLanguage
{
    public class CompareLessThan : IBooleanReturningStatement
    {
        private IIntegerReturningStatement isThisOneLessThan;
        private IIntegerReturningStatement thisOne;

        public CompareLessThan(IIntegerReturningStatement isThisOneLessThan, IIntegerReturningStatement thisOne)
        {
            this.isThisOneLessThan = isThisOneLessThan;
            this.thisOne = thisOne;
        }
        public bool execute(Context context)
        {
            return isThisOneLessThan.execute(context).IsThisLessThan(thisOne.execute(context));
        }
    }
}