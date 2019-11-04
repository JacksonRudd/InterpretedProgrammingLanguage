using System.Collections.Generic;

namespace MyProgrammingLanguage
{
    public class IntReturningFunction : IIntegerReturningStatement
    {
        private Block statements;
        private IIntegerReturningStatement lastStatement;
        public List<VariableName> variableNames;

        public IntReturningFunction(Block statements, IIntegerReturningStatement lastStatement, List<VariableName> variableNames)
        {
            this.statements = statements;
            this.lastStatement = lastStatement;
            this.variableNames = variableNames;
        }

        public MyInteger execute(Context functionContext)
        {
            statements.execute(functionContext);
            return lastStatement.execute(functionContext);
        }


    }
}