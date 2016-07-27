using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerThinkCore
{
    public enum Suit
    {
        Spades = 4,
        Hearts = 3,
        Diamond = 2,
        Clubs = 1
    }

    public enum Rank
    {
        StraightFlush = 9,
        FourOfAKind = 8,
        FullHouse = 7,
        Flush = 6,
        Straight = 5,
        ThreeOfAKind = 4,
        TwoPair = 3,
        Pair = 2,
        HighCard = 1
    }

    [DebuggerDisplay("{ShortHand}")]
    public class Card : IComparable<Card>, IEquatable<Card>
    {
        public Suit Suit { get; private set; }
        public int Value { get; private set; }
        public string ShortHand
        {
            get { return string.Format("{0}{1}", GetValueShorthand(), Suit.ToString().ToLower()[0]); }
        }

        #region Constructors
        public Card(int value, Suit suit)
        {
            if (value < 2 || value > 14)
                throw new ArgumentOutOfRangeException("value");
            Value = value;
            Suit = suit;
        }
        #endregion

        #region Methods
        private string GetValueShorthand()
        {
            if (Value > 1 && Value < 10)
                return Value.ToString();
            switch(Value)
            {
                case 14:
                    return "A";
                case 13:
                    return "K";
                case 12:
                    return "Q";
                case 11:
                    return "J";
                case 10:
                    return "T";
                default:
                    throw new InvalidOperationException("Value out of Range");
            }
        }

        public int CompareTo(Card other)
        {
            if (Value != other.Value)
                return Value.CompareTo(other.Value);
            else if (Suit != other.Suit)
                return Suit.CompareTo(other.Suit);
            return 0;
        }

        #region Equality
        public override bool Equals(object obj)
        {
            return this.Equals((Card)obj);
        }

        public override int GetHashCode()
        {
            return ShortHand.GetHashCode();
        }

        public bool Equals(Card other)
        {
            return this.Suit == other.Suit && this.Value == other.Value;
        }
        #endregion

        #endregion

        public override string ToString()
        {
            return ShortHand;
        }
    }

    [DebuggerDisplay("{ShortHand}")]
    public class HoleCards : IEquatable<HoleCards>
    {
        public string ShortHand
        {
            get { return string.Format("{0}{1}", Card1.ShortHand, Card2.ShortHand); }
        }

        public Card Card1 { get; private set; }
        public Card Card2 { get; private set; }

        public HoleCards(Card card1, Card card2)
        {
            Card1 = card1;
            Card2 = card2;
        }

        #region Equality
        public override bool Equals(object obj)
        {
            return this.Equals((HoleCards)obj);
        }

        public override int GetHashCode()
        {
            return ShortHand.GetHashCode();
        }

        public bool Equals(HoleCards other)
        {
            return Card1.Equals(other.Card1) &&
                Card2.Equals(other.Card2);
        }
        #endregion

        public override string ToString()
        {
            return ShortHand;
        }
    }

    [DebuggerDisplay("{ShortHand}")]
    public class Flop : IEquatable<Flop>
    {
        public string ShortHand
        {
            get { return string.Format("{0}{1}{2}", Card1.ShortHand, Card2.ShortHand, Card3.ShortHand); }
        }

        public Card Card1 { get; private set; }
        public Card Card2 { get; private set; }
        public Card Card3 { get; private set; }

        public Flop(Card card1, Card card2, Card card3)
        {
            Card1 = card1;
            Card2 = card2;
            Card3 = card3;
        }

        #region Equality
        public override bool Equals(object obj)
        {
            return this.Equals((Flop)obj);
        }

        public override int GetHashCode()
        {
            return ShortHand.GetHashCode();
        }

        public bool Equals(Flop other)
        {
            return Card1.Equals(other.Card1) &&
                Card2.Equals(other.Card2) &&
                Card3.Equals(other.Card3);
        }
        #endregion

        public override string ToString()
        {
            return ShortHand;
        }
    }

    [DebuggerDisplay("{ShortHand}")]
    public class FinalHand : IEquatable<FinalHand>, IComparable<FinalHand>
    {
        public Rank HandRank { get; private set; }

        public string ShortHand
        {
            get { return string.Format("{0}{1}{2}{3}{4}", Card1.ShortHand, Card2.ShortHand, Card3.ShortHand, Card4.ShortHand, Card5.ShortHand); }
        }

        public Card Card1 { get; private set; }
        public Card Card2 { get; private set; }
        public Card Card3 { get; private set; }
        public Card Card4 { get; private set; }
        public Card Card5 { get; private set; }

        public FinalHand(Rank rank, Card card1, Card card2, Card card3, Card card4, Card card5)
        {
            HandRank = rank;
            Card1 = card1;
            Card2 = card2;
            Card3 = card3;
            Card4 = card4;
            Card5 = card5;
        }

        #region Equality
        public override bool Equals(object obj)
        {
            return this.Equals((FinalHand)obj);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public bool Equals(FinalHand other)
        {
            return HandRank.Equals(other.HandRank) &&
                Card1.Equals(other.Card1) &&
                Card2.Equals(other.Card2) &&
                Card3.Equals(other.Card3) &&
                Card4.Equals(other.Card4) &&
                Card5.Equals(other.Card5);
        }
        #endregion

        #region Comparison

        public int CompareTo(FinalHand other)
        {
            if (HandRank != other.HandRank)
                return HandRank.CompareTo(other.HandRank);
            var comp = Card1.Value.CompareTo(other.Card1.Value);
            if (comp != 0)
                return comp;
            comp = Card2.Value.CompareTo(other.Card2.Value);
            if (comp != 0)
                return comp;
            comp = Card3.Value.CompareTo(other.Card3.Value);
            if (comp != 0)
                return comp;
            comp = Card4.Value.CompareTo(other.Card4.Value);
            if (comp != 0)
                return comp;
            comp = Card5.Value.CompareTo(other.Card5.Value);
            return comp;
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0} {1}", HandRank, ShortHand);
        }

        
    }

    public class Deck : ICloneable
    {
        private List<Card> deck;
        private List<Card> delt;

        public int Count { get { return deck.Count; } }

        #region Constructors
        public Deck()
        {
            deck = new List<Card>();
            delt = new List<Card>();

            foreach(Suit suit in Enum.GetValues(typeof(Suit)))
            {
                for (int i = 2; i <= 14; i++)
                {
                    deck.Add(new Card(i, suit));
                }
            }
            Shuffle();
        }
        public Deck(List<Card> deck, List<Card> delt)
        {
            this.deck = deck;
            this.delt = delt;
        }
        #endregion

        public void Shuffle()
        {
            deck.Shuffle();
        }

        public Card DealCard()
        {
            if (deck.Count == 0)
                throw new InvalidOperationException("Empty Deck");
            var card = deck[0];
            deck.RemoveAt(0);
            delt.Add(card);
            return card;
        }

        public HoleCards DealHoleCards()
        {
            return new HoleCards(DealCard(), DealCard());
        }

        public Flop DealFlop()
        {
            return new Flop(DealCard(), DealCard(), DealCard());
        }

        public object Clone()
        {
            return new Deck(this.deck, this.delt);
        }
    }
}
