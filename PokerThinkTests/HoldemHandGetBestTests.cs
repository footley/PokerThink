using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerThinkCore;

namespace PokerThinkTests
{
    [TestClass]
    public class HoldemHandGetBestTests
    {
        [TestMethod]
        public void HighcardOnlyAvailable()
        {
            HoldemBoard board = new HoldemBoard();
            board.Flop = "TdJd2d".ToFlop();
            board.Turn = "Kh".ToCard();
            board.River = "4h".ToCard();

            HoldemHand hand = new HoldemHand(board, "Qc6c".ToHoleCards());
            var best = hand.GetBest();
            Assert.AreEqual("HighCard KhQcJdTd6c", best.ToString());
        }

        [TestMethod]
        public void PairOnlyAvailable()
        {
            HoldemBoard board = new HoldemBoard();
            board.Flop = "TdJd2d".ToFlop();
            board.Turn = "Th".ToCard();
            board.River = "4h".ToCard();

            HoldemHand hand = new HoldemHand(board, "Qc6c".ToHoleCards());
            var best = hand.GetBest();
            Assert.AreEqual("Pair ThTdQcJd6c", best.ToString());
        }

        [TestMethod]
        public void TwoPairAvailable()
        {
            HoldemBoard board = new HoldemBoard();
            board.Flop = "TdJd2d".ToFlop();
            board.Turn = "Th".ToCard();
            board.River = "2h".ToCard();

            HoldemHand hand = new HoldemHand(board, "Qc6c".ToHoleCards());
            var best = hand.GetBest();
            Assert.AreEqual("TwoPair ThTd2h2dQc", best.ToString());
        }

        [TestMethod]
        public void ThreePairAvailable()
        {
            HoldemBoard board = new HoldemBoard();
            board.Flop = "TdJd2d".ToFlop();
            board.Turn = "Th".ToCard();
            board.River = "2h".ToCard();

            HoldemHand hand = new HoldemHand(board, "6d6c".ToHoleCards());
            var best = hand.GetBest();
            Assert.AreEqual("TwoPair ThTd6d6cJd", best.ToString());
        }

        [TestMethod]
        public void ThreeOfAKindAvailable()
        {
            HoldemBoard board = new HoldemBoard();
            board.Flop = "TdJd3d".ToFlop();
            board.Turn = "Th".ToCard();
            board.River = "2h".ToCard();

            HoldemHand hand = new HoldemHand(board, "6dTc".ToHoleCards());
            var best = hand.GetBest();
            Assert.AreEqual("ThreeOfAKind ThTdTcJd6d", best.ToString());
        }

        [TestMethod]
        public void FullHouseAvailable()
        {
            HoldemBoard board = new HoldemBoard();
            board.Flop = "ThJd3d".ToFlop();
            board.Turn = "Ts".ToCard();
            board.River = "2h".ToCard();

            HoldemHand hand = new HoldemHand(board, "2dTc".ToHoleCards());
            var best = hand.GetBest();
            Assert.AreEqual("FullHouse TsThTc2h2d", best.ToString());
        }

        [TestMethod]
        public void FullHouseTwoThreeOfAindAvailable()
        {
            HoldemBoard board = new HoldemBoard();
            board.Flop = "Th2c3d".ToFlop();
            board.Turn = "Ts".ToCard();
            board.River = "2h".ToCard();

            HoldemHand hand = new HoldemHand(board, "2dTc".ToHoleCards());
            var best = hand.GetBest();
            Assert.AreEqual("FullHouse TsThTc2h2d", best.ToString());
        }

        [TestMethod]
        public void WheelAvailable()
        {
            HoldemBoard board = new HoldemBoard();
            board.Flop = "Ah2c3c".ToFlop();
            board.Turn = "Ts".ToCard();
            board.River = "4h".ToCard();

            HoldemHand hand = new HoldemHand(board, "3d5c".ToHoleCards());
            var best = hand.GetBest();
            Assert.AreEqual("Straight 5c4h3d2cAh", best.ToString());
        }

