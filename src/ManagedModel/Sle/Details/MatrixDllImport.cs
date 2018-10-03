using System;
using System.Collections.Generic;
using System.Text;

namespace ManagedModel.Sle.Details
{
    internal delegate IntPtr Create(int rows, int columns, double initialValue);
    internal delegate int Free(IntPtr ctx);
    internal delegate int Set(IntPtr ctx, int row, int column, double value);
    internal delegate int Get(IntPtr ctx, int row, int column, ref double value);

    internal static class MatrixDllImport
    {
        static MatrixDllImport()
        {
            if (Environment.Is64BitProcess)
            {
                Create = MatrixDllImport64.Create;
                Free = MatrixDllImport64.Free;
                Get = MatrixDllImport64.Get;
                Set = MatrixDllImport64.Set;
            }
            else
            {
                Create = MatrixDllImport32.Create;
                Free = MatrixDllImport32.Free;
                Get = MatrixDllImport32.Get;
                Set = MatrixDllImport32.Set;
            }
        }

        public static Create Create { get; set; }

        public static Free Free { get; set; }

        public static Get Get { get; set; }

        public static Set Set { get; set; }
    }
}
