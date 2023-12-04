﻿namespace Adwencik_2k23.Handlers._4
{
    internal class ScratchCardModel
    {
        public int Id { get; set; }
        public int[] WiningNumbers { get; set; }
        public int[] Numbers { get; set; }
        public int Count { get; set; }

        public int RightNumbers => Numbers.Intersect(WiningNumbers).Count();

        public double CardValue => RightNumbers > 0 ? Math.Pow(2, RightNumbers - 1) : 0;

        public ScratchCardModel(string input)
        {
            var splitByName = input.Split(':');
            Id = int.Parse(splitByName.First().Trim().Split(' ').Last());
            var splitByNumbers = splitByName.Last().Split('|');

            WiningNumbers = splitByNumbers.First()
                .Trim()
                .Split(' ')
                .Where(number => !string.IsNullOrEmpty(number))
                .Select(number => int.Parse(number))
                .ToArray();

            Numbers = splitByNumbers.Last()
                .Trim()
                .Split(' ')
                .Where(number => !string.IsNullOrEmpty(number))
                .Select(number => int.Parse(number))
                .ToArray();

            Count = 1;
        }
    }
}