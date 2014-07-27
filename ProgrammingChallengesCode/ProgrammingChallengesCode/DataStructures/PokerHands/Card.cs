using System;

namespace ProgrammingChallengesCode.DataStructures.PokerHands
{
    /// <summary>
    /// Class to model a card in a game of poker.
    /// </summary>
    public class Card :  IComparable<Card>
    {
        private int Rank { get; set; }
        /// <summary>
        /// Suit value of the card.
        /// </summary>
        public char Suit { get; private set; }
        /// <summary>
        ///  Numerical value associated with the card.
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// String of all possible values assumed by a card.
        /// </summary>
        private const string Values = "23456789TJQKA";
        
        /// <summary>
        /// Suits.
        /// </summary>
        private const string Suits = "CDHS";
        /// <summary>
        /// Constructor initializing an instance of the 
        /// Card class.
        /// </summary>
        /// <param name="str"></param>
        public Card(string str)
        {
            Rank = ComputeRanking(str);
            Value = Rank/Suits.Length;
            Suit = Suits[Rank%Suits.Length];
        }

        /// <summary>
        /// Given a card , with a value and suit information,
        /// compute the rank associated with it.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private int ComputeRanking(string str)
        {
            if(string.IsNullOrWhiteSpace(str) || str.Length !=2) throw new ArgumentException("invalid input");
            char value = str[0];
            char suit = str[1];
            if(!char.IsLetterOrDigit(str[0]) || !char.IsLetter(str[1])) throw new ArgumentException("invalid input");
            for (int i = 0; i < Values.Length; i++)
            {
                if (Values[i] == value)
                {
                    for (int j = 0; j < Suits.Length; j++)
                    {
                        if (Suits[j] == suit)
                        {
                            return (i + 2)*Suits.Length + j;
                        }
                    }
                }
            }
            return -1;
        }
        /// <summary>
        /// Method to compare one card with another.
        /// </summary>
        /// <param name="anotherCard"></param>
        /// <returns></returns>
        public int CompareTo(Card anotherCard)
        {
            if (Value < anotherCard.Value) return -1;
            if (Value > anotherCard.Value) return 1;
            return 0;
        }

        public override bool Equals(object obj)
        {
            Card anotherCard = (Card) obj;
            return Value == anotherCard.Value;

        }

        public override string ToString()
        {
            return "[" + Value + Suit + "]";
        }
    }
}
