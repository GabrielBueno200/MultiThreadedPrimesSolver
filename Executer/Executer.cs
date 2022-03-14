using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PrimeNumbersThreaded.Extensions;
using PrimeNumbersThreaded.Graphics;
using PrimeNumbersThreaded.PrimesSolver;

namespace PrimeNumbersThreaded.Tests
{
    public static class Executer
    {
        public static void RepeatSolverExecutions(IList<int> numbers, 
            int timesToExecute = 10, 
            int threadsAmount = 5
        )
        {
            IList<long> SimpleExecutions = new List<long>();
            IList<long> ThreadedExecutions = new List<long>();

            for (var executionIndex = 0; executionIndex < timesToExecute; executionIndex++)
            {
                new SimpleSolver().Solve(numbers, out var simpleSolverExecutionTime);
                new ThreadedSolver { ThreadsAmount = threadsAmount }.Solve(numbers, out var threadedSolverExecutionTime);

                SimpleExecutions.Add(simpleSolverExecutionTime);
                ThreadedExecutions.Add(threadedSolverExecutionTime);
            }

            var medianSimpleSolveTime = SimpleExecutions.Median();
            var medianThreadedSolveTime = ThreadedExecutions.Median();

            Console.WriteLine($"Resultados após {timesToExecute} execuções:");
            Console.WriteLine($"Tempo médio de execução sem threads: ${medianSimpleSolveTime}");
            Console.WriteLine($"Tempo médio de execução com {threadsAmount} threads: ${medianThreadedSolveTime}");
        }

        
        public static IDictionary<int, long> ExecuteFromThreadRange(IList<int> numbers, int maxThreadsAmount)
        {
            IDictionary<int, long> threadExecutions = new Dictionary<int, long>();

            var minThreadAsmount = 1;
            for (var threadAmount = minThreadAsmount; threadAmount <= maxThreadsAmount; threadAmount++)
            {
                var primesAmount = new ThreadedSolver { ThreadsAmount = threadAmount }.Solve(numbers, out var executionTime);

                Console.WriteLine($"Found {primesAmount} primes after {threadAmount} threads executed in {executionTime} ms");

                threadExecutions.Add(threadAmount, executionTime);
            }

            Application.Run(new ThreadByTimeGraphic(threadExecutions));

            return threadExecutions;
        }

        public static void ExecuteSimpleSolution(IList<int> numbers)
        {
            var simpleSolver = new SimpleSolver();
            var primeNumbers = simpleSolver.Solve(numbers, out var simpleExecutionTime);

            Console.WriteLine($"Found {primeNumbers} primes in {simpleExecutionTime} ms without threads");
        }

        public static void ExecuteThreadedSolution(IList<int> numbers, int threadsAmount = 1)
        {
            var threadedSolver = new ThreadedSolver { ThreadsAmount = threadsAmount };
            var primeNumbersThreaded = threadedSolver.Solve(numbers, out var threadedExecutionTime);

            Console.WriteLine($"Found {primeNumbersThreaded} primes in {threadedExecutionTime} ms with {threadsAmount} threads");
        }

    }
}
