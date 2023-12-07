using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adwencik_2k23.Handlers._7
{
    internal enum HandValue
    {
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind,
        Wrong = -1,
    }

    internal class CamelokerModel(string[] input, GameType gameType = GameType.Simple)
    {
        public Hand[] Hands { get; set; } = input
                .Select(l => l.Split(' '))
                .Select(l => new Hand(l.First(), l.Last(), gameType))
                .ToArray();
    }

    internal class Hand
    {
        public CardValue[] Cards { get; set; }
        public CardWithJocks[] CardsWithJocks { get; set; }
        public string CardsString { get; set; }
        public int Bid { get; set; }
        public HandValue HandValue { get; set; }

        public Hand(string cards, string bid, GameType gameType)
        {
            CardsString = cards;
            Cards = cards.Select(c => _stringToCardMap[c]).ToArray();
            CardsWithJocks = cards.Select(c => _stringToJokesCardMap[c]).ToArray();

            Bid = int.Parse(bid);
            if (gameType == GameType.Simple)
                HandValue = GetHandValue(Cards);
            else
                HandValue = GetHandValueWithJocks(CardsWithJocks);
        }

        private static HandValue GetHandValue(CardValue[] cards)
        {
            var groupCounts = cards
                .GroupBy(c => c)
                .Select(c => c.Count())
                .Order()
                .Reverse();

            return (groupCounts.First(), groupCounts.Skip(1).FirstOrDefault()) switch
            {
                (5, _) => HandValue.FiveOfAKind,
                (4, 1) => HandValue.FourOfAKind,
                (3, 2) => HandValue.FullHouse,
                (3, _) => HandValue.ThreeOfKind,
                (2, 2) => HandValue.TwoPair,
                (2, _) => HandValue.OnePair,
                _ => HandValue.Wrong
            };
        }

        private static HandValue GetHandValueWithJocks(CardWithJocks[] cards)
        {
            var groupCounts = cards
                .Where(r => r != CardWithJocks.J)
                .GroupBy(c => c)
                .Select(c => c.Count())
                .Order()
                .Reverse();

            var jocksCount = cards.Where(r => r == CardWithJocks.J).Count();

            return (groupCounts.FirstOrDefault() + jocksCount, groupCounts.Skip(1).FirstOrDefault()) switch
            {
                (5, _) => HandValue.FiveOfAKind,
                (4, 1) => HandValue.FourOfAKind,
                (3, 2) => HandValue.FullHouse,
                (3, _) => HandValue.ThreeOfKind,
                (2, 2) => HandValue.TwoPair,
                (2, _) => HandValue.OnePair,
                _ => HandValue.Wrong
            };
        }


        private readonly Dictionary<char, CardValue> _stringToCardMap = new()
        {
            { '2', CardValue.n2 },
            { '3', CardValue.n3 },
            { '4', CardValue.n4 },
            { '5', CardValue.n5 },
            { '6', CardValue.n6 },
            { '7', CardValue.n7 },
            { '8', CardValue.n8 },
            { '9', CardValue.n9 },
            { 'T', CardValue.T },
            { 'J', CardValue.J },
            { 'K', CardValue.K },
            { 'Q', CardValue.Q },
            { 'A', CardValue.A },
        };

        private readonly Dictionary<char, CardWithJocks> _stringToJokesCardMap = new()
        {
            { '2', CardWithJocks.n2 },
            { '3', CardWithJocks.n3 },
            { '4', CardWithJocks.n4 },
            { '5', CardWithJocks.n5 },
            { '6', CardWithJocks.n6 },
            { '7', CardWithJocks.n7 },
            { '8', CardWithJocks.n8 },
            { '9', CardWithJocks.n9 },
            { 'T', CardWithJocks.T },
            { 'J', CardWithJocks.J },
            { 'K', CardWithJocks.K },
            { 'Q', CardWithJocks.Q },
            { 'A', CardWithJocks.A },
        };
    }
}
