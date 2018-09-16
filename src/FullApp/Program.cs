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
            var bisection = new ManagedModel.Bisection(x => x);
            bisection.Lower = -1;
            bisection.Upper = 1;
            bisection.MaxIterations = 100;

            var root = bisection.FindRoot();
        }
    }
}
