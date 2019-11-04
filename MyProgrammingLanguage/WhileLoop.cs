namespace MyProgrammingLanguage
{
    public class WhileLoop : INullStatement
    {
        private IBooleanReturningStatement condition;
        private INullStatement thenDo;

        public WhileLoop(IBooleanReturningStatement condition, INullStatement thenDo)
        {
            this.condition = condition;
            this.thenDo = thenDo;
        }

        public void execute(Context context)
        {
            while (condition.execute(context)) { thenDo.execute(context); }
        }
    }
}