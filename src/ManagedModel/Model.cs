﻿using System;
using System.Runtime.InteropServices;

namespace ManagedModel
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct InputData
    {
        public double V1;
        public double V2;
        public double V3;
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
        public static extern ResultData RunModel(InputData data);
    }

    internal static class ModelImport32
    {
        [DllImport("NativeModel32")]
        public static extern ResultData RunModel(InputData data);
    }

    public class Result
    {
        public double Sum { get; set; }

        public double Product { get; set; }
    }

    public class Model
    {
        public Result Run(double v1, double v2, double v3)
        {
            var data = new InputData()
            {
                V1 = v1,
                V2 = v2,
                V3 = v3
            };

            if (Environment.Is64BitProcess)
            {
                return From(ModelImport64.RunModel(data));
            }
            else
            {
                return From(ModelImport32.RunModel(data));
            }
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
