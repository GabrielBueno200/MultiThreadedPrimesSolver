using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PrimeNumbersThreaded.Utilities
{
    public static class Utils
    {
        public static IEnumerable<int> LoadNumbersFromCsv(string csvFileName)
        {
            var rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                            .Replace("\\bin\\Debug\\net5.0-windows", "");
            var csvPath = Path.Combine(rootPath, csvFileName);

            string line = "";
            using (var sr = File.OpenText(csvPath))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    yield return Convert.ToInt32(line);
                }
            }
        }
    }
}
