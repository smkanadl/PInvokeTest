using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ManagedModel.Sle.Details
{
    internal static class SolverDllImport32
    {
        private const string dllName = "NativeModel32";

        [DllImport(dllName)]
        public static extern int Solve(IntPtr matrix, IntPtr result, IntPtr solution);
    }
}
