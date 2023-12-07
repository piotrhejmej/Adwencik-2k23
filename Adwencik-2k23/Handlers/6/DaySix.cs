using Adwencik_2k23.Utils;

namespace Adwencik_2k23.Handlers._6
{
    public class DaySix
    {
        public static int One(string[] input)
        {
            var model = new RaceSheetModel(input);
            return SolveOne(model);
        }

        public static double Two(string[] input) 
        {
            var model = new RaceSheetModel(input, true);
            return SolveTwo(model);
        }

        private static int SolveOne(RaceSheetModel model)
            => model.GetPossibleRecordDistances().Mul(r => r.Count());

        private static double SolveTwo(RaceSheetModel model)
            => model.GetPossibleRecordDistances().First().Count();
    }
}
