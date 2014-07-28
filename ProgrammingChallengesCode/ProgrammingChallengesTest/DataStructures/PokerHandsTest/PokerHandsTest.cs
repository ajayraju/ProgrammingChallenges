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
            string handString = "2H 4S 4C 2D 4H 2S 8S AS QS 3S";
            string blackHandString = null;
            string whiteHandString = null;
            GetHands(handString,ref blackHandString,ref whiteHandString);
            Hand blackHand = new Hand(blackHandString, HandName.Black);
            Hand whiteHand = new Hand(whiteHandString, HandName.White);
            PokerOutcome result = PokerHands.GetWinner(blackHand, whiteHand);
            Assert.AreEqual(PokerOutcome.Black,result);

        }

        private void GetHands(string handString, ref string blackHandString, ref string whiteHandString)
        {
            if (!string.IsNullOrWhiteSpace(handString))
            {
                blackHandString = handString.Substring(0, handString.Length / 2);
                whiteHandString = handString.Substring(handString.Length / 2 + 1);
            }
        }
    }
}
