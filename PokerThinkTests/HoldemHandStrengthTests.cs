using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerThinkCore;

namespace PokerThinkTests
{
    [TestClass]
    public class HoldemHandStrengthTests
    {
        [TestMethod]
        public void SixHighStraightVsWheel()
        {
            var best = new FinalHand(Rank.Straight, "6h".ToCard(), "5h".ToCard(), "4c".ToCard(), "3h".ToCard(), "2h".ToCard());
            var worst = new FinalHand(Rank.Straight, "5h".ToCard(), "4c".ToCard(), "3h".ToCard(), "2h".ToCard(), "Ah".ToCard());

            Assert.AreEqual(1, best.CompareTo(worst));
        }

        [TestMethod]
        public void StraightVsFlush()
        {
            var best = new FinalHand(Rank.Straight, "7h".ToCard(), "6c".ToCard(), "5h".ToCard(), "4h".ToCard(), "3h".ToCard());
            var worst = new FinalHand(Rank.Flush, "6h".ToCard(), "5h".ToCard(), "Th".ToCard(), "3h".ToCard(), "2h".ToCard()); 

            Assert.AreEqual(-1, best.CompareTo(worst));
        }

        [TestMethod]
        public void HighCardVsHighCard()
        {
            var best = new FinalHand(Rank.HighCard, "Th".ToCard(), "5h".ToCard(), "4h".ToCard(), "3c".ToCard(), "2h".ToCard());
            var worst = new FinalHand(Rank.HighCard, "Jc".ToCard(), "5h".ToCard(), "4h".ToCard(), "3h".ToCard(), "2h".ToCard());

            Assert.AreEqual(-1, best.CompareTo(worst));
        }

        [TestMethod]
        public void HighCardVsHighCardSame()
        {
            var best = new FinalHand(Rank.HighCard, "Th".ToCard(), "5h".ToCard(), "4h".ToCard(), "3c".ToCard(), "2h".ToCard());
            var worst = new FinalHand(Rank.HighCard, "Th".ToCard(), "5h".ToCard(), "4h".ToCard(), "3h".ToCard(), "2h".ToCard());

            Assert.AreEqual(0, best.CompareTo(worst));
        }

        [TestMethod]
        public void HighCardVsHighCardLateDiff()
        {
            var best = new FinalHand(Rank.HighCard, "Th".ToCard(), "6h".ToCard(), "5h".ToCard(), "4c".ToCard(), "2h".ToCard());
            var worst = new FinalHand(Rank.HighCard, "Th".ToCard(), "5h".ToCard(), "4h".ToCard(), "3h".ToCard(), "2h".ToCard());
            Assert.AreEqual(1, best.CompareTo(worst));

            best = new FinalHand(Rank.HighCard, "Th".ToCard(), "6h".ToCard(), "5h".ToCard(), "4c".ToCard(), "2h".ToCard());
            worst = new FinalHand(Rank.HighCard, "Th".ToCard(), "6h".ToCard(), "4h".ToCard(), "3h".ToCard(), "2h".ToCard());
            Assert.AreEqual(1, best.CompareTo(worst));

            best = new FinalHand(Rank.HighCard, "Th".ToCard(), "6h".ToCard(), "5h".ToCard(), "4c".ToCard(), "2h".ToCard());
            worst = new FinalHand(Rank.HighCard, "Th".ToCard(), "6h".ToCard(), "5h".ToCard(), "3h".ToCard(), "2h".ToCard());
            Assert.AreEqual(1, best.CompareTo(worst));

            best = new FinalHand(Rank.HighCard, "Th".ToCard(), "6h".ToCard(), "5h".ToCard(), "4c".ToCard(), "3h".ToCard());
            worst = new FinalHand(Rank.HighCard, "Th".ToCard(), "6h".ToCard(), "5h".ToCard(), "4h".ToCard(), "2h".ToCard());
            Assert.AreEqual(1, best.CompareTo(worst));
        }
    }
}
