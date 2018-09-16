using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ManagedModel
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Input
    {
        public double Lower;
        public double Upper;
        public int MaxIterations;
        public IntPtr f;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct RootResult
    {
        public double Root;
    }

    internal static class BisectionImport64
    {
        [DllImport("NativeModel64")]
        public static extern int RunBisection(Input data, ref RootResult result);
    }

    internal static class BisectionImport32
    {
        [DllImport("NativeModel32")]
        public static extern int RunBisection(Input data, ref RootResult result);
    }

    public class Bisection
    {
        private Input input;
        private delegate double Function(double value);

        public Bisection(Func<double, double> function)
        {
            var d = new Function(function);
            GC.KeepAlive(d);
            var ptr = Marshal.GetFunctionPointerForDelegate(d);
            input = new Input()
            {
                f = ptr
            };
        }

        public double Lower
        {
            get => input.Lower;
            set => input.Lower = value;
        }

        public double Upper
        {
            get => input.Upper;
            set => input.Upper = value;
        }

        public int MaxIterations
        {
            get => input.MaxIterations;
            set => input.MaxIterations = value;
        }

        public double FindRoot()
        {
            var result = new RootResult();
            if (Environment.Is64BitProcess)
            {
                var e = BisectionImport64.RunBisection(input, ref result);
                if (e != 0)
                {
                    throw new Exception("Boom by error code: " + e);
                }
            }
            else
            {
                var e = BisectionImport32.RunBisection(input, ref result);
                if (e != 0)
                {
                    throw new Exception("Boom by error code: " + e);
                }
            }
            return result.Root;
        }
    }
}
