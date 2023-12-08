namespace Adwencik_2k23.Handlers._8
{
    internal enum Direction
    {
        R,
        L
    }

    internal class NodeContainer
    {
        public Direction[] Directions { get; set; }
        public NodeModel[] Nodes { get; set; }

        public NodeContainer(string[] input)
        {
            Directions = input
                .First()
                .Select(c => (Direction)Enum.Parse(typeof(Direction), c.ToString()))
                .ToArray();

            Nodes = input
                .Skip(2)
                .Select(l => new NodeModel(l))
                .ToArray();

            AssignDirectionNodes();
        }

        private void AssignDirectionNodes()
        {
            foreach (var node in Nodes)
            {
                node.Right = Nodes.FirstOrDefault(c => c.Name == node.RightName);
                node.Left = Nodes.FirstOrDefault(c => c.Name == node.LeftName);
            }
        }
    }

    internal class NodeModel
    {
        public string Name { get; set; }
        public string RightName { get; set; }
        public NodeModel? Right { get; set; }
        public string LeftName { get; set; }
        public NodeModel? Left { get; set; }

        public NodeModel(string line)
        {
            var splitByEqual = line.Split("=");

            Name = splitByEqual[0].Trim();

            var splitBySides = splitByEqual[1]
                .Replace("(", "")
                .Replace(")", "")
                .Split(",");

            LeftName = splitBySides[0].Trim();
            RightName = splitBySides[1].Trim();
        }
    }
}
