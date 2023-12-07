using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adwencik_2k23.Handlers._6
{
    internal class RaceSheetModel
    {
        public RaceModel[] Races { get; set; }

        public RaceSheetModel(string[] input)
        {
            Races = input
                .Select(r => r.Split(' ').Where(c => !string.IsNullOrEmpty(c)).Skip(1))
                .Select(r => r.Select((n) => double.Parse(n.Trim())))
                .SelectMany((r, rowId) => r.Select((c, colId) => (RowId: rowId, ColId: colId, Num: c)))
                .GroupBy(g => g.ColId)
                .Select(g => new RaceModel(g.First(c => c.RowId == 0).Num, g.First(c => c.RowId == 1).Num))
                .ToArray();
        }

        public RaceSheetModel(string[] input, bool _) 
        {
            var inputs = input
                    .Select(c => c.Split(':').Last())
                    .Select(c => c.Replace(" ", ""))
                    .Select(double.Parse);

            Races = new List<RaceModel>()
            {
                new RaceModel(inputs.First(), inputs.Last())
            }.ToArray();
        }

        public IEnumerable<IEnumerable<double>> GetPossibleRecordDistances()
            => Races
                .Select(r => Enumerable.Range(1, (int)r.Time)
                    .Select(t => r.GetPossibleDistance(t))
                    .Where(d => d > r.RecordDistance)
                );
    }

    internal class RaceModel
    {
        public double Time { get; set; }
        public double RecordDistance { get; set; }


        public RaceModel(double time, double distance)
        {
            Time = time;
            RecordDistance = distance;
        }

        public double GetPossibleDistance(double x)
            => (Time - x) * x;
    }
}
