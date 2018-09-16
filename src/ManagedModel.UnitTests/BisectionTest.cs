using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagedModel
{
    [TestFixture]
    public class BisectionTest
    {
        Bisection bisection;

        [SetUp]
        public void SetUp()
        {
            bisection = new Bisection(x => x);
        }

        [Test]
        public void FindRoot_Is0()
        {
            bisection.Lower = -1;
            bisection.Upper = 1;
            bisection.MaxIterations = 100;

            var root = bisection.FindRoot();

            Assert.That(root, Is.EqualTo(0.0).Within(0.00001));
        }

        [Test]
        public void FindRoot_Is1()
        {
            bisection = new Bisection(x => x - 1);
            bisection.Lower = -1;
            bisection.Upper = 1;
            bisection.MaxIterations = 100;

            var root = bisection.FindRoot();

            Assert.That(root, Is.EqualTo(1.0).Within(0.00001));
        }
    }
}
