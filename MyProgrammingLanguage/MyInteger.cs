using System;

namespace MyProgrammingLanguage
{
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
}