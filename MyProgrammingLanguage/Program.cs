using System;

namespace MyProgrammingLanguage
{
    class Program
    {
        static void Main(string[] args)
        {
            Block block = new Block()
                .AddChild(new IntegerAssignment(new VariableName("b"), new MyInteger(2)))
                .AddChild(new IntegerAssignment(new VariableName("a"), new MyInteger(5)))
                .AddChild(
                    new WhileLoop(
                        new CompareLessThan(new IntVariableEvaluation(new VariableName("a")),new MyInteger(300) ),
                        new Block()
                            .AddChild(new PrintIntegerReturningStatement(new IntVariableEvaluation(new VariableName("a")))

                            ).AddChild(new IntegerAssignment(new VariableName("a"), new Sum(new IntVariableEvaluation(new VariableName("a")),new MyInteger(me: 5) )))
                        ));
            block.execute(new Context());
        }
    }
}
