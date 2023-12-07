using System.Reflection;

namespace Adwencik_2k23.Handlers._7
{
    public class DaySeven
    {
        public static int One(string[] input)
        {
            var model = new CamelokerModel(input);
            return SolveOne(model);
        }

        public static double Two(string[] input)
        {
            var model = new CamelokerModel(input, GameType.WithJocks);
            return SolveTwo(model);
        }

        private static int SolveOne(CamelokerModel model)
            => model
                .Hands
                .ToList()
                .Order(new HandsComparer())
                .Select((hand, index) => hand.Bid * (index + 1))
                .Sum();

        private static double SolveTwo(CamelokerModel model)
            => model
                .Hands
                .ToList()
                .Order(new HandsWithJocksComparer())
                .Select((hand, index) => hand.Bid * (index + 1))
                .Sum();
    }
}
