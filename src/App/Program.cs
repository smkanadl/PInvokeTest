using System;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var model = new ManagedModel.Model();
            var result = model.Run(2, 3, 4);
            Console.WriteLine("Sum is: " + result.Sum);
            Console.WriteLine("Product is: " + result.Product);

            var matrix = new ManagedModel.Sle.CoefficientMatrix(3, 1.0);
            Console.WriteLine("Matrix is: " + matrix.ToString());
        }
    }
}
