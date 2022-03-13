using System.Linq;
using System.Collections.Generic;

namespace PrimeNumbersThreaded.Extensions
{
    public static class RangeExtension
    {
        /// <summary>
        /// Returns a list of integer elements separed by a specified range
        /// </summary>
        /// <param name="range"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static IEnumerable<int> RangeWithStep(this IEnumerable<int> range, int start, int end, int step)
        {
            var counter = start;
            while (counter <= end)
            {
                yield return (int) counter;
                counter += step;
            }
        }
    }
}
