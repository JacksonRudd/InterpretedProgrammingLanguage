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
        private IntReturningFunction function;
        public MyInteger execute(Context functionContext)
        {
            return function.execute(functionContext);
        }
    }

    public class IntReturningFunction : IIntegerReturningStatement
    {
        private IIntegerReturningStatement statement;

        public MyInteger execute(Context functionContext)
        {
            return statement.execute(functionContext);
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
        public void AddVariableAssignment(VariableName varName, MyInteger value)
        {
            variableLookup[varName.variableName] = value;
        }

        public MyInteger GetValue(VariableName variableName)
        {
            MyInteger myVal;
            variableLookup.TryGetValue(variableName.variableName, out myVal);
            return myVal;

        }
    }
}
