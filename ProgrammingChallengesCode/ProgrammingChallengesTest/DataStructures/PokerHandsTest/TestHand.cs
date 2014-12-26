using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingChallengesCode.DataStructures.PokerHands;

namespace ProgrammingChallengesTest.DataStructures.PokerHandsTest
{
    [TestClass]
    public class TestHand
    {
        [TestMethod]
        public void TestRankOfHand_HighCard()
        {
            string handString = "2C 3D 4H 5S AS";
            var hand = new Hand(handString, HandName.Black);
            Assert.AreEqual(HandRank.HighCard,hand.GetHandRank());
        }

        [TestMethod]
        public void TestRankOfHand_Pair()
        {
            string handString = "2C 3D 4H 5S 2S";
            var hand = new Hand(handString, HandName.Black);
            Assert.AreEqual(HandRank.Pair, hand.GetHandRank());
        }

        [TestMethod]
        public void TestRankOfHand_TwoPair()
        {
            string handString = "2C 3D 4H 3S 2S";
            var hand = new Hand(handString, HandName.Black);
            Assert.AreEqual(HandRank.TwoPair,hand.GetHandRank());
        }

        [TestMethod]
        public void TestRankOfHand_ThreeOfAKind()
        {
            string handString = "2C 3D 3H 3S 4S";
            var hand = new Hand(handString, HandName.Black);
            Assert.AreEqual(HandRank.ThreeOfAKind, hand.GetHandRank());
        }

        [TestMethod]
        public void TestRankOfHand_Straight()
        {
            string handString = "2C 3D 4H 5S 6C";
            var hand = new Hand(handString, HandName.Black);
            Assert.AreEqual(HandRank.Straight, hand.GetHandRank());
        }
        [TestMethod]
        public void TestRankOfHand_Flush()
        {
            string handString = "2C 3C 7C AC 6C";
            Hand hand = new Hand(handString, HandName.Black);
            var expected = HandRank.Flush;
            Assert.AreEqual(expected, hand.GetHandRank());
        }

        [TestMethod]
        public void TestRankOfHand_FullHouse()
        {
            string handString = "AD AC AS 2C 2H";
            var hand = new Hand(handString, HandName.White);
            Assert.AreEqual(HandRank.FullHouse, hand.GetHandRank());
        }
        [TestMethod]
        public void TestRankOfHand_FourOfAKind()
        {
            string handString = "2C 2D 2H 2S 4S";
            var hand = new Hand(handString, HandName.Black);
            Assert.AreEqual(HandRank.FourOfAKind, hand.GetHandRank());
        }
        [TestMethod]
        public void TestRankOfHand_StraightFlush()
        {
            string handString = "2C 3C 4C 5C 6C";
            var hand = new Hand(handString, HandName.Black);
            Assert.AreEqual(HandRank.StraightFlush, hand.GetHandRank());
        }
    }
}
