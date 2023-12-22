using Adwencik_2k23.Handlers._4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adwencik_2k23.Handlers._19
{
    public static class DayNineteen
    {
        public static double One(string[] input)
        {
            var model = new SystemModel(input);

            return SolveOne(model);
        }

        public static double Two(string[] input)
        {
            var model = new SystemModel(input);

            return SolveTwo(model);
        }

        private static double SolveOne(SystemModel model)
        {
            return model.SolveFirst();
        }

        private static double SolveTwo(SystemModel model)
        {
            return model.SolveSecond();
        }
    }
}
