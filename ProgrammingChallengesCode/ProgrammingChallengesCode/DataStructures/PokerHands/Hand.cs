using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingChallengesCode.DataStructures.PokerHands
{
    public enum HandRank
    {
        HighCard,
        Pair,
        TwoPair,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush
    }

    public class Hand
    {
        public Card[] HandCards { get; set; }
        public string HandName { get; set; }

        public Hand()
        {
            HandCards = null;
            HandName = string.Empty;
        }

        public Hand(string handString, string name)
        {
            string[] cards = handString.Split(new char[] {' '});
            HandCards =  new Card[cards.Length];
            for (int i = 0; i < cards.Length; i++)
            {
                HandCards[i] =  new Card(cards[i]);
            }
            Array.Sort(HandCards);
            HandName = name;
        }

        /// <summary>
        /// method to compute the rank of a hand.
        /// </summary>
        /// <returns></returns>
        private HandRank GetHandRank()
        {
            Card[] tempHand = this.HandCards;
            if(IsStraightFlush(tempHand)) return HandRank.StraightFlush;
            if(IsFourOfAKind(tempHand)) return HandRank.FourOfAKind;
            if(IsFullHouse(tempHand)) return HandRank.FullHouse;
            if(IsFlush(tempHand)) return HandRank.Flush;
            if(IsStraight(tempHand)) return HandRank.Straight;
            if(IsThreeOfAKind(tempHand)) return HandRank.ThreeOfAKind;
            if(IsTwoPair(tempHand)) return HandRank.TwoPair;
            if(IsPair(tempHand)) return HandRank.Pair;
            return HandRank.HighCard;
        }

        #region Card Rank Helpers
        private static bool IsPair(Card[] hand)
        {
            var cardValues = GetCardValuesFromHand(hand);
            return (cardValues[0] == cardValues[1] && cardValues[1] != cardValues[2] ||
                cardValues[1] ==  cardValues[2] && cardValues[2] != cardValues[3] && cardValues[0] != cardValues[1] ||
                cardValues[2] ==  cardValues[3] && cardValues[3]!= cardValues[4] && cardValues[2] != cardValues[1] || 
                cardValues[3] ==  cardValues[4] && cardValues[2] != cardValues[3]) ;

        }

        private static bool IsTwoPair(Card[] hand)
        {
            var cardValues = GetCardValuesFromHand(hand);
            return (cardValues[0] == cardValues[1] && cardValues[2] == cardValues[3] &&
                        cardValues[1] != cardValues[2] && cardValues[3] != cardValues[4] ||
                        cardValues[1] == cardValues[2] && cardValues[3] == cardValues[4] &&
                        cardValues[2] != cardValues[3] && cardValues[0] != cardValues[1]);
        }

        private static bool IsThreeOfAKind(Card[] hand)
        {
            var cardValues = GetCardValuesFromHand(hand);
            return (cardValues[0] == cardValues[2] || cardValues[1] == cardValues[3] || cardValues[2] == cardValues[4]);
        }

        private static int[] GetCardValuesFromHand(Card[] hand)
        {
            int[] cardValues = new int[hand.Length];
            for (int i = 0; i < hand.Length; i++)
            {
                cardValues[i] = hand[i].Value;
            }
            return cardValues;
        }

        private static bool IsStraight(Card[] hand)
        {
            return (hand[4].Value - hand[0].Value == 4);
        }

        private static bool IsFlush(Card[] hand)
        {
            for (int i = 1; i < hand.Length; i++)
            {
                if (hand[i - 1].Suit != hand[i].Suit) return false;
            }
            return true;
        }

        private static bool IsFullHouse(Card[] hand)
        {
            var cardValues = GetCardValuesFromHand(hand);
            // since the cards are sorted.
            return (cardValues[0] == cardValues[1] && cardValues[1] != cardValues[2] && (cardValues[2] == cardValues[4]));
        }

        private static bool IsFourOfAKind(Card[] hand)
        {
            var cardValues = GetCardValuesFromHand(hand);
            return (cardValues[0] == cardValues[3] || cardValues[1] == cardValues[4]);
        }

        private static bool IsStraightFlush(Card[] hand)
        {
            return (IsStraight(hand) && IsFlush(hand));
        }

        #endregion
        #region Equality Helper

        private int CompareHighCard(Hand hand1, Hand hand2)
        {
            Card[] hand1Cards = hand1.HandCards;
            Card[] hand2Cards = hand1.HandCards;
            return hand1Cards[hand1Cards.Length - 1].CompareTo(hand2Cards[hand2Cards.Length - 1]);
        }

        private int CompareHighCardRecursive(Hand hand1, Hand hand2)
        {
            Card[] hand1Cards = hand1.HandCards;
            Card[] hand2Cards = hand2.HandCards;
            int currentCard = hand1Cards.Length - 1;
            int cmp = 0;
            while (currentCard >= 0)
            {
                cmp = hand1Cards[currentCard].CompareTo(hand2Cards[currentCard]);
                // compare the cards and keep comparing them until they are not equal.
                if(cmp != 0) break;
                currentCard--;
            }
            return cmp;
        }

        /// <summary>
        /// Hands which contain the same pair are ranked
        /// by the value of the card forming the pair. If
        /// these values are the same , the hands are ranked by 
        /// the values of the cards not forming the pair.
        /// </summary>
        /// <param name="hand1"></param>
        /// <param name="hand2"></param>
        /// <returns></returns>
        private int ComparePair(Hand hand1, Hand hand2)
        {
            Card[] hand1Cards = hand1.HandCards;
            Card[] hand2Cards = hand2.HandCards;
            ISet<Card> hand1Set =  new HashSet<Card>();
            ISet<Card> hand2Set =  new HashSet<Card>();

            // find the card forming the pair.
            Card hand1PairCard = null;
            Card hand2PairCard = null;
            for (int i = 0; i < hand1Cards.Length; i++)
            {
                if (!hand1Set.Contains(hand1Cards[i])) hand1Set.Add(hand1Cards[i]);

            }

        }

        #endregion

    }
}
