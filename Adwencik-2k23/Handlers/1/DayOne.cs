using Adwencik_2k23.Utils;

namespace Adwencik_2k23.Handlers._1
{
    public static class DayOne
    {
        public static int One(string[] input) 
            => input.GetResultValue();

        public static int Two(string[] input)
            => input.Select(ReplaceWrittenDigits)
                    .GetResultValue();

        private static string ReplaceWrittenDigits(string input)
        {
            input = input.Replace("one", "onee");
            input = input.Replace("two", "twoo");
            input = input.Replace("eight", "eightt");
            input = input.Replace("nine", "ninee");
            input = input.Replace("seven", "sevenn");

            foreach(var digit in Digits)
            {
                input = input.Replace(digit, (Array.IndexOf(Digits, digit) + 1).ToString());
            }

            return input;
        }

        private static readonly string[] Digits =
        [
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine"
        ];
    }

    static class DayOneSolverHelperExtenstion 
    {
        public static int GetResultValue(this IEnumerable<string> coll)
             => coll.Select(i => i
                    .Where(i => i > 47 && i < 58)
                    .Select(i => i - 48)
                ).Select(i => i.First() * 10 + i.Last())
                .Sum();
    }
}
