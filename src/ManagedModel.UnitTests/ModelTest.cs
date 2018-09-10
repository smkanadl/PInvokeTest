using NUnit.Framework;
using System;

namespace ManagedModel
{
    [TestFixture]
    public class ModelTest
    {
        private Model model;

        [SetUp]
        public void SetUp()
        {
            model = new Model();
        }

        [Test]
        public void Run_Sum_Is3()
        {
            Assert.That(model.Run(1, 1, 1).Sum, Is.EqualTo(3));
        }

        [Test]
        public void Run_Product_Is3()
        {
            Assert.That(model.Run(1, 1, 3).Product, Is.EqualTo(3));
        }
    }
}
