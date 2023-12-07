using Adwencik_2k23.Utils;

namespace Adwencik_2k23.Handlers._5
{
    internal enum EntryType
    {
        Seed,
        Soil,
        Fertilizer,
        Water,
        Light,
        Temperature,
        Humidity,
        Location,
        Unrelevant = -1
    }

    internal class AlmanacModel
    {
        public StepModel[] Steps { get; set; }
        public StepBatchModel[] StepBatches { get; set; }

        public MapModel[] Maps { get; set; }

        public AlmanacModel(string[] input)
        {
            var seedsLine = input.First();
            var seedValues = seedsLine.Split(":").Last().Trim();

            Steps = seedValues.Split(" ").Select(val => new StepModel(val)).ToArray();
            var inputBatches = seedValues.Split(" ")
                .Batch(2)
                .Select((batch, index) => new StepBatchModel(batch.First(), batch.Last(), index));

            StepBatches = inputBatches.ToArray();

            Maps = input
                .Skip(2)
                .BatchWhere(string.IsNullOrEmpty)
                .Select(group => new MapModel(group.ToArray()))
                .ToArray();
        }
    }

    internal class StepBatchModel
    {
        public EntryType CurrentType { get; set; }
        public double From { get; set; }
        public double To { get; set; }
        public int? Id { get; set; }

        public StepBatchModel(string from, string range, int? id = null)
        {
            CurrentType = EntryType.Seed;
            From = double.Parse(from);
            To = From + double.Parse(range) - 1;
            Id = id;
        }

        public StepBatchModel() { }
    }

    internal class StepModel
    {
        public EntryType CurrentType { get; set; }
        public double Value { get; set; }

        public StepModel(string initValue) : this(double.Parse(initValue)) { }

        public StepModel(double value)
        {
            CurrentType = EntryType.Seed;
            Value = value;
        }
    }

    internal class MapModel
    {
        public EntryType FromEntry { get; set; }
        public EntryType ToEntry { get; set; }

        public MapEntryModel[] MapEntries { get; set; }

        private Dictionary<string, EntryType> _stringToEntryTypeMap = new Dictionary<string, EntryType>()
        {
            { "seed", EntryType.Seed },
            { "soil", EntryType.Soil },
            { "fertilizer", EntryType.Fertilizer },
            { "water", EntryType.Water },
            { "light", EntryType.Light },
            { "temperature", EntryType.Temperature },
            { "humidity", EntryType.Humidity },
            { "location", EntryType.Location },
        };

        public MapModel(string[] input)
        {
            var mapDefinition = input.First().Split(" ").First().Split("-");
            FromEntry = _stringToEntryTypeMap[mapDefinition.First()];
            ToEntry = _stringToEntryTypeMap[mapDefinition.Last()];

            MapEntries = input
                .Skip(1)
                .Where(inputLine => !string.IsNullOrEmpty(inputLine))
                .Select(inputLine => new MapEntryModel(inputLine))
                .ToArray();
        }
    }

    internal class MapEntryModel
    {
        public double DestinationRangeFrom { get; set; }
        public double DestinationRangeEnd { get; set; }
        public double SourceRangeFrom { get; set; }
        public double SourceRangeTo { get; set; }
        public double RangeLength { get; set; }

        public MapEntryModel(string input)
        {
            var numbers = input.Split(" ");

            DestinationRangeFrom = double.Parse(numbers[0]);
            SourceRangeFrom = double.Parse(numbers[1]);
            RangeLength = double.Parse(numbers[2]);

            DestinationRangeEnd = DestinationRangeFrom + RangeLength - 1;
            SourceRangeTo = SourceRangeFrom + RangeLength - 1;
        }
    }
}
