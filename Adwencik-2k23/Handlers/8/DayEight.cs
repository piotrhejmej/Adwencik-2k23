namespace Adwencik_2k23.Handlers._8
{
    public class DayEight
    {
        private readonly static string Z = "Z";
        private readonly static string ZZZ = "ZZZ";

        public static long One(string[] input)
        {
            var model = new NodeContainer(input);
            return SolveOne(model);
        }

        public static long Two(string[] input)
        {
            var model = new NodeContainer(input);
            return SolveTwo(model);
        }

        private static long SolveOne(NodeContainer model)
        {
            var current = model.Nodes.First(n => n.Name == "AAA");

            if (current is null)
                return -1;

            return GetNumberOfSteps(current, model.Directions, ZZZ);
        }

        private static long SolveTwo(NodeContainer model)
        {
            var currents = model.Nodes.Where(n => n.Name.EndsWith('A')).ToList();

            if (currents is null)
                return -1;

            var shortestSteps = currents.Select(c => GetNumberOfSteps(c, model.Directions, Z)).ToArray();

            return LCM(shortestSteps);
        }

        private static long GetNumberOfSteps(NodeModel current, Direction[] directions, string endString)
        {
            var steps = 0;

            while (!current.Name.EndsWith(endString))
            {
                foreach (var direction in directions)
                {
                    steps++;
                    current = direction switch
                    {
                        Direction.R => current.Right!,
                        _ => current.Left!
                    };

                    if (current is null)
                        return -1;

                    if (current.Name.EndsWith(endString))
                        break;
                }
            }

            return steps;
        }

        static long LCM(long[] numbers)
        {
            return numbers.Aggregate(LCM);
        }

        static long LCM(long a, long b)
        {
            return Math.Abs(a * b) / GCD(a, b);
        }

        static long GCD(long a, long b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }
    }
}
