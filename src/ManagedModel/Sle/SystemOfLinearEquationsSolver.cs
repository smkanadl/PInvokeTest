using ManagedModel.Sle.Details;
using System.Diagnostics;

namespace ManagedModel.Sle
{
    public class SystemOfLinearEquationsSolver
    {
        public SystemOfLinearEquationsSolver(CoefficientMatrix A, ColumnMatrix b)
        {
            CoefficientMatrix = A;
            RightHandSide = b;
            Solution = null;
        }

        /// <summary>
        ///  Get the calculated solution.
        /// </summary>
        /// <returns></returns>
        public Solution GetSolution()
        {
            Debug.Assert(Solution != null,
                "No solution available. Call Solve() before!");

            return Solution;
        }

        public Solution Solve()
        {
            var columnMatrix = new ColumnMatrix(RightHandSide.Size, 0.0);
            var result = SolverDllImport.Solve(CoefficientMatrix.Handle, RightHandSide.Handle, columnMatrix.Handle);
            Solution = new Solution(columnMatrix, (SolveResult)result);
            return Solution;
        }

        /// <summary>
        ///  Data for the sle solver A_ * x_ = b_
        /// </summary>
        private CoefficientMatrix CoefficientMatrix;
        private ColumnMatrix RightHandSide;
        private Solution Solution;
    }
}
