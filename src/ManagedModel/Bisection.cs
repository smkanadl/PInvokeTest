using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ManagedModel
{
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate double Function(double value);

    [StructLayout(LayoutKind.Sequential)]
    internal struct Input
    {
        public double Lower;
        public double Upper;
        public int MaxIterations;
        public Function f;
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

        public Bisection(Func<double, double> function)
        {
            input = new Input()
            {
                f = new Function(function)
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
            GC.KeepAlive(input);
            return result.Root;
        }
    }
}
