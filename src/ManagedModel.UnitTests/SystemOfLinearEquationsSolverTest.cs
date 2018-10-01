using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ManagedModel
{
    public class SystemOfLinearEquationsSolverTestExamples
    {
        private static CoefficientMatrix CreateMatrixWithSize(int size, double defaultValue = 0.0)
        {
            return new CoefficientMatrix(size, defaultValue);
        }

        public static SystemOfLinearEquationsSolver Solver_2Times2_Example1()
        {
            var matrix = CreateMatrixWithSize(2);
            matrix.SetValue(0, 0, +1.0);
            matrix.SetValue(0, 1, +1.0);
            matrix.SetValue(1, 0, +1.0);
            matrix.SetValue(1, 1, -4.0);

            var result = new ColumnMatrix(2, 0.0)
            {
                [0] = 62.0,
                [1] = -18.0
            };
            var solver = new SystemOfLinearEquationsSolver(
                matrix, result);
            
            return solver;
        }
        
        public static SystemOfLinearEquationsSolver Solver_2Times2_Example2()
        {
            var matrix = CreateMatrixWithSize(2);
            matrix[0, 0] = 1.0;
            matrix[0, 1] = 0.0;
            matrix[1, 0] = 0.0;
            matrix[1, 1] = 1.0;

            var result = new ColumnMatrix(2, 0.0)
            {
                [0] = 10.0,
                [1] = 20.0
            };
            var solver = new SystemOfLinearEquationsSolver(
                matrix, result);
            
            return solver;
        }
        
        public static SystemOfLinearEquationsSolver Solver_2Times2_Example3()
        {
            var matrix = CreateMatrixWithSize(2);
            matrix.SetValue(0, 0, 0.0);
            matrix.SetValue(0, 1, 0.0);
            matrix.SetValue(1, 0, 0.0);
            matrix.SetValue(1, 1, 0.0);

            var result = new ColumnMatrix(2, 0.0)
            {
                [0] = 10.0,
                [1] = 20.0
            };
            var solver = new SystemOfLinearEquationsSolver(
                matrix, result);
            
            return solver;
        }
    }
    
    [TestFixture]
    public class SystemOfLinearEquationsSolverTest
    {
        
        [Test]
        public void Solve_2Times2_Example1()
        {
            var solver =
                SystemOfLinearEquationsSolverTestExamples.Solver_2Times2_Example1();

            var solution = solver.Solve();

            Assert.That(solution.Values[0], Is.EqualTo(46.0).Within(0.01).Percent);
            Assert.That(solution.Values[1], Is.EqualTo(16.0).Within(0.01).Percent);
        }
        
        [Test]
        public void Validation_2Times2_Example1()
        {
            var solver =
                SystemOfLinearEquationsSolverTestExamples.Solver_2Times2_Example1();

            var solution = solver.Solve();
            Assert.That(solution.Result, Is.EqualTo(
                SolveResult.IsOk));
        }
        
        [Test]
        public void Solve_2Times2_Example2()
        {
            var solver =
                SystemOfLinearEquationsSolverTestExamples.Solver_2Times2_Example2();

            var solution = solver.Solve();

            Assert.That(solution.Values[0], Is.EqualTo(10.0).Within(0.01).Percent);
            Assert.That(solution.Values[1], Is.EqualTo(20.0).Within(0.01).Percent);
        }
        
        [Test]
        public void Validate_2Times2Example2_IsOk()
        {
            var solver =
                SystemOfLinearEquationsSolverTestExamples.Solver_2Times2_Example2();

            var solution = solver.Solve();
            Assert.That(solution.Result, Is.EqualTo(
                SolveResult.IsOk));
        }
        
        [Test]
        public void Solve_2Times2_Example3_IsSingular()
        {
            var solver =
                SystemOfLinearEquationsSolverTestExamples.Solver_2Times2_Example3();
            
            solver.Solve();
            var solution = solver.GetSolution();

            Assert.That(solution.Values[0], Is.EqualTo(0.0).Within(0.01).Percent);
            Assert.That(solution.Values[1], Is.EqualTo(0.0).Within(0.01).Percent);
        }
        
        [Test]
        public void Validate_2Times2_Example3_IsSingular()
        {
            var solver =
                SystemOfLinearEquationsSolverTestExamples.Solver_2Times2_Example3();

            var solution = solver.Solve();
            Assert.That(solution.Result, Is.EqualTo(
                SolveResult.IsSingular));
        }
    }
}
