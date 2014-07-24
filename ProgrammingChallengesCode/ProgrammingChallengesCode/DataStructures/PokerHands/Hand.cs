using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
        /// Options of hand rank vary from being :  
        /// a. Hand Rank.
        /// b. Pair :  Two of the five cards in the hand have the same value.
        /// c. Two pair :  The hand contains two different pairs.
        /// d. Three of a kind :  Three of the cards in the hand have the same value.
        /// e. Straight :  Hand contains five cards with consecutive values.
        /// f. Flush : Hand contains five cards of the same suit.
        /// g. Full House :  Hand contains, three cards of the same value , with the other two forming a pair.
        /// h. Four of a Kind : Four cards with the same value.
        /// i. Straight Flush: Five cards of the same suit with consecutive values.
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
                else hand1PairCard = hand1Cards[i];

            }
            for (int i = 0; i < hand2Cards.Length; i++)
            {
                if (!hand2Set.Contains(hand2Cards[i])) hand2Set.Add(hand2Cards[i]);
                else hand2PairCard = hand2Cards[i];
            }

            // compare the pair cards. If pair cards are equal, keep comparing the cards not forming 
            // the pair.
            if (hand1PairCard != null && hand1PairCard.CompareTo(hand2PairCard) != 0)
                return hand1PairCard.CompareTo(hand2PairCard);
            else
            {
                // keep comparing the rest of the cards. i.e keep iterating over the elements of the set.
                while (hand1Set.Count() != 0 && hand2Set.Count() != 0)
                {
                    Card tempHand1Max = hand1Set.Max();
                    Card tempHand2Max = hand2Set.Max();
                    if (tempHand1Max.CompareTo(tempHand2Max) != 0) return tempHand1Max.CompareTo(tempHand2Max);
                    hand1Set.Remove(tempHand1Max);
                    hand2Set.Remove(tempHand2Max);
                }
            }
            return -1;
        }

        

        /// <summary>
        /// The hand contains two different pairs. Hands which both contain the same pairs
        /// are ranked by the value of their highest pair. 
        /// Hands with the same high pair are ranked by the value of the other pair.
        /// If this value is same as well, the hands are ranked by the value of the remaining card.
        /// </summary>
        /// <param name="hand1"></param>
        /// <param name="hand2"></param>
        /// <returns></returns>
        private int CompareTwoPair(Hand hand1, Hand hand2)
        {

            // pseudocode : Find the bigger pair, smaller pair and the remaining card.
            // Compare the bigger pair, followed by the smaller pair, followed by the remaining card.
            Card hand1BiggerCard = null ;
            Card hand2BiggerCard  = null;
            Card hand1SmallerCard  = null;
            Card hand2SmallerCard = null;
            Card hand1RemainingCard = null;
            Card hand2RemainingCard  = null;
            
            OrderCardsInHand(hand1, ref hand1BiggerCard, ref hand1SmallerCard, ref hand1RemainingCard);
            OrderCardsInHand(hand2, ref hand2BiggerCard, ref hand2SmallerCard, ref hand2RemainingCard);
            
            if (hand1BiggerCard != null && hand2BiggerCard != null && hand1BiggerCard.CompareTo(hand2BiggerCard) != 0)
                return hand1BiggerCard.CompareTo(hand2BiggerCard);
            
            else if (hand1SmallerCard != null && hand2SmallerCard != null &&
                     hand1SmallerCard.CompareTo(hand2SmallerCard) != 0)
                return hand1SmallerCard.CompareTo(hand2SmallerCard);
            
            return hand1RemainingCard.CompareTo(hand2RemainingCard);
        }

        /// <summary>
        /// Private helper used to find the bigger and smaller cards in a hand forming
        /// a two pair.
        /// </summary>
        /// <param name="hand1"></param>
        /// <param name="biggerCard"></param>
        /// <param name="smallerCard"></param>
        /// <param name="remainingCard"></param>
        private static void OrderCardsInHand(Hand hand1, ref Card biggerCard, ref Card smallerCard,
            ref Card remainingCard)
        {

            /*
             * Find the cards which form the double pair.
             * Isolate the card from the bigger pair and the smaller pair and compare them.
             * Find the biggerCard and the smaller card and the remaining card.
             * */
            Card[] hand1Cards = hand1.HandCards;
            int[] hand1CardValues = Hand.GetCardValuesFromHand(hand1Cards);
            
            // finding the bigger card.
            if (hand1CardValues[0] == hand1CardValues[1] && hand1CardValues[2] == hand1CardValues[3] &&
                hand1CardValues[1] != hand1CardValues[2] && hand1CardValues[3] != hand1CardValues[4])
            {
                if (hand1CardValues[0] > hand1CardValues[2])
                {
                    biggerCard = hand1Cards[0];
                    smallerCard = hand1Cards[2];
                }
                else
                {
                    biggerCard = hand1Cards[2];
                    smallerCard = hand1Cards[0];
                }
                remainingCard = hand1Cards[4];
            }
            else if (hand1CardValues[0] != hand1CardValues[1] && hand1CardValues[1] == hand1CardValues[2] &&
                     hand1CardValues[3] == hand1CardValues[4] && hand1CardValues[2] != hand1CardValues[3])
            {
                if (hand1CardValues[1] > hand1CardValues[3])
                {
                    biggerCard = hand1Cards[1];
                    smallerCard = hand1Cards[3];
                }
                else
                {
                    biggerCard = hand1Cards[3];
                    smallerCard = hand1Cards[1];
                }
                remainingCard = hand1Cards[0];
            }
        }

        /// <summary>
        /// Ranked by the value of the four cards. Return the card
        /// with the same value.
        /// </summary>
        /// <param name="tempHand"></param>
        /// <returns></returns>
        private Card CompareFourOfAKind(Hand tempHand)
        {
            Card[] tempCards = tempHand.HandCards;
            if (tempCards[0].Equals(tempCards[3])) return tempCards[0];
            else if (tempCards[1].Equals(tempCards[4])) return tempCards[1];
            return null;
        }
        /// <summary>
        /// Ranked by the values of the three cards. Return the card
        /// with the same value.
        /// </summary>
        /// <param name="tempHand"></param>
        /// <returns></returns>
        private Card CompareThreeOfAKind(Hand tempHand)
        {
            Card[] tempCards = tempHand.HandCards;
            if (tempCards[0].Equals(tempCards[2])) return tempCards[0];
            else if (tempCards[1].Equals(tempCards[3])) return tempCards[1];
            else if (tempCards[2].Equals(tempCards[4])) return tempCards[2];
            return null;
        }

        

        #endregion
        /// <summary>
        /// Method to determine the winner , give two hands.
        /// </summary>
        /// <param name="hand1"></param>
        /// <param name="hand2"></param>
        /// <returns></returns>
        private string GetWinner(Hand hand1, Hand hand2)
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
                    cmp = CompareHighCard(hand1, hand2);
                    break;
                case HandRank.FourOfAKind:
                    cmp = CompareFourOfAKind(hand1).CompareTo(CompareFourOfAKind(hand2));
                    break;
                case HandRank.ThreeOfAKind:
                    cmp = CompareThreeOfAKind(hand1).CompareTo(CompareThreeOfAKind(hand2));
                    break;
                case HandRank.FullHouse:
                    cmp = CompareThreeOfAKind(hand1).CompareTo(CompareThreeOfAKind(hand2));
                    break;
                case HandRank.Flush:
                    cmp = CompareHighCardRecursive(hand1, hand2);
                    break;
                case HandRank.Straight:
                    cmp = CompareHighCard(hand1, hand2);
                    break;
                case HandRank.TwoPair :
                    cmp = CompareTwoPair(hand1, hand2);
                    break;
                case HandRank.Pair:
                    cmp = ComparePair(hand1, hand2);
                    break;
                case HandRank.HighCard :
                    cmp = CompareHighCard(hand1, hand2);
                    break;
                default:
                    break;
            }
            return (cmp > 0) ? hand1.HandName : cmp < 0 ? hand2.HandName : "Tie";

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

    }
}
