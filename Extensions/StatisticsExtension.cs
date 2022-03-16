using System.Linq;
using System.Collections.Generic;
using System;

namespace PrimeNumbersThreaded.Extensions
{
    public static class StatisticsExtension
    {
        /// <summary>
        /// Calculate the median of a long list dataset
        /// </summary>
        /// <param name="numbers">the dataset</param>
        /// <returns>median</returns>
        public static double Median(this IEnumerable<long> numbers)
        {
            double median;

            int numbersAmount = numbers.Count();
            int halfIndex = numbersAmount / 2;

            var sortedNumbers = numbers.OrderBy(n => n).ToList();

            if (numbersAmount % 2 == 0)
                median = (sortedNumbers.ElementAt(halfIndex) + sortedNumbers.ElementAt(halfIndex)) / 2;
            else
                median = sortedNumbers.ElementAt(halfIndex);

            return median;
        }

        /// <summary>
        /// Calculate the standard deviation of a double list dataset
        /// </summary>
        /// <param name="numbers">the dataset</param>
        /// <returns>standart deviation</returns>
        public static double StandardDeviation(this IEnumerable<double> numbers)
        {
            double standardDeviation = 0;
            int numbersAmount = numbers.Count();

            if (numbersAmount > 1)
            {
                //Compute the Average
                var average = numbers.Average();

                //Perform the Sum of (value-avg)^2
                var sum = numbers.Sum(number => (number - average) * (number - average));

                //Put it all together
                standardDeviation = Math.Sqrt(sum / numbersAmount);
            }

            return standardDeviation;
        }
    }
}
