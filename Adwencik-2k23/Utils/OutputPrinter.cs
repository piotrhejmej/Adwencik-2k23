using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adwencik_2k23.Utils
{
    internal static class OutputPrinter
    {
        public static void PrintToFile(string[] output, string fileName)
        {
            using FileStream fs = File.Open(fileName, FileMode.OpenOrCreate);
            using StreamWriter sw = new(fs);
            foreach (var line in output)
            {
                sw.WriteLine(line);
            }
            sw.Close();
        }
    }
}
