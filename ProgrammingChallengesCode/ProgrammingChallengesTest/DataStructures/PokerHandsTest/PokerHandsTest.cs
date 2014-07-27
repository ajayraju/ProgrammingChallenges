using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingChallengesCode.DataStructures.PokerHands;

namespace ProgrammingChallengesTest.DataStructures.PokerHandsTest
{
    [TestClass]
    public class PokerHandsTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //string twoHands = ;
            string blackHandString = "2H 3D 5S 9C KD";
            string whiteHandString = "2C 3H 4S 8C AH";
            Hand blackHand = new Hand(blackHandString, "Black");
            Hand whiteHand = new Hand(whiteHandString, "White");
            string result = PokerHands.GetWinner(blackHand, whiteHand);
        }
    }
}
