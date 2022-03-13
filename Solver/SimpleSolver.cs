using System.Collections.Generic;

namespace PrimeNumbersThreaded.PrimesSolver
{
    public class SimpleSolver : PrimesSolver
    {
        protected override int Solve(IList<int> numbers, out int executionTime)
        {
            var primesAmount = FindPrimesAmount(numbers);
            executionTime = 0;

            return primesAmount;
        }
    }
}
