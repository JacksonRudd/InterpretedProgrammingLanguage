using System.Collections.Generic;

namespace MyProgrammingLanguage
{
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
}