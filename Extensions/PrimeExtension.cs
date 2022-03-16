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
            if (number == 2 || number == 3)
                return true;

            if (number <= 1 || number % 2 == 0 || number % 3 == 0)
                return false;

            for (int i = 5; i * i <= number; i += 6)
            {
                if (number % i == 0 || number % (i + 2) == 0)
                    return false;
            }

            return true;
        }
    }
}
