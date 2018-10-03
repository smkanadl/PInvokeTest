namespace ManagedModel.Sle
{
    public class Solution
    {
        public Solution(ColumnMatrix values, SolveResult result)
        {
            Values = values;
            Result = result;
        }

        public ColumnMatrix Values;
        public SolveResult Result;
    }
}
