using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ManagedModel.Sle
{
    [TestFixture]
    class ColumnMatrixTest
    {
        [Test]
        public void InitWith42_AllValuesAre42()
        {
            var matrix = new ColumnMatrix(10, 42.0);
            Assert.That(matrix.All(value => value == 42.0));
            Assert.That(matrix.Count(), Is.EqualTo(10));
        }

        [Test]
        public void SetValueTo42_ValueIs42()
        {
            var matrix = new ColumnMatrix(10, 0.0);
            matrix[3] = 42.0;
            Assert.That(matrix[3], Is.EqualTo(42.0));
        }
    }
}
