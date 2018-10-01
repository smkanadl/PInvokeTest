using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

namespace ManagedModel
{
    public class InvalidMatrixOperationException : Exception
    {
        public InvalidMatrixOperationException()
        {
        }

        public InvalidMatrixOperationException(string message)
            : base(message)
        {
        }

        public InvalidMatrixOperationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected InvalidMatrixOperationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
    
    internal static class CoefficientMatrixImport64
    {
        [DllImport("NativeModel64")]
        public static extern IntPtr Create(int rows, int columns, double initialValue);

        [DllImport("NativeModel64")]
        public static extern int Free(IntPtr ctx);

        [DllImport("NativeModel64")]
        public static extern int Set(IntPtr ctx, int row, int column, double value);

        [DllImport("NativeModel64")]
        public static extern int Get(IntPtr ctx, int row, int column, ref double value);

        [DllImport("NativeModel64")]
        public static extern int Solve(IntPtr matrix, IntPtr result, IntPtr solution);
    }

    public class ColumnMatrix : IEnumerable<double>, IDisposable
    {
        internal readonly IntPtr impl;

        public ColumnMatrix(int size, double initialValue)
        {
            Size = size;
            impl = CoefficientMatrixImport64.Create(size, 1, initialValue);
        }

        public ColumnMatrix(IReadOnlyList<double> values)
            : this(values.Count, 0.0)
        {
            for (int i = 0; i < Size; ++i)
            {
                this[i] = values[i];
            }
        }

        public void Dispose()
        {
            CoefficientMatrixImport64.Free(impl);
        }

        public double GetValue(int row)
        {
            var value = 0.0;
            CoefficientMatrixImport64.Get(impl, row, 0, ref value);
            return value;
        }

        public void SetValue(int row, double value)
        {
            CoefficientMatrixImport64.Set(impl, row, 0, value);
        }

        public IEnumerator<double> GetEnumerator()
        {
            return new Iterator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public double this[int row]
        {
            get => GetValue(row);
            set => SetValue(row, value);
        }

        public int Size { get; }

        private class Iterator : IEnumerator<double>
        {
            int index;
            ColumnMatrix data;

            public Iterator(ColumnMatrix data)
            {
                this.data = data;
                index = -1;
            }

            public void Dispose()
            {
            }

            public double Current => data[index];
            
            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (++index == data.Size)
                    return false;
                return true;
            }

            public void Reset()
            {
                index = -1;
            }
        };
    }

    public class CoefficientMatrix : IDisposable
    {
        internal readonly IntPtr impl;

        public CoefficientMatrix(int size, double initialValue)
        {
            this.Size = size;
            impl = CoefficientMatrixImport64.Create(size, size, initialValue);
        }

        public void Dispose()
        {
            CoefficientMatrixImport64.Free(impl);
        }

        public double GetValue(int row, int column)
        {
            var value = 0.0;
            CoefficientMatrixImport64.Get(impl, row, column, ref value);
            return value;
        }

        public void SetValue(int row, int column, double value)
        {
            CoefficientMatrixImport64.Set(impl, row, column, value);
        }

        public double this[int row, int column]
        {
            get => GetValue(row, column);
            set => SetValue(row, column, value);
        }

        public int Size { get; }

        public bool IsNullMatrix
        {
            get
            {
                for (var i = 0; i < Size; ++i)
                {
                    for (var j = 0; j < Size; ++j)
                    {
                        if (GetValue(i, j) != 0.0)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }

        public void SetAllRowValuesToNull(int row)
        {
            for (var i = 0; i < Size; ++i)
            {
                SetValue(row, i, 0.0);
            }
        }

        /// <summary>
        ///  Matrix times Solution yields a result.
        /// </summary>
        public static ColumnMatrix operator *(CoefficientMatrix matrix, ColumnMatrix columnMatrix)
        {
            // TODO: Move to Eigen
            if (matrix.Size != columnMatrix.Size)
                throw new InvalidMatrixOperationException();

            var result = new ColumnMatrix(matrix.Size, 0.0);

            for (var row = 0; row < matrix.Size; ++row)
            {
                var value = 0.0;
                for (var column = 0; column < matrix.Size; ++column)
                {
                    value += matrix[row, column] * columnMatrix[column];
                }
                result[row] = value;
            }

            return result;
        }

        public override string ToString()
        {
            var result = "";

            for (var i = 0; i < Size; ++i)
            {
                for (var j = 0; j < Size; ++j)
                {
                    result += GetValue(i, j).ToString();
                    result += ";";
                }
                result += "/";
            }
                
            return result;
        }
    }

    public enum SolveResult
    {
        IsOk,
        IsSingular,
        IsFailed,
        HasUnkownError
    }

    public class Solution
    {
        public Solution(ColumnMatrix values, SolveResult result)
        {
            Values = values;
            Result = result;
        }

        public ColumnMatrix Values;
        public SolveResult Result;
    }

    public class SystemOfLinearEquationsSolver
    {
        public SystemOfLinearEquationsSolver(CoefficientMatrix A, ColumnMatrix b)
        {
            CoefficientMatrix = A;
            RightHandSide = b;
            Solution = null;
        }

        /// <summary>
        ///  Get the calculated solution.
        /// </summary>
        /// <returns></returns>
        public Solution GetSolution()
        {
            Debug.Assert(Solution != null,
                "No solution available. Call Solve() before!");

            return Solution;
        }

        public Solution Solve()
        {
            var columnMatrix = new ColumnMatrix(RightHandSide.Size, 0.0);
            var result = CoefficientMatrixImport64.Solve(CoefficientMatrix.impl, RightHandSide.impl, columnMatrix.impl);
            Solution = new Solution(columnMatrix, (SolveResult)result);
            return Solution;
        }

        /// <summary>
        ///  Data for the sle solver A_ * x_ = b_
        /// </summary>
        private CoefficientMatrix CoefficientMatrix;
        private ColumnMatrix RightHandSide;
        private Solution Solution;
    }
}
