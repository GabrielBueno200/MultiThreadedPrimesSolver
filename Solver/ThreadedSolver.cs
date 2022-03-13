using System.Linq;
using System.Collections.Generic;
using System;
using PrimeNumbersThreaded.Extensions;
using System.Threading;

namespace PrimeNumbersThreaded.PrimesSolver
{
    public sealed class ThreadedSolver : PrimesSolver
    {
        public int ThreadsAmount { get; set; };

        /// <summary>
        /// Splits the intervals of numbers that will be solved by each thread, 
        /// which are separated two by two in the returned list 
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        private IList<int> GetThreadsIntervals(IList<int> numbers)
        {
            var numbersAmount = numbers.Count;
            var step = (int) Math.Ceiling( (decimal) numbersAmount / ThreadsAmount);
            var range = new List<int>().RangeWithStep(start: 0, end: numbersAmount + step, step);

            return range.ToList();
        }

        /// <inheritdoc/>
        protected override int Solve(IList<int> numbers, out int executionTime)
        {
            var primesAmount = 0;
            executionTime = 0;

            var threadsIntervals = GetThreadsIntervals(numbers);

            for(var i = 0; i < ThreadsAmount; i++)
            {
                var intervalBegin = threadsIntervals.ElementAt(i);
                var intervalEnd = threadsIntervals.ElementAt(i + 1);
                var intervalNumbers = numbers.ToList().GetRange(intervalBegin, intervalEnd);
                
                var thread = new Thread(() => primesAmount += FindPrimesAmount(intervalNumbers));
                thread.Start();
            }

            return 0;
        }
    }
}