        [TestMethod]
        public void SplitStraightButStraightAvailable()
        {
            HoldemBoard board = new HoldemBoard();
            board.Flop = "AhKcTc".ToFlop();
            board.Turn = "Js".ToCard();
            board.River = "9h".ToCard();

            HoldemHand hand = new HoldemHand(board, "8d7c".ToHoleCards());
            var best = hand.GetBest();
            Assert.AreEqual("Straight JsTc9h8d7c", best.ToString());
        }

        [TestMethod]
        public void SplitStraightFlushButNoStraightFlushAvailable()
        {
            HoldemBoard board = new HoldemBoard();
            board.Flop = "AhKhQh".ToFlop();
            board.Turn = "Th".ToCard();
            board.River = "9h".ToCard();

            HoldemHand hand = new HoldemHand(board, "8hJc".ToHoleCards());
            var best = hand.GetBest();
            Assert.AreEqual("Flush AhKhQhTh9h", best.ToString());
        }

        [TestMethod]
        public void BroadwayAvailable()
        {
            HoldemBoard board = new HoldemBoard();
            board.Flop = "AhKc3c".ToFlop();
            board.Turn = "Qs".ToCard();
            board.River = "Jh".ToCard();

            HoldemHand hand = new HoldemHand(board, "Td5c".ToHoleCards());
            var best = hand.GetBest();
            Assert.AreEqual("Straight AhKcQsJhTd", best.ToString());
        }

        [TestMethod]
        public void FourOfAKindAvailable()
        {
            HoldemBoard board = new HoldemBoard();
            board.Flop = "ThJd3d".ToFlop();
            board.Turn = "Ts".ToCard();
            board.River = "2h".ToCard();

            HoldemHand hand = new HoldemHand(board, "TdTc".ToHoleCards());
            var best = hand.GetBest();
            Assert.AreEqual("FourOfAKind TsThTdTcJd", best.ToString());
        }

        [TestMethod]
        public void ExactFlushAvailable()
        {
            HoldemBoard board = new HoldemBoard();
            board.Flop = "TdJd2d".ToFlop();
            board.Turn = "Th".ToCard();
            board.River = "Tc".ToCard();

            HoldemHand hand = new HoldemHand(board, "Qd6d".ToHoleCards());
            var best = hand.GetBest();
            Assert.AreEqual("Flush QdJdTd6d2d", best.ToString());
        }

        [TestMethod]
        public void BestFlushAvailable()
        {
            HoldemBoard board = new HoldemBoard();
            board.Flop = "TdJd2d".ToFlop();
            board.Turn = "Th".ToCard();
            board.River = "3d".ToCard();

            HoldemHand hand = new HoldemHand(board, "Qd6d".ToHoleCards());
            var best = hand.GetBest();
            Assert.AreEqual("Flush QdJdTd6d3d", best.ToString());
        }

        [TestMethod]
        public void StraightFlushAvailable()
        {
            HoldemBoard board = new HoldemBoard();
            board.Flop = "2d3d4d".ToFlop();
            board.Turn = "Ad".ToCard();
            board.River = "5d".ToCard();

            HoldemHand hand = new HoldemHand(board, "5h6h".ToHoleCards());
            var best = hand.GetBest();
            Assert.AreEqual("StraightFlush 5d4d3d2dAd", best.ToString());
        }

        [TestMethod]
        public void LowStraightFlushWheelHigherStraightAvailable()
        {
            HoldemBoard board = new HoldemBoard();
            board.Flop = "Ah2h3h".ToFlop();
            board.Turn = "4h".ToCard();
            board.River = "5h".ToCard();

            HoldemHand hand = new HoldemHand(board, "6d7c".ToHoleCards());
            var best = hand.GetBest();
            Assert.AreEqual("StraightFlush 5h4h3h2hAh", best.ToString());
        }

        [TestMethod]
        public void LowStraightFlushHigherStraightAvailable()
        {
            HoldemBoard board = new HoldemBoard();
            board.Flop = "9h8h7h".ToFlop();
            board.Turn = "6h".ToCard();
            board.River = "5h".ToCard();

            HoldemHand hand = new HoldemHand(board, "Td7c".ToHoleCards());
            var best = hand.GetBest();
            Assert.AreEqual("StraightFlush 9h8h7h6h5h", best.ToString());
        }
    }
}
