using System.Linq;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Diagnostics;

namespace PrimeNumbersThreaded.PrimesSolver
{
    public sealed class ThreadedSolver : PrimesSolver
    {
        public int ThreadsAmount { get; set; }

        private List<Thread> Threads { get; set; } = new List<Thread>(); 

        /// <inheritdoc/>
        public override int Solve(IList<int> numbers, out long elapsedMs)
        {
            var timer = new Stopwatch();
            timer.Start();

            #region solving with threads
            var numbersAmount = numbers.Count;
            var primesAmountInIntervals = new List<int>();
            
            var step = Convert.ToInt32(numbersAmount / ThreadsAmount);

            for (int i = 0, intervalBegin = 0; i < ThreadsAmount; i++, intervalBegin += step)
            {
                var willMissNumbers = intervalBegin + step < numbersAmount && i + 1 >= ThreadsAmount;
                if (willMissNumbers) step = numbersAmount - intervalBegin;

                var intervalNumbers = numbers.ToList().GetRange(intervalBegin, step);

                var thread = new Thread(() => primesAmountInIntervals.Add(FindPrimesAmount(intervalNumbers)));
                thread.Start();
                Threads.Add(thread);
            }

            Threads.ForEach(thread => thread.Join()); // wait all threads finish

            var totalPrimesAmount = primesAmountInIntervals.Sum();
            #endregion

            timer.Stop();

            elapsedMs = timer.ElapsedMilliseconds;
            return totalPrimesAmount;
        }
    }
}
