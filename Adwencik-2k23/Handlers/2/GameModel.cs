using System.Collections.Generic;

namespace Adwencik_2k23.Handlers._2
{
    internal enum Colors
    {
        Blue,
        Green,
        Red
    }

    internal class GameModel
    {
        public int Id { get; set; }
        public GrabModel[] Grabs { get; set; }

        public GameModel(string input)
        {
            var gameGrabSplit = input.Trim().Split(':');

            Id = int.Parse(gameGrabSplit.First().Split(" ").Last());
            
            var grabSplit = gameGrabSplit.Last().Split(";");
            Grabs = grabSplit.Select(grab => new GrabModel(grab)).ToArray();
        }
    }

    internal class GrabModel
    {
        public ColorGrab[] ColorGrabs { get; set; }

        public GrabModel(string grabInput)
        {
            var colorsSplit = grabInput.Trim().Split(",");
            ColorGrabs = colorsSplit.Select(colorGrab => new ColorGrab(colorGrab)).ToArray();
        }
    }

    internal class ColorGrab
    {
        public Colors Color { get; set; }
        public int Amount { get; set; }

        public ColorGrab(string colorGrabInput)
        {
            var colorAmountSplit = colorGrabInput.Trim().Split(" ");
            Color = ColorNameEntryDictionary[colorAmountSplit.Last()];
            Amount = int.Parse(colorAmountSplit.First());
        }

        private readonly Dictionary<string, Colors> ColorNameEntryDictionary = new()
        {
            { "red", Colors.Red },
            { "green", Colors.Green },
            { "blue", Colors.Blue }
        };
    }
}
