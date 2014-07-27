using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingChallengesCode.DataStructures.PokerHands
{
    public class PokerHands
    {
        /// <summary>
        /// Method to determine the winner , give two hands.
        /// </summary>
        /// <param name="hand1"></param>
        /// <param name="hand2"></param>
        /// <returns></returns>
        public static string GetWinner(Hand hand1, Hand hand2)
        {
            HandRank hand1Rank = hand1.GetHandRank();
            HandRank hand2Rank = hand2.GetHandRank();

            //get the relative ordering between hand1 and hand2.

            int cmp = hand1Rank.CompareTo(hand2Rank);
            if (cmp > 0) return hand1.HandName;
            else if (cmp < 0) return hand2.HandName;

            Card[] hand1Cards = hand1.HandCards;
            Card[] hand2Cards = hand2.HandCards;

            // both hands rank same.
            switch (hand1Rank)
            {
                case HandRank.StraightFlush:
                    cmp = Hand.CompareHighCard(hand1, hand2);
                    break;
                case HandRank.FourOfAKind:
                    cmp = Hand.CompareFourOfAKind(hand1).CompareTo(Hand.CompareFourOfAKind(hand2));
                    break;
                case HandRank.ThreeOfAKind:
                    cmp = Hand.CompareThreeOfAKind(hand1).CompareTo(Hand.CompareThreeOfAKind(hand2));
                    break;
                case HandRank.FullHouse:
                    cmp = Hand.CompareThreeOfAKind(hand1).CompareTo(Hand.CompareThreeOfAKind(hand2));
                    break;
                case HandRank.Flush:
                    cmp = Hand.CompareHighCardRecursive(hand1, hand2);
                    break;
                case HandRank.Straight:
                    cmp = Hand.CompareHighCard(hand1, hand2);
                    break;
                case HandRank.TwoPair:
                    cmp = Hand.CompareTwoPair(hand1, hand2);
                    break;
                case HandRank.Pair:
                    cmp = Hand.ComparePair(hand1, hand2);
                    break;
                case HandRank.HighCard:
                    cmp = Hand.CompareHighCard(hand1, hand2);
                    break;
                default:
                    break;
            }
            return (cmp > 0) ? hand1.HandName : cmp < 0 ? hand2.HandName : "Tie";

        }

        public static void Main(string[] args)
        {
            string twoHands = "2H 3D 5S 9C KD 2C 3H 4S 8C AH";
            string blackHandString = twoHands.Substring(0, twoHands.Length/2);
            string whiteHandString = twoHands.Substring(twoHands.Length/2 + 1, twoHands.Length);
            Hand blackHand =  new Hand(blackHandString,"Black");
            Hand whiteHand = new Hand(whiteHandString,"White");
            string result = PokerHands.GetWinner(blackHand, whiteHand);


        }
    }
}
