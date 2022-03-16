using System.Linq;
using System.Collections.Generic;

namespace PrimeNumbersThreaded.Extensions
{
    public static class MedianExtension
    {
        public static double Median(this IList<long> numbers)
        {
            double median;
            var sortedNumbers = numbers.OrderBy(n => n).ToList();

            int numbersAmount = numbers.Count();
            int halfIndex = numbers.Count() / 2;

            if ((numbersAmount % 2) == 0)
                median = (sortedNumbers.ElementAt(halfIndex) + sortedNumbers.ElementAt(halfIndex)) / 2;
            else
                median = sortedNumbers.ElementAt(halfIndex);

            return median;
        }
    }
}
