using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerThinkCore;

namespace PokerThinkConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // play a game of poker
            Deck deck = new Deck();

            // deal hands
            var hand1 = deck.DealHoleCards();
            var hand2 = deck.DealHoleCards();
            Console.WriteLine(hand1.ShortHand);
            Console.WriteLine(hand2.ShortHand);
            Console.ReadLine();
            // flop
            var flop = deck.DealFlop();
            Console.WriteLine(flop.ShortHand);
            Console.ReadLine();
            // turn
            var turn = deck.DealCard();
            Console.WriteLine(string.Format("{0}{1}", flop.ShortHand, turn.ShortHand));
            Console.ReadLine();
            // river
            var river = deck.DealCard();
            Console.WriteLine(string.Format("{0}{1}{2}", flop.ShortHand, turn.ShortHand, river.ShortHand));
            Console.ReadLine();
        }
    }
}
