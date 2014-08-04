using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingChallengesCode.DataStructures.PokerHands;

namespace ProgrammingChallengesTest.DataStructures.PokerHandsTest
{
    [TestClass]
    public class TestCard
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCardWithInvalidArgument()
        {
            Card c =  new Card(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void TestCardWithInvalidArgument_Two()
        {
            Card c =  new Card("2CF");
        }

        [TestMethod]
        public void TestCompareTo_WithSameCards()
        {
            Card c1 =  new Card("2C");
            Card c2 = new Card("2C");
            Assert.AreEqual(0, c1.CompareTo(c2));
        }

        public void TestCompareTo_WithUnequalCards()
        {
            Card c1 =  new Card("2C");
            Card c2 =  new Card("KS");
            Assert.AreEqual(-1,c1.CompareTo(c2));
        }
    }
}
