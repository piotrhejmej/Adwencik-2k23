namespace Adwencik_2k23.Handlers._4
{
    public class DayFour
    {
        public static double One(string[] input)
        {
            var scratchCards = input.Select(line => new ScratchCardModel(line)).ToArray();

            return SolveOne(scratchCards);
        }

        public static double Two(string[] input)
        {
            var scratchCards = input.Select(line => new ScratchCardModel(line)).ToArray();

            return SolveTwo(scratchCards);
        }

        private static double SolveOne(ScratchCardModel[] scratchCards)
            => scratchCards.Select(card => card.CardValue).Sum();

        private static double SolveTwo(ScratchCardModel[] scratchCards)
        {
            foreach (var card in scratchCards)
            {
                Enumerable
                    .Range(card.Id + 1, card.RightNumbers)
                    .ToList()
                    .ForEach(Id =>
                    {
                        var nextCard = scratchCards.FirstOrDefault(c => c.Id == Id);
                        if (nextCard is not null)
                        {
                            nextCard.Count += card.Count;
                        }
                    });
            }

            return scratchCards.Select(card => card.Count).Sum();
        }
    }
}
