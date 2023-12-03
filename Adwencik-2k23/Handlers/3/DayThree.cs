using Adwencik_2k23.Utils;

namespace Adwencik_2k23.Handlers._3
{
    public class DayThree
    {
        public static int One(string[] input)
        {
            var model = new SchematicsModel(input);
           
            return SolveOne(model);
        }

        public static int Two(string[] input)
        {
            var model = new SchematicsModel(input);

            return SolveTwo(model);
        }

        private static int SolveOne(SchematicsModel model)
            => model
                .NodeMatrix
                .Where(n => n is PartNode)
                .Select(n => n as PartNode)
                .Select(n => model.GetNeighbouringPartNumbers(n!)
                    .Sum())
                .Sum();

        private static int SolveTwo(SchematicsModel model)
            => model
                 .NodeMatrix
                 .Where(n => n is PartNode)
                 .Select(n => n as PartNode)
                 .Where(n => n!.PartName is "*")
                 .Select(n => model.GetNeighbouringPartNumbers(n!))
                 .Where(n => n.Length is 2)
                 .Sum(n => n.Mul(x => x));
    }
}
