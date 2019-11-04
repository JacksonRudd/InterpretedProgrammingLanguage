namespace MyProgrammingLanguage
{
    public class IfStatement : INullStatement
    {
        private IBooleanReturningStatement condition;
        private INullStatement thenDo;

        public IfStatement(IBooleanReturningStatement condition, INullStatement thenDo)
        {
            this.condition = condition;
            this.thenDo = thenDo;
        }
        public void execute(Context context)
        {
            if (condition.execute(context)) thenDo.execute(context);
        }
    }
}