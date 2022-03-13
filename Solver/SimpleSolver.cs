using System.Collections.Generic;
using System.Diagnostics;

namespace PrimeNumbersThreaded.PrimesSolver
{
    public sealed class SimpleSolver : PrimesSolver
    {
        public override int Solve(IList<int> numbers, out long elapsedMs)
        {
            var timer = new Stopwatch();
            timer.Start();

            var primesAmount = FindPrimesAmount(numbers);

            timer.Stop();

            elapsedMs = timer.ElapsedMilliseconds;

            return primesAmount;
        }
    }
}
