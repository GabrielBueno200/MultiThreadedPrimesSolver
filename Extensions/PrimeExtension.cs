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
            if (number == 2) return true;
            
            if (number < 2 || number % 2 == 0) return false;
            
            for (var i = 3; i <= Math.Sqrt(number); i += 2)
                if (number % i == 0) return false;
            
            return true;
        }
    }
}
