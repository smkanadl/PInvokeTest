using System;
using System.Runtime.InteropServices;

namespace ManagedModel
{
    internal static class MatrixDllImport64
    {
        [DllImport("NativeModel64")]
        public static extern IntPtr Create(int rows, int columns, double initialValue);

        [DllImport("NativeModel64")]
        public static extern int Free(IntPtr ctx);

        [DllImport("NativeModel64")]
        public static extern int Set(IntPtr ctx, int row, int column, double value);

        [DllImport("NativeModel64")]
        public static extern int Get(IntPtr ctx, int row, int column, ref double value);
    }
}
