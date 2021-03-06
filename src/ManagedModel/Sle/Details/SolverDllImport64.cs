﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ManagedModel.Sle.Details
{
    internal static class SolverDllImport64
    {
        private const string dllName = "NativeModel64";

        [DllImport(dllName)]
        public static extern int Solve(IntPtr matrix, IntPtr result, IntPtr solution);
    }
}
