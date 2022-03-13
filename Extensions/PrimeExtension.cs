using System;

namespace PrimeNumbersThreaded
{
    public static class PrimeExtension
    {
        /// <summary>
        /// Checks if a number is prime
        /// </summary>
        /// <param name="number">the number</param>
        /// <returns>true if number is prime otherwise false</returns>
        public static bool IsPrime(this int number)
        {
            if (number <= 3) return number > 1;

            if (number % 2 == 0 || number % 3 == 0) return false;

            var count = 5;

            while (count * count <= number)
            {
                if (number % count == 0 || number % (count + 2) == 0) return false;
                count += 6;
            }

            return true;
        }
    }
}
