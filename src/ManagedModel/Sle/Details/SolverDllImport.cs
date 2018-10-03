using System;
using System.Collections.Generic;
using System.Text;

namespace ManagedModel.Sle.Details
{
    internal delegate int Solve(IntPtr matrix, IntPtr result, IntPtr solution);

    internal static class SolverDllImport
    {
        static SolverDllImport()
        {
            if (Environment.Is64BitProcess)
            {
                Solve = SolverDllImport64.Solve;
            }
            else
            {
                Solve = SolverDllImport32.Solve;
            }
        }

        public static Solve Solve { get; set; }
    }
}
