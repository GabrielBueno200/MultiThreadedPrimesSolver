using System;
using System.Collections.Generic;
using PrimeNumbersThreaded.Extensions;
using PrimeNumbersThreaded.PrimesSolver;

namespace PrimeNumbersThreaded.Tests
{
    public static class Executer
    {
        /// <summary>
        /// Run threaded solver with an increasing range based in a max threads amount value
        /// </summary>
        /// <param name="numbers">numbers list</param>
        /// <param name="maxThreadsAmount">max threads amount</param>
        /// <returns></returns>
        public static IDictionary<int, long> ExecuteFromThreadRange(IList<int> numbers, int maxThreadsAmount)
        {
            IDictionary<int, long> threadExecutions = new Dictionary<int, long>();

            var minThreadsAmount = 1;
            for (var threadAmount = minThreadsAmount; threadAmount <= maxThreadsAmount; threadAmount++)
            {
                var primesAmount = new ThreadedSolver { ThreadsAmount = threadAmount }.Solve(numbers, out var executionTime);

                Console.WriteLine($"Found {primesAmount} primes after {threadAmount} threads executed in {executionTime} ms");

                threadExecutions.Add(threadAmount, executionTime);
            }

            return threadExecutions;
        }

        /// <summary>
        /// Run the solver more times, calculating the threaded and simple executions medians
        /// </summary>
        /// <param name="numbers">numbers list</param>
        /// <param name="timesToExecute">number of executions</param>
        /// <param name="threadsAmount">threads amount</param>
        public static void RepeatSolverExecutions(IList<int> numbers, int timesToExecute = 10, int threadsAmount = 5
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

            Console.WriteLine($"Results after {timesToExecute} executions:");
            Console.WriteLine($"Average execution time without threads: ${medianSimpleSolveTime} ms");
            Console.WriteLine($"TAverage execution time with {threadsAmount} threads: ${medianThreadedSolveTime}");
        }

    }
}
