using System;
using System.Collections.Generic;
using PrimeNumbersThreaded.Extensions;
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

            var minThreadsAmount = 1;
            for (var threadAmount = minThreadsAmount; threadAmount <= maxThreadsAmount; threadAmount++)
            {
                var primesAmount = new ThreadedSolver { ThreadsAmount = threadAmount }.Solve(numbers, out var executionTime);

                Console.WriteLine($"Found {primesAmount} primes after {threadAmount} threads executed in {executionTime} ms");

                threadExecutions.Add(threadAmount, executionTime);
            }

            return threadExecutions;
        }

    }
}
