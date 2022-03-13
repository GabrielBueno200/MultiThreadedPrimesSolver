using System.Linq;
using System.Collections.Generic;

namespace PrimeNumbersThreaded.PrimesSolver
{
    public abstract class PrimesSolver
    {
        /// <summary>
        /// Gets the amount of prime numbers found
        /// </summary>
        /// <param name="numbers">the list of numbers</param>
        /// <returns></returns>
        protected int FindPrimesAmount(IList<int> numbers) => numbers.Where(n => n.IsPrime()).Count();

        /// <summary>
        /// Solves the prime numbers
        /// </summary>
        /// <param name="numbers">the list of numbers</param>
        /// <param name="elapsedMs">execution time employed</param>
        /// <returns>the amount of prime numbers</returns>
        public abstract int Solve(IList<int> numbers, out long elapsedMs);                                                                                                                                                            
    }
}
