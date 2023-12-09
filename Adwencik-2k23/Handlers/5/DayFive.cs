namespace Adwencik_2k23.Handlers._5
{
    public class DayFive
    {
        public static double One(string[] input)
        {
            var model = new AlmanacModel(input);
            return SolveOne(model);
        }

        public static double Two(string[] input)
        {
            var model = new AlmanacModel(input);
            return SolveTwo(model);
        }

        private static double SolveOne(AlmanacModel model)
        {
            foreach (var entry in model.Steps)
            {
                while (entry.CurrentType is not EntryType.Location)
                {
                    var matchingMapGroup = model.Maps.First(m => m.FromEntry == entry.CurrentType);
                    var matchingMap = matchingMapGroup
                        .MapEntries
                        .Where(me => me.SourceRangeFrom <= entry.Value && me.SourceRangeTo >= entry.Value)
                        .FirstOrDefault();

                    entry.CurrentType = matchingMapGroup.ToEntry;

                    if (matchingMap is null)
                        continue;

                    var diff = entry.Value - matchingMap.SourceRangeFrom;
                    entry.Value = diff + matchingMap.DestinationRangeFrom;
                }
            }

            return model.Steps.OrderBy(s => s.Value).First().Value;
        }

        //Doesn't work
        private static double SolveTwo(AlmanacModel model)
        {
            var newBatches = model.StepBatches.ToList();
            var processedBatches = new List<StepBatchModel>(newBatches);

            while (newBatches.Count != 0)
            {
                newBatches = ProcessBatches(newBatches, model);
                processedBatches.AddRange(newBatches);
            }

            var test = processedBatches
                .GroupBy(r => (r.From, r.To))
                .Where(c => c.Count() > 1)
                .SelectMany(c => c.ToList())
                .OrderBy(c => c.From)
                .ToList();

            return processedBatches.Where(r => r.CurrentType is EntryType.Location).Min(b => b.From);
        }

        private static List<StepBatchModel> ProcessBatches(List<StepBatchModel> batches, AlmanacModel model)
        {
            var newBatches = new List<StepBatchModel>();
            foreach (var batch in batches)
            {
                while (batch.CurrentType is not EntryType.Location)
                {
                    var matchingMapGroup = model.Maps.First(m => m.FromEntry == batch.CurrentType);

                    var fullyOverlapingMap = matchingMapGroup
                        .MapEntries.FirstOrDefault(m => m.SourceRangeFrom <= batch.From && m.SourceRangeTo >= batch.To);

                    if (fullyOverlapingMap is not null)
                    {
                        UpdateBatch(batch, fullyOverlapingMap, matchingMapGroup.ToEntry);
                        continue;
                    }

                    var upOverlappingMap = matchingMapGroup
                        .MapEntries.FirstOrDefault(m => (batch.From >= m.SourceRangeFrom && batch.From <= m.SourceRangeTo));

                    var downOverlappingMap = matchingMapGroup
                        .MapEntries.FirstOrDefault(m => (batch.To >= m.SourceRangeFrom && batch.To <= m.SourceRangeTo));

                    if (upOverlappingMap is not null)
                    {
                        var newBatch = new StepBatchModel
                        {
                            From = batch.From,
                            To = upOverlappingMap.SourceRangeTo
                        };
                        batch.From = newBatch.To + 1;
                        UpdateBatch(newBatch, upOverlappingMap, matchingMapGroup.ToEntry);
                        newBatches.Add(newBatch);
                    }

                    if (downOverlappingMap is not null)
                    {
                        var newBatch = new StepBatchModel
                        {
                            From = downOverlappingMap.SourceRangeFrom,
                            To = batch.To
                        };
                        batch.To = newBatch.From - 1;
                        UpdateBatch(newBatch, downOverlappingMap, matchingMapGroup.ToEntry);
                        newBatches.Add(newBatch);
                    }

                    if (batch.To - batch.From <= 1)
                    {
                        batch.CurrentType = EntryType.Unrelevant;
                        break;
                    }

                    batch.CurrentType = matchingMapGroup.ToEntry;
                }
            }

            return newBatches;
        }

        private static void UpdateBatch(StepBatchModel batch, MapEntryModel map, EntryType newType)
        {
            batch.CurrentType = newType;

            var diffFrom = batch.From - map.SourceRangeFrom;
            batch.From = diffFrom + map.DestinationRangeFrom;

            var diffTo = batch.To - map.SourceRangeFrom;
            batch.To = diffTo + map.DestinationRangeFrom;
        }
    }
}
