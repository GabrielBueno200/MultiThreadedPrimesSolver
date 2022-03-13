using System.Linq;
using System.Collections.Generic;
using System;
using PrimeNumbersThreaded.Extensions;

namespace PrimeNumbersThreaded.PrimesSolver
{
    public class ThreadedSolver : PrimesSolver
    {
        public int ThreadsAmount { get; set; } = 2;

        private IList<int> GetTheadsIntervals(IList<int> numbers)
        {
            var numbersAmount = numbers.Count;
            var step = (int) Math.Ceiling( (decimal) numbersAmount / ThreadsAmount);
            var range = new List<int>().RangeWithStep(start: 0, end: numbersAmount + step, step);

            return range.ToList();
        }

        protected override int Solve(IList<int> numbers, out int executionTime)
        {
            var primesAmount = 0;
            executionTime = 0;

            return 0;
        }
    }
}
