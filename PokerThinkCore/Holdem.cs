using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerThinkCore
{
    public class HoldemHand
    {
        HoldemBoard board;
        HoleCards holeCards;

        public HoldemHand(HoldemBoard board, HoleCards hand)
        {
            this.board = board;
            this.holeCards = hand;
        }

        public FinalHand GetBest()
        {
            // gather cards and sort them
            var cards = GatherAndSort();
            var byValue = SortByValue(cards);

            // check straight flush
            var straightFlush = GetStraightFlush(byValue);
            if (straightFlush != null)
                return straightFlush;
            // check 4 of a kind
            var four = Get4OfAKind(byValue, cards);
            if (four != null)
                return four;
            // check full house
            var fullHouse = GetFullHouse(byValue, cards);
            if (fullHouse != null)
                return fullHouse;
            // check flush
            var flush = GetFlush(SortBySuit(cards));
            if (flush != null)
                return flush;
            // check straight
            var straight = GetStraight(byValue);
            if (straight != null)
                return straight;
            // check 3 of a kind
            var three = Get3OfAKind(byValue, cards);
            if (three != null)
                return three;
            // check 2 pair
            var twoPair = Get2Pair(byValue, cards);
            if (twoPair != null)
                return twoPair;
            // check pair
            var pair = GetPair(byValue, cards);
            if (pair != null)
                return pair;
            // check highcard
            return new FinalHand(Rank.HighCard, cards[0], cards[1], cards[2], cards[3], cards[4]);
        }

        private List<Card> GatherAndSort()
        {
            List<Card> cards = new List<Card>();
            cards.Add(holeCards.Card1);
            cards.Add(holeCards.Card2);
            cards.Add(board.Flop.Card1);
            cards.Add(board.Flop.Card2);
            cards.Add(board.Flop.Card3);
            cards.Add(board.Turn);
            cards.Add(board.River);
            cards.Sort((c1, c2) => c2.CompareTo(c1));
            return cards;
        }

        private Dictionary<Suit, List<Card>> SortBySuit(List<Card> cards)
        {
            var bySuit = new Dictionary<Suit, List<Card>>();
            bySuit[Suit.Spades] = new List<Card>();
            bySuit[Suit.Diamond] = new List<Card>();
            bySuit[Suit.Hearts] = new List<Card>();
            bySuit[Suit.Clubs] = new List<Card>();
            foreach (Card card in cards)
            {
                bySuit[card.Suit].Add(card);
            }
            return bySuit;
        }

        private Dictionary<int, List<Card>> SortByValue(List<Card> cards)
        {
            var byValue = new Dictionary<int, List<Card>>();
            foreach (var card in cards)
            {
                if (!byValue.ContainsKey(card.Value))
                {
                    byValue[card.Value] = new List<Card>();
                }
                byValue[card.Value].Add(card);
            }

            if (byValue.ContainsKey(14)) // Ace
            {
                byValue[1] = byValue[14];
            }

            return byValue;
        }

        private FinalHand Get4OfAKind(Dictionary<int, List<Card>> byValue, List<Card> cards)
        {
            foreach (var key in byValue.Keys)
            {
                if (key == 1)
                    continue;
                if(byValue[key].Count == 4)
                {
                    // here is the N of a kind, now we need to add the remaining best card
                    Card best = null;
                    foreach(var card in cards)
                    {
                        if (card.Value != key && (best == null || card.Value > best.Value))
                            best = card;
                    }
                    return new FinalHand(Rank.FourOfAKind, byValue[key][0], byValue[key][1], byValue[key][2], byValue[key][3], best);

                }
            }
            return null;
        }

        private FinalHand Get3OfAKind(Dictionary<int, List<Card>> byValue, List<Card> cards)
        {
            foreach (var key in byValue.Keys)
            {
                if (key == 1)
                    continue;
                if (byValue[key].Count == 3)
                {
                    // here is the N of a kind, now we need to add the remaining best card
                    Card best = null;
                    foreach (var card in cards)
                    {
                        if (card.Value != key && (best == null || card.Value > best.Value))
                            best = card;
                    }
                    Card second = null;
                    foreach (var card in cards)
                    {
                        if (card.Value != key && card != best && (second == null || card.Value > second.Value))
                            second = card;
                    }
                    return new FinalHand(Rank.ThreeOfAKind, byValue[key][0], byValue[key][1], byValue[key][2], best, second);

                }
            }
            return null;
        }

        private FinalHand Get2Pair(Dictionary<int, List<Card>> byValue, List<Card> cards)
        {
            var pairs = new List<List<Card>>();
            foreach (var key in byValue.Keys)
            {
                if (key == 1)
                    continue;
                if (byValue[key].Count == 2)
                {
                    pairs.Add(byValue[key]);
                }
            }
            if(pairs.Count >= 2)
            {
                // here is the pair, now we need to add the remaining best card
                Card best = null;
                foreach (var card in cards)
                {
                    if (card != pairs[0][0] && 
                        card != pairs[0][1] &&
                        card != pairs[1][0] &&
                        card != pairs[1][1] &&
                        (best == null || card.Value > best.Value))
                        best = card;
                }
                return new FinalHand(Rank.TwoPair, pairs[0][0], pairs[0][1], pairs[1][0], pairs[1][1], best);
            }
            return null;
        }

        private FinalHand GetPair(Dictionary<int, List<Card>> byValue, List<Card> cards)
        {
            foreach (var key in byValue.Keys)
            {
                if (key == 1)
                    continue;
                if (byValue[key].Count == 2)
                {
                    // here is the N of a kind, now we need to add the remaining best card
                    Card best = null;
                    foreach (var card in cards)
                    {
                        if (card.Value != key && (best == null || card.Value > best.Value))
                            best = card;
                    }
                    Card second = null;
                    foreach (var card in cards)
                    {
                        if (card.Value != key && card != best && (second == null || card.Value > second.Value))
                            second = card;
                    }
                    Card third = null;
                    foreach (var card in cards)
                    {
                        if (card.Value != key && card != best && card != second && (third == null || card.Value > third.Value))
                            third = card;
                    }
                    return new FinalHand(Rank.Pair, byValue[key][0], byValue[key][1], best, second, third);

                }
            }
            return null;
        }

        private FinalHand GetFullHouse(Dictionary<int, List<Card>> byValue, List<Card> cards)
        {
            var threesAndTwos = new List<List<Card>>();
            foreach (var key in byValue.Keys)
            {
                if (key == 1)
                    continue;
                if (byValue[key].Count == 3 || byValue[key].Count == 2)
                {
                    threesAndTwos.Add(byValue[key]);
                }
            }
            if (threesAndTwos.Count >= 2 && threesAndTwos.Any(l=>l.Count==3))
            {
                return new FinalHand(Rank.FullHouse, threesAndTwos[0][0], threesAndTwos[0][1], threesAndTwos[0][2], threesAndTwos[1][0], threesAndTwos[1][1]);
            }
            return null;
        }

        private FinalHand GetFlush(Dictionary<Suit, List<Card>> bySuit)
        {
            foreach (Suit s in Enum.GetValues(typeof(Suit)))
            {
                if (bySuit[s].Count >= 5)
                {
                    // we have a flush, now find the best flush
                    return new FinalHand(Rank.Flush, bySuit[s][0], bySuit[s][1], bySuit[s][2], bySuit[s][3], bySuit[s][4]);
                }
            }
            return null;
        }

        private FinalHand GetStraight(Dictionary<int, List<Card>> byValue)
        {
            var sequence = new List<Card>();
            for(int i=0; i+1 < byValue.Keys.Count; i++)
            {
                int value1 = byValue.Keys.ElementAt(i);
                int value2 = byValue.Keys.ElementAt(i+1);
                if (value1 == value2 + 1)
                {
                    if (sequence.Count == 0)
                    {
                        sequence.Add(byValue[value1][0]);
                    }
                    sequence.Add(byValue[value2][0]);
                }
                else if (sequence.Count < 5)
                    sequence.Clear();

                if (sequence.Count == 5)
                    return new FinalHand(Rank.Straight, sequence[0], sequence[1], sequence[2], sequence[3], sequence[4]);
            }
            return null;
        }

        private FinalHand GetStraightFlush(Dictionary<int, List<Card>> byValue)
        {
            var sequence = new List<List<Card>>();
            int last = byValue.Keys.ElementAt(0);
            for (int i = 1; i < byValue.Keys.Count; i++)
            {
                int current = byValue.Keys.ElementAt(i);
                if (last == current + 1)
                {
                    if (sequence.Count == 0)
                    {
                        sequence.Add(byValue[last]);
                    }
                    sequence.Add(byValue[current]);
                }
                else if (sequence.Count < 5)
                    sequence.Clear();

                last = current;
            }

            if (sequence.Count >= 5)
            {
                foreach(Suit s in Enum.GetValues(typeof(Suit)))
                {
                    var straightFlush = from l in sequence
                                        from c in l
                                        where c.Suit == s
                                        orderby c.Value descending
                                        select c ;

                    if (straightFlush.Count() >= 5)
                    {
                        int difference = straightFlush.First().Value - straightFlush.Last().Value;
                        if (difference == 4) 
                        {
                            return new FinalHand(
                                Rank.StraightFlush,
                                straightFlush.ElementAt(0),
                                straightFlush.ElementAt(1),
                                straightFlush.ElementAt(2),
                                straightFlush.ElementAt(3),
                                straightFlush.ElementAt(4)
                                );
                        }
                        else if(difference == 12) // 12 is for wheels
                        {
                            return new FinalHand(
                                Rank.StraightFlush,
                                straightFlush.ElementAt(1),
                                straightFlush.ElementAt(2),
                                straightFlush.ElementAt(3),
                                straightFlush.ElementAt(4),
                                straightFlush.ElementAt(0)
                                );
                        }
                    }
                }
            }

            return null;
        }
    }

    public class HoldemBoard
    {
        public Flop Flop{ get; set; }
        public Card Turn { get; set; }
        public Card River { get; set; }
    }
}
