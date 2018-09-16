using System;
using System.Runtime.InteropServices;

namespace ManagedModel
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct InputData
    {
        public double V1;
        public double V2;
        public double V3;
        public bool ShouldThrow;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct ResultData
    {
        public double Sum;
        public double Product;
    }

    internal static class ModelImport64
    {
        [DllImport("NativeModel64")]
        public static extern int RunModel(InputData data, ref ResultData result);
    }

    internal static class ModelImport32
    {
        [DllImport("NativeModel32")]
        public static extern int RunModel(InputData data, ref ResultData result);
    }

    public class Result
    {
        public double Sum { get; set; }

        public double Product { get; set; }
    }

    public class Model
    {
        public bool ShouldThrow { get; set; }

        public Result Run(double v1, double v2, double v3)
        {
            var data = new InputData()
            {
                V1 = v1,
                V2 = v2,
                V3 = v3,
                ShouldThrow = ShouldThrow
            };

            var result = new ResultData();

            if (Environment.Is64BitProcess)
            {
                var e = ModelImport64.RunModel(data, ref result);
                if (e != 0)
                {
                    throw new Exception("Boom by error code: " + e);
                }
            }
            else
            {
                var e = ModelImport32.RunModel(data, ref result);
                if (e != 0)
                {
                    throw new Exception("Boom by error code: " + e);
                }
            }
            return From(result);
        }

        private Result From(ResultData result)
        {
            return new Result
            {
                Product = result.Product,
                Sum = result.Sum
            };
        }
    }
}
