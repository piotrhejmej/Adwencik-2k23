namespace Adwencik_2k23.Handlers._7
{
    public class DaySeven
    {
        public static double One(string[] input)
        {
            var model = new CamelokerModel(input);
            return Solve(model, new HandsComparer());
        }

        public static double Two(string[] input)
        {
            var model = new CamelokerModel(input, GameType.WithJocks);
            return Solve(model, new HandsWithJocksComparer());
        }

        private static double Solve(CamelokerModel model, IComparer<Hand> comparer)
            => model
                    .Hands
                    .ToList()
                    .Order(comparer)
                    .Select((hand, index) => hand.Bid * (index + 1))
                    .Sum();
    }
}
