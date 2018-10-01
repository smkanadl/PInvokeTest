using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ManagedModel
{
    [TestFixture]
    public class CoefficientMatrixTest
    {
        [Test]
        public void Size_ConstructWithSize3_SizeIs3()
        {
            var matrix = CreateMatrixWithSize(3);
            Assert.That(matrix.Size, Is.EqualTo(3));
        }

        private static CoefficientMatrix CreateMatrixWithSize(int size, double defaultValue = 0.0)
        {
            return new CoefficientMatrix(size, defaultValue);
        }

        [Test]
        public void ConstructWithSize_ConstructWithSize3_AllElementsAreZero()
        {
            var matrix = CreateMatrixWithSize(3);
            Assert.That(matrix.Size, Is.EqualTo(3));
        }
        
        [Test]
        public void IsNullMatrix_ConstructWithSize3_IsTrue()
        {
            var matrix = CreateMatrixWithSize(3);
            Assert.That(matrix.IsNullMatrix, Is.True);
        }
        
        [Test]
        public void IsNullMatrix_ConstructWithSize3Set1Comma1To2_IsFalse()
        {
            var matrix = CreateMatrixWithSize(3);
            matrix.SetValue(1, 1, 2.0);
            
            Assert.That(matrix.IsNullMatrix, Is.False);
        }
        
        [Test]
        public void GetValue_ConstructWithSize3Set1Comma1To2_Is2()
        {
            var matrix = CreateMatrixWithSize(3);
            matrix.SetValue(1, 1, 2.0);

            Assert.That(matrix.GetValue(1,1), Is.EqualTo(2.0));
        }
        
        [Test]
        public void GetValue_ConstructWithSize3Set2Comma1To2_Is2()
        {
            var matrix = CreateMatrixWithSize(3);
            matrix.SetValue(2, 1, 2.0);
            
            Assert.That(matrix.GetValue(2, 1), Is.EqualTo(2.0));
        }
        
        [Test]
        public void SetAllRowValuesToNull_ConstructWithSize3AndInit2_Row2IsNull()
        {
            var matrix = CreateMatrixWithSize(3, 2.0);
            matrix.SetAllRowValuesToNull(2);
            
            for (int i = 0; i < matrix.Size; ++i)
            {
                Assert.That(matrix.GetValue(2, i), Is.EqualTo(0.0));
            }
        }
        
        [Test]
        public void SetAllRowValuesToNull_ConstructWithSize3AndInit2_Row0And1IsNotNull()
        {
            var matrix = CreateMatrixWithSize(3, 2.0);
            matrix.SetAllRowValuesToNull(2);
            
            for (int i = 0; i < 2; ++i)
            {
                for (int j = 0; j < matrix.Size; ++j)
                {
                    Assert.That(matrix.GetValue(i, j), Is.EqualTo(2.0));
                }
            }
        }
        
        [Test]
        public void AddToValue_Is1Add3_Is4()
        {
            var matrix = CreateMatrixWithSize(3);

            matrix.SetValue(1, 2, 1.0);
            matrix[1, 2] += 3.0;
            
            Assert.That(matrix.GetValue(1, 2), Is.EqualTo(4.0));
        }

        [Test]
        public void IndexAccessOperator_SetTo42_Is42()
        {
            var matrix = CreateMatrixWithSize(3);
            matrix[1, 2] = 42.0;
            Assert.That(matrix[1, 2], Is.EqualTo(42.0));
        }
        
        [Test]
        public void Multiplication_Size3SolutionSize2_Throws()
        {
            var matrix = CreateMatrixWithSize(3);
            var solution = new ColumnMatrix(new double[2] { 1, 2 });
            Assert.Throws<InvalidMatrixOperationException>(() => { var r = matrix * solution; });
        }

        [Test]
        public void Multiplication_IdentityMatrixSolutionSize3_ResultMatchesSolution()
        {
            var matrix = CreateMatrixWithSize(3);
            matrix[0, 0] = 1.0;
            matrix[1, 1] = 1.0;
            matrix[2, 2] = 1.0;
            var solution = new ColumnMatrix(new double[3] { 1, 2, 3 });
            var result = matrix * solution;
            Assert.That(result[0], Is.EqualTo(1.0).Within(0.01).Percent);
            Assert.That(result[1], Is.EqualTo(2.0).Within(0.01).Percent);
            Assert.That(result[2], Is.EqualTo(3.0).Within(0.01).Percent);
        }

        [Test]
        public void Multiplication_ResultMatchesSolution()
        {
            var matrix = CreateMatrixWithSize(3);
            matrix[0, 0] = 0.0;
            matrix[0, 1] = 1.0;
            matrix[0, 2] = 2.0;
            matrix[1, 0] = 3.0;
            matrix[1, 1] = -4.0;
            matrix[1, 2] = 5.0;
            matrix[2, 0] = 6.0;
            matrix[2, 1] = 7.0;
            matrix[2, 2] = -8.0;
            var solution = new ColumnMatrix(new double[3] { 1, 2, 3 });
            var result = matrix * solution;
            Assert.That(result[0], Is.EqualTo(8.0).Within(0.01).Percent);
            Assert.That(result[1], Is.EqualTo(10.0).Within(0.01).Percent);
            Assert.That(result[2], Is.EqualTo(-4.0).Within(0.01).Percent);
        }
    }
}
