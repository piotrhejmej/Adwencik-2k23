using Adwencik_2k23.Utils;
using System.Diagnostics.Metrics;
using System.Net.NetworkInformation;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace Adwencik_2k23.Handlers._10
{
    public class DayTen
    {
        public static long One(string[] input)
        {
            var model = new PipeMapModel(input);
            return SolveOne(model);
        }

        public static long Two(string[] input)
        {
            var model = new PipeMapModel(input);
            return SolveTwo(model);
        }

        private static long SolveOne(PipeMapModel model)
        {
            var animal = model.Cells.First(c => c.Type == MapCellType.Animal);
            UpdateCellCoveredByAnimal(animal, model);
            return MarkLoopAndGetLength(animal) / 2;
        }

        private static long SolveTwo(PipeMapModel model)
        {
            var animal = model.Cells.First(c => c.Type == MapCellType.Animal);
            UpdateCellCoveredByAnimal(animal, model);
            MarkLoopAndGetLength(animal);

            var notPartOfTheLoop = model.Cells.Where(c => !c.IsPartOfLoop).ToList();

            var counter = 0;
            foreach (var cell in notPartOfTheLoop)
            {
                var ray = model.Cells
                    .Where(c => c.X == cell.X && c.Y < cell.Y).ToList();

                var cutCounter = 0;
                for (var i = 0; i < ray.Count; i++)
                {
                    var current = ray[i];

                    if (!current.IsPartOfLoop) continue;

                    var neighbourGroupTypes = new List<MapCellType>() { current.Type };

                    var next = ray.Skip(i + 1).FirstOrDefault();

                    while (current.NeighboursAtEnds.Contains(next!))
                    {
                        neighbourGroupTypes.Add(next!.Type);
                        current = next;
                        i++;
                        next = ray.Skip(i + 1).FirstOrDefault();
                    }

                    var upTypes = neighbourGroupTypes.Where(c => c == MapCellType.CornerUpLeft || c == MapCellType.CornerUpRight).Count();
                    var downTypes = neighbourGroupTypes.Where(c => c == MapCellType.CornerDownLeft || c == MapCellType.CornerDownRight).Count();

                    if (upTypes == downTypes)
                    {
                        cutCounter++;
                    }

                }

                if (cutCounter > 0 && cutCounter % 2 > 0)
                {
                    cell.IsOutsideOfLoop = true;
                    counter++;
                }
            }

            PrintFormattedPipeMap(model);

            return counter;
        }

        private static long MarkLoopAndGetLength(MapCellModel startCell)
        {
            var currentCell = startCell.NeighboursAtEnds.First();
            var prevCell = startCell;

            var counter = 1;

            while (currentCell != startCell)
            {
                currentCell.IsPartOfLoop = true;
                var tmp = currentCell;
                counter++;
                currentCell = currentCell.NeighboursAtEnds.First(c => c != prevCell);
                prevCell = tmp;
            }

            return counter;
        }

        private static void UpdateCellCoveredByAnimal(MapCellModel animal, PipeMapModel model)
        {
            var animalNeighbours = model.Cells.Where(c => c.NeighboursAtEnds.Contains(animal)).ToList();
            animal.IsPartOfLoop = true;
            animal.NeighboursAtEnds = animalNeighbours;
            var first = animalNeighbours.First();
            var last = animalNeighbours.Last();

            animal.Type = (first.X - animal.X + last.X - animal.X, first.Y - animal.Y + last.Y - animal.Y) switch
            {
                (1, 1) => MapCellType.CornerDownRight,
                (1, -1) => MapCellType.CornerDownLeft,
                (-1, 1) => MapCellType.CornerUpRight,
                (-1, -1) => MapCellType.CornerDownRight,
                (0, 0) when last.X == first.X => MapCellType.StraightHorizontal,
                (0, 0) when last.Y == first.Y => MapCellType.StraightVertical,
                _ => MapCellType.Animal
            };
        }

        private static void PrintFormattedPipeMap(PipeMapModel model)
        {
            model.UpdateCellTypeSymbol(c => c.IsOutsideOfLoop, '█');
            OutputPrinter.PrintToFile(model.GetOutputString(), "output.txt");
        }
    }
}
