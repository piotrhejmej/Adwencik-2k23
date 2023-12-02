using Adwencik_2k23.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adwencik_2k23.Handlers._1
{
    public class DayOne : IDay
    {
        public string[] Input { get; set; }

        public DayOne()
        {
            Input = new InputLoader().Load("one");
        }

        public void Run()
        {
            var pierwsze = Pierwsze(Input);
            var drugie = Drugie(Input);
        }

        public int Pierwsze(string[] input) 
        {
            var inty = ParsePierwsze(input);

            var res = input
                .Select(i => i
                .Where(i => i > 47 && i < 58)
                .Select(i => i - 48)
            ).Select(i => i.First() * 10 + i.Last())
            .Sum();

            return res;
        }

        public int Drugie(string[] input)
        {
            var res = input.Select(ReplaceWrittenDigits)
                .Select(i => i
                .Where(i => i > 47 && i < 58)
                .Select(i => i - 48)
            ).Select(i => {
                var C = string.Join("", i);
                var a = i;
                return i.First() * 10 + i.Last();
                })
            .Sum();

            return res;
        }

        private int[][] ParsePierwsze(string[] input)
         => input
            .Select(i => i.Select(c => (int)c).ToArray())
            .ToArray();

        private string ReplaceWrittenDigits(string input)
        {
            var ORG = input;
            input = input.Replace("one", "oneXe");
            input = input.Replace("two", "twoXo");
            input = input.Replace("eight", "eightXt");
            input = input.Replace("nine", "nineXe");
            input = input.Replace("seven", "sevenXn");
            var i = 1;

            foreach(var digit in Digits)
            {
                input = input.Replace(digit, i.ToString());
                i++;
            }

            return input;
        }


        private static string[] Digits =
        {
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine"
        };
    }
}
//54093