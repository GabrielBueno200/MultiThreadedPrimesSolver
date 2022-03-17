using System;
using System.Linq;
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
        /// <returns>Dictionary containing the threads amount as key and its respective execution time as value</returns>
        public static IDictionary<int, long> ExecuteFromThreadRange(IList<int> numbers, int maxThreadsAmount, int minThreadsAmount = 1)
        {
            IDictionary<int, long> threadExecutions = new Dictionary<int, long>();

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
        public static (int, double, double) RepeatThreadedSolverExecutions(IList<int> numbers, int timesToExecute, int threadAmount, double? simpleSolveMedian = null)
        {
            var executions = new List<long>();
            var speedUps = new List<double>();

            Console.WriteLine($"Executions with {threadAmount} threads");
            Console.WriteLine($"========================================");

            for (var executionIndex = 1; executionIndex <= timesToExecute; executionIndex++)
            {
                Console.Write($"Execution {executionIndex} - ");
                if (threadAmount > 1)
                {
                    var primesAmount = new ThreadedSolver { ThreadsAmount = threadAmount }.Solve(numbers, out var threadedSolverExecutionTime);
                    executions.Add(threadedSolverExecutionTime);

                    var speedUp = simpleSolveMedian / threadedSolverExecutionTime;
                    speedUps.Add(speedUp ?? 0);

                    Console.WriteLine($"{primesAmount} primes found");
                    Console.WriteLine($"SpeedUp: {Math.Round(speedUp ?? 0, 2)}");
                    Console.WriteLine($"Finished in {threadedSolverExecutionTime} ms");
                }
                else
                {
                    var primesAmount = new SimpleSolver().Solve(numbers, out var simpleSolverExecutionTime);
                    executions.Add(simpleSolverExecutionTime);

                    Console.WriteLine($"{primesAmount} primes found");
                    Console.WriteLine($"Finished in {simpleSolverExecutionTime} ms");
                }

                Console.WriteLine();
            }

            var median = executions.Median();
            var speedUpError = speedUps.StandardDeviation();

            Console.Write(threadAmount > 1 ? $"SpeedUp error: {speedUpError}\n" : string.Empty);
            Console.WriteLine($"Execution time median: {median} ms\n");

            return (threadAmount, median, speedUpError);
        }

        public static (int, double, double, double) ExecuteSerialFraction(IList<(int, double, double)> threadsSpeedUpsTimes)
        {
            var bestSpeedUpExecution = threadsSpeedUpsTimes.Where(x => x.Item3 == threadsSpeedUpsTimes.Max(x => x.Item3)).FirstOrDefault();
            var (threadsAmount, time, speedUp) = bestSpeedUpExecution;

            var serialFrac = (threadsAmount - speedUp) / (speedUp * (threadsAmount - 1));

            return (threadsAmount, time, speedUp, serialFrac);
        }
    }
}
