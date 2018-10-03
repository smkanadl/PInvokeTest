using System;
using System.Text;

namespace ManagedModel.Sle
{
    public class CoefficientMatrix : IDisposable
    {
        public CoefficientMatrix(int size, double initialValue)
        {
            Size = size;
            Handle = MatrixDllImport64.Create(size, size, initialValue);
        }

        public void Dispose()
        {
            MatrixDllImport64.Free(Handle);
        }

        public double GetValue(int row, int column)
        {
            var value = 0.0;
            MatrixDllImport64.Get(Handle, row, column, ref value);
            return value;
        }

        public void SetValue(int row, int column, double value)
        {
            MatrixDllImport64.Set(Handle, row, column, value);
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

        internal IntPtr Handle { get; }
    }
}
