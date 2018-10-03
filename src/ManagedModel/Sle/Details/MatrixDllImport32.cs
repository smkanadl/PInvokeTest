using System;
using System.Runtime.InteropServices;

namespace ManagedModel.Sle.Details
{
    internal static class MatrixDllImport32
    {
        private const string dllName = "NativeModel64";

        [DllImport(dllName)]
        public static extern IntPtr Create(int rows, int columns, double initialValue);

        [DllImport(dllName)]
        public static extern int Free(IntPtr ctx);

        [DllImport(dllName)]
        public static extern int Set(IntPtr ctx, int row, int column, double value);

        [DllImport(dllName)]
        public static extern int Get(IntPtr ctx, int row, int column, ref double value);
    }
}
