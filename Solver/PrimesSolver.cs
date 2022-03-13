using System.Linq;
using System.Collections.Generic;

namespace PrimeNumbersThreaded.PrimesSolver
{
    public abstract class PrimesSolver
    {
        protected int FindPrimesAmount(IList<int> numbers) => numbers.Where(n => n.IsPrime()).Count();

        protected abstract int Solve(IList<int> numbers, out int executionTime);                                                                                                                                                            
    }
}
