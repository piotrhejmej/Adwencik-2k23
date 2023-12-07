using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adwencik_2k23.Handlers._7
{
    internal class HandsComparer : IComparer<Hand>
    {
        public int Compare(Hand? x, Hand? y)
        {
            if (x.HandValue > y.HandValue)
                return 1;

            if (x.HandValue < y.HandValue)
                return -1;

            for (int i = 0; i < x.Cards.Count(); i++)
            {
                if (x.Cards[i] > y.Cards[i])
                    return 1;

                if (x.Cards[i] < y.Cards[i])
                    return -1;
            }

            return 0;
        }
    }

    internal class HandsWithJocksComparer : IComparer<Hand>
    {
        public int Compare(Hand? x, Hand? y)
        {
            if (x.HandValue > y.HandValue)
                return 1;

            if (x.HandValue < y.HandValue)
                return -1;

            for (int i = 0; i < x.Cards.Count(); i++)
            {
                if (x.CardsWithJocks[i] > y.CardsWithJocks[i])
                    return 1;

                if (x.CardsWithJocks[i] < y.CardsWithJocks[i])
                    return -1;
            }

            return 0;
        }
    }
}
