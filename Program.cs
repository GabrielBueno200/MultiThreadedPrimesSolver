using System;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PrimeNumbersThreaded.Utilities;
using PrimeNumbersThreaded.Tests;

namespace PrimeNumbersThreaded
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles(); // Enable Window Forms visual styles
            Application.SetCompatibleTextRenderingDefault(false);
            AllocConsole(); // Enable console

            SolvePrimes();
        }

        private static void SolvePrimes()
        {
            // dataset numbers
            var numbers = Utils.LoadNumbersFromCsv(csvFileName: "Dataset.csv").ToList();

            Executer.ExecuteFromThreadRange(numbers, 10);
            
            /*
            Console.WriteLine($"Analyzing {numbers.Count} numbers");

            Executer.ExecuteSimpleSolution(numbers);
            Executer.ExecuteThreadedSolution(numbers);
            */

            Console.ReadLine();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
    }
}
