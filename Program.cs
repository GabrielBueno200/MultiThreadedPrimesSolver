using System.Linq;
using PrimeNumbersThreaded.Utilities;
using System;
using System.Windows.Forms;
using PrimeNumbersThreaded.PrimesSolver;
using System.Runtime.InteropServices;

namespace PrimeNumbersThreaded
{
    class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles(); // Enable Window Forms visual styles
            AllocConsole(); // Enable console

            SolvePrimes();
        }

        private static void SolvePrimes()
        {
            // dataset numbers
            var numbers = Utils.LoadNumbersFromCsv(csvFileName: "Dataset2.csv").ToList();

            Console.WriteLine($"Analyzing {numbers.Count} numbers");

            #region Simple solving
            var simpleSolver = new SimpleSolver();
            var primeNumbers = simpleSolver.Solve(numbers, out var simpleExecutionTime);

            Console.WriteLine($"Found {primeNumbers} primes in {simpleExecutionTime} ms without threads");
            #endregion

            #region Threaded solving
            var threadedSolver = new ThreadedSolver { ThreadsAmount = 5 };
            var primeNumbersThreaded = threadedSolver.Solve(numbers, out var threadedExecutionTime);

            Console.WriteLine($"Found {primeNumbersThreaded} primes in {threadedExecutionTime} ms with threads");
            #endregion

            Console.ReadLine();
        }
    }
}
