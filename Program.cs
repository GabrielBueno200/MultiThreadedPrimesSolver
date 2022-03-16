using System;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PrimeNumbersThreaded.Utilities;
using PrimeNumbersThreaded.Forms;

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

            ShowPrimesSolveOptions();
        }

        private static void ShowPrimesSolveOptions()
        {
            var numbers = Utils.LoadNumbersFromCsv(csvFileName: "Dataset.csv").ToList();

            Application.Run(new GraphicsOptionsForm(numbers));
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
    }
}
