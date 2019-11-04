using System;
using System.Collections.Generic;

namespace MyProgrammingLanguage
{
    class Program
    {
        static void Main(string[] args)
        {
            FunctionDefinitionSet functionSet = new FunctionDefinitionSet();
            functionSet.Add(new FunctionName("functionName"),  
                new IntReturningFunction(
                    new Block().AddChild(new IntegerAssignment(new VariableName("b"), new MyInteger(4000))).AddChild(new PrintIntegerReturningStatement(new IntVariableEvaluation(new VariableName("b")))),
                    new IntVariableEvaluation(new VariableName("c")), 
                    new List<VariableName>{new VariableName("c")}
                    ));
            Block block = new Block()
                .AddChild(new IntegerAssignment(new VariableName("b"), new MyInteger(2)))
                .AddChild(new IntegerAssignment(new VariableName("a"), new MyInteger(5)))
                .AddChild(new PrintIntegerReturningStatement(
                    new CallIntegerReturningFunction(
                        functionSet.Lookup(new FunctionName("functionName")), new Context().AddVariableAssignment(new VariableName("c"),new MyInteger(80085) )
                        )))
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

    public class FunctionDefinitionSet
    {
        private Dictionary<string, IntReturningFunction> _dictionary = new Dictionary<string, IntReturningFunction>();

        public void Add(FunctionName functionname, IntReturningFunction intReturningFunction)
        {
            _dictionary[functionname.name] = intReturningFunction;
        }

        public IntReturningFunction Lookup(FunctionName functionname)
        {
            return _dictionary[functionname.name] ;
        }
    }

    public class FunctionName
    {
        public string name;

        public FunctionName(string name)
        {
            this.name = name;
        }
    }
}
