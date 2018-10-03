using System;
using System.Collections;
using System.Collections.Generic;

namespace ManagedModel.Sle
{
    public class ColumnMatrix : IEnumerable<double>, IDisposable
    {
        public ColumnMatrix(int size, double initialValue)
        {
            Size = size;
            Handle = MatrixDllImport64.Create(size, 1, initialValue);
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
            MatrixDllImport64.Free(Handle);
        }

        public double GetValue(int row)
        {
            var value = 0.0;
            MatrixDllImport64.Get(Handle, row, 0, ref value);
            return value;
        }

        public void SetValue(int row, double value)
        {
            MatrixDllImport64.Set(Handle, row, 0, value);
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
        
        internal IntPtr Handle { get; }
    }
}
