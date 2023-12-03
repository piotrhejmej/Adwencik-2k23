using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Adwencik_2k23.Handlers._3
{
    internal class SchematicsModel
    {
        public SchematicNode[] NodeMatrix { get; set; }

        public SchematicsModel(string[] input)
        {
            NodeMatrix = input.SelectMany((line, x) =>
                line.Select((character, y) => CreateNode(character.ToString(), x, y))
            ).ToArray();

            InitPartNumbers();
        }

        public int[] GetNeighbouringPartNumbers(PartNode node)
        => Enumerable.Range(-1, 3)
                .SelectMany(x => Enumerable.Range(-1, 3)
                .Select(y =>
                    (x, y)
                ))
                .Select((coord) =>
                    NodeMatrix.FirstOrDefault(n => n.X == node.X + coord.x && n.Y == node.Y + coord.y)
                )
                .Where(node => node is not null && node is PartNumberNode)
                .Select(node => node as PartNumberNode)
                .Select(partNumberNode => partNumberNode!.PartNumber)
                .Distinct()
                .Select(partNumber => partNumber!.Id).ToArray();

        private static SchematicNode CreateNode(string character, int x, int y)
            => character switch
                {
                    "." => new EmptyNode(x, y),
                    _ when int.TryParse(character, out int digit) => new PartNumberNode(x, y, digit),
                    _ => new PartNode(x, y, character)
                };
        
        private void InitPartNumbers()
            => NodeMatrix
                .Where(node => node is PartNumberNode)
                .Select(node => node as PartNumberNode)
                .Select(partNumberNode => (id: partNumberNode!.Y + 1000 *  partNumberNode.X, partNumberNode))
                .OrderBy(x => x.id)
                .Select((model, index) => (model.id - index, model.partNumberNode))
                .GroupBy(x => x.Item1)
                .Select(x => x.Select(x => x.partNumberNode)).ToList()
                .ForEach(CreatePartNumber);

        private void CreatePartNumber(IEnumerable<PartNumberNode> neighbours)
        {
            var numberValue = new PartNumber(string.Join("", neighbours.Select(n => n.Digit)));
           
            foreach(var neightbour in neighbours)
            {
                neightbour.PartNumber = numberValue;
            }
        }
    }

    internal abstract class SchematicNode(int x, int y)
    {
        public int X { get; set; } = x;
        public int Y { get; set; } = y;
    }

    internal class PartNumberNode(int x, int y, int digit) : SchematicNode(x, y)
    {
        public PartNumber? PartNumber { get; set; }
        public int Digit { get; set; } = digit;
    }

    internal class PartNode(int x, int y, string partName) : SchematicNode(x, y)
    {
        public string PartName { get; set; } = partName;
    }

    internal class EmptyNode(int x, int y) : SchematicNode(x, y) {
    }

    internal class PartNumber(string stringValue)
    {
        public int Id { get; set; } = int.Parse(stringValue);
    }
}
