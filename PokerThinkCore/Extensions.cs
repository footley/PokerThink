using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace PokerThinkCore
{
    public static class Extensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static Card ToCard(this string card)
        {
            if (string.IsNullOrWhiteSpace(card))
                throw new ArgumentOutOfRangeException("card");
            if (card.Length != 2)
                throw new ArgumentOutOfRangeException("card");

            int value;
            if (!int.TryParse(card[0].ToString(), out value))
            {
                switch (card[0])
                {
                    case 'A':
                        value = 14;
                        break;
                    case 'K':
                        value = 13;
                        break;
                    case 'Q':
                        value = 12;
                        break;
                    case 'J':
                        value = 11;
                        break;
                    case 'T':
                        value = 10;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("card");
                }
            }

            Suit suit;
            switch (card[1].ToString().ToLower())
            {
                case "s":
                    suit = Suit.Spades;
                    break;
                case "h":
                    suit = Suit.Hearts;
                    break;
                case "d":
                    suit = Suit.Diamond;
                    break;
                case "c":
                    suit = Suit.Clubs;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("card");
            }
            return new Card(value, suit);
        }

        public static HoleCards ToHoleCards(this string cards)
        {
            if (string.IsNullOrWhiteSpace(cards))
                throw new ArgumentOutOfRangeException("cards");
            if (cards.Length != 4)
                throw new ArgumentOutOfRangeException("cards");

            return new HoleCards(cards.Substring(0, 2).ToCard(), cards.Substring(2, 2).ToCard());
        }

        public static Flop ToFlop(this string cards)
        {
            if (string.IsNullOrWhiteSpace(cards))
                throw new ArgumentOutOfRangeException("cards");
            if (cards.Length != 6)
                throw new ArgumentOutOfRangeException("cards");

            return new Flop(cards.Substring(0, 2).ToCard(), cards.Substring(2, 2).ToCard(), cards.Substring(4, 2).ToCard());
        }

        public static FinalHand ToFinalHand(this string cards, Rank rank)
        {
            if (string.IsNullOrWhiteSpace(cards))
                throw new ArgumentOutOfRangeException("cards");
            if (cards.Length != 10)
                throw new ArgumentOutOfRangeException("cards");

            return new FinalHand(rank, cards.Substring(0, 2).ToCard(), cards.Substring(2, 2).ToCard(), cards.Substring(4, 2).ToCard(), cards.Substring(6, 2).ToCard(), cards.Substring(8, 2).ToCard());
        }
    }
}
