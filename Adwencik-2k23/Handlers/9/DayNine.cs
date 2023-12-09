namespace Adwencik_2k23.Handlers._9
{
    public class DayNine
    {
        public static long One(string[] input)
        {
            var model = new EnvironmentalReportModel(input);
            return SolveOne(model);
        }

        public static long Two(string[] input)
        {
            var model = new EnvironmentalReportModel(input);
            return SolveTwo(model);
        }

        private static long SolveOne(EnvironmentalReportModel model)
            => model
                .Rows
                .Sum(r => r
                    .DataSet
                    .Select(c => c.Last())
                    .Sum()
                );

        private static long SolveTwo(EnvironmentalReportModel model)
            => model
                .Rows
                .Sum(r => r
                    .DataSet
                    .Select(c => c.First())
                    .Reverse()
                    .Aggregate((a, b) => b - a)
                );
    }
}
