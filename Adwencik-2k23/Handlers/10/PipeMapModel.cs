using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adwencik_2k23.Handlers._10
{
    internal enum MapCellType
    {
        Empty = -1,
        Animal,
        StraightVertical,
        StraightHorizontal,
        CornerUpRight,
        CornerUpLeft,
        CornerDownRight,
        CornerDownLeft,
    }

    internal class PipeMapModel
    {
        public List<MapCellModel> Cells { get; set; }

        public PipeMapModel(string[] input)
        {
            Cells = input.SelectMany((line, x) => line
                            .Select((character, y) => new MapCellModel(x, y, character)))
                         .ToList();
            AssignNeighbours();
        }

        public string[] GetOutputString()
        {
            var maxX = Cells.Max(c => c.X);

            var strings = new List<string>();

            for (int x = 0; x < maxX; x++)
            {
                var line = Cells.Where(c => c.X == x).OrderBy(c => c.Y).Select(c => c.Symbol);
                strings.Add(string.Join("", line));
            }

            return [.. strings];
        }

        public void UpdateCellTypeSymbol(Func<MapCellModel, bool> predicate, char newSymbol)
            => Cells.Where(predicate).ToList()
                .ForEach(c => c.Symbol = newSymbol);

        private void AssignNeighbours()
        {
            foreach (var cell in Cells.Where(c => c.Type > 0))
            {
                foreach (var neighbourCoord in _typeToNeighboursDictionary[cell.Type])
                {
                    var x = cell.X + neighbourCoord.x;
                    var y = cell.Y + neighbourCoord.y;

                    var neighbour = Cells.FirstOrDefault(c => c.X == x && c.Y == y);

                    if (neighbour is not null)
                        cell.NeighboursAtEnds.Add(neighbour);
                }
            }
        }

        private readonly Dictionary<MapCellType, List<(int x, int y)>> _typeToNeighboursDictionary = new()
        {
            { MapCellType.Empty, [] },
            { MapCellType.Animal, [] },
            { MapCellType.StraightVertical, [(-1, 0), (1, 0)] },
            { MapCellType.StraightHorizontal, [(0, -1), (0, 1)] },
            { MapCellType.CornerUpLeft, [(-1, 0),(0, -1)] },
            { MapCellType.CornerUpRight, [(-1, 0),(0, 1)] },
            { MapCellType.CornerDownRight, [(1, 0), (0, 1)] },
            { MapCellType.CornerDownLeft, [(1, 0),(0, -1)] },
        };
    }

    internal class MapCellModel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public MapCellType Type { get; set; }
        public List<MapCellModel> NeighboursAtEnds { get; set; }
        public char Symbol { get; set; }
        public bool IsPartOfLoop { get; set; } = false;
        public bool IsOutsideOfLoop { get; set; } = false;

        public MapCellModel(int x, int y, char type)
        {
            X = x;
            Y = y;
            Type = _charToTypeDictionary[type];
            NeighboursAtEnds = [];
            Symbol = _typeToBetterCharDictionary[Type];
        }

        private readonly Dictionary<char, MapCellType> _charToTypeDictionary = new()
        {
            { '.', MapCellType.Empty },
            { 'S', MapCellType.Animal },
            { '|', MapCellType.StraightVertical },
            { '-', MapCellType.StraightHorizontal },
            { 'J', MapCellType.CornerUpLeft },
            { 'L', MapCellType.CornerUpRight },
            { 'F', MapCellType.CornerDownRight },
            { '7', MapCellType.CornerDownLeft },
        };

        private static readonly Dictionary<MapCellType, char> _typeToBetterCharDictionary = new()
        {
            { MapCellType.Empty, '.' },
            { MapCellType.Animal, 'S' },
            { MapCellType.StraightVertical, '║' },
            { MapCellType.StraightHorizontal, '═' },
            { MapCellType.CornerUpLeft, '╝' },
            { MapCellType.CornerUpRight, '╚' },
            { MapCellType.CornerDownRight, '╔' },
            { MapCellType.CornerDownLeft, '╗' },
        };
    }
}
