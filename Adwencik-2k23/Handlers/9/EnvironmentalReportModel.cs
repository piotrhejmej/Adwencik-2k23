using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adwencik_2k23.Handlers._9
{
    internal class EnvironmentalReportModel
    {
        public List<Row> Rows { get; set; }

        public EnvironmentalReportModel(string[] input)
        {
            Rows = input
                .Select(l => new Row(l))
                .ToList();
        }
    }

    internal class Row
    {
        public List<List<long>> DataSet { get; set; }

        public Row(string line)
        {
            var numbers = line
                .Split(" ")
                .Select(long.Parse)
                .ToList();

            var firstRow = new List<long>(numbers);
            DataSet = new List<List<long>>() { firstRow };

            var previousRow = firstRow;

            while (!previousRow.All(n => n is 0)) 
            {
                var newRow = new List<long>();
                
                for (int i = 1; i < previousRow.Count(); i++)
                {
                    newRow.Add(previousRow[i] - previousRow[i - 1]);
                }

                DataSet.Add(newRow);

                previousRow = newRow;
            }
        }
    }
}
