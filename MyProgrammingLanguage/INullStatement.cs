using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MyProgrammingLanguage
{
    public interface INullStatement
    {
        void execute(Context context);
    }


    public interface IBooleanReturningStatement
    {
        bool execute(Context context);
    }




    public interface IIntegerReturningStatement
    {
        
        MyInteger execute(Context context);
    }

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

    public class IntVariableEvaluation : IIntegerReturningStatement
    {
        private VariableName variableName;

        public IntVariableEvaluation(VariableName variableName)
        {
            this.variableName = variableName;
        }
        public MyInteger execute(Context context)
        {
            return context.GetValue(variableName);
        }
    }
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
    public class MyInteger : IIntegerReturningStatement
    {
        public int me;

        public MyInteger(int me)
        {
            this.me = me;
        }
        public MyInteger execute(Context context)
        {
            return this;
        }

        public bool IsThisLessThan(MyInteger thisOne)
        {
            return me < thisOne.me;
        }

        public void Print()
        {
            Console.WriteLine(me);
        }

        public MyInteger Plus(MyInteger other)
        {
            return new MyInteger(me + other.me);
        }

        public MyInteger Negate()
        {
            me = -me;
            return this;
        }
    }


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
    public class CallIntegerReturningFunction : IIntegerReturningStatement
    {
        private readonly List<IIntegerReturningStatement> _args;
        private readonly FunctionDefinitionSet _functionSet;
        private readonly FunctionName _name;

        public CallIntegerReturningFunction(List<IIntegerReturningStatement> args, FunctionDefinitionSet functionSet, FunctionName name)
        {
            _args = args;
            _functionSet = functionSet;
            _name = name;
        }
        public MyInteger execute(Context context)
        {
            IntReturningFunction function = _functionSet.Lookup(
                _name
            );
            Context myContext = new Context();

            for (int i = 0; i < function.variableNames.Count; i++)
            {
                myContext.AddVariableAssignment(function.variableNames[i], _args[i].execute(context));
            }
            return function.execute(myContext);
        }
    }

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

    public class VariableName
    {
        public string variableName;

        public VariableName(string variableName)
        {
            this.variableName = variableName;
        }

        
    }

    public class Block : INullStatement
    {
        private List<INullStatement> list =new List<INullStatement>();
        public void execute(Context context)
        {
            foreach (var statement in list)
            {
                statement.execute(context);
            }
        }

        public Block AddChild(INullStatement assignment)
        {
            list.Add(assignment);
            return this;
        }
    }



    public class Context
    {
        private Dictionary<string, MyInteger> variableLookup = new Dictionary<string, MyInteger>();
        public Context AddVariableAssignment(VariableName varName, MyInteger value)
        {
            variableLookup[varName.variableName] = value;
            return this;
        }

        public MyInteger GetValue(VariableName variableName)
        {
            MyInteger myVal;
            variableLookup.TryGetValue(variableName.variableName, out myVal);
            return myVal;

        }
    }
}
