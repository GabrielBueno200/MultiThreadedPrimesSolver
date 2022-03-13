using System.Linq;
using PrimeNumbersThreaded.Utilities;
using System;
using System.Windows.Forms;

namespace PrimeNumbersThreaded
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var numbers = Utils.LoadNumbersFromCsv(csvFileName: "Dataset.csv").ToList();

            Application.EnableVisualStyles();
            //solver.Test(new List<int> { 1, 2, 3, 4, 11 });

        }
    }
}
