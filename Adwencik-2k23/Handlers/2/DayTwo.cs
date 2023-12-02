using Adwencik_2k23.Utils;

namespace Adwencik_2k23.Handlers._2
{
    public class DayTwo
    {
        private Dictionary<Colors, int> _colorAmountDictionary = new Dictionary<Colors, int>()
        {
            { Colors.Red, 12 },
            { Colors.Green, 13 },
            { Colors.Blue, 14 },
        };

        public int One(string[] input)
            => SolveOne(input.Select(inputLine => new GameModel(inputLine)).ToArray());

        public int Two(string[] input)
            => SolveTwo(input.Select(inputLine => new GameModel(inputLine)).ToArray());

        private int SolveOne(GameModel[] games)
            => games
                .Where(g => !g.Grabs
                    .SelectMany(gr => gr.ColorGrabs)
                    .Any(c => c.Amount > _colorAmountDictionary[c.Color])
                ).Select(g => g.Id)
            .Sum();

        private int SolveTwo(GameModel[] games)
            => games
                .Select(g => g.Grabs
                    .SelectMany(gr => gr.ColorGrabs)
                    .OrderByDescending(cg => cg.Amount)
                    .GroupBy(cg => cg.Color)
                    .Select(c => c.First())
                ).Select(g => g.Mul(c => c.Amount))
                .Sum();
    }
}
