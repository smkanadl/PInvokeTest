using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var bisection = new ManagedModel.Bisection(x => x - 1);
            bisection.Lower = -1;
            bisection.Upper = 1;
            bisection.MaxIterations = 100;

            var root = bisection.FindRoot();

            var matrix = new ManagedModel.Sle.CoefficientMatrix(3, 1.0);
            Console.WriteLine("Matrix is: " + matrix.ToString());
        }
    }
}
