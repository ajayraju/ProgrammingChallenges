using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingChallengesCode.DataStructures.PokerHands
{
    /// <summary>
    ///  Given two poker hands, evaluate the winner.
    /// </summary>
    public class PokerHands
    {
        
    }

    public class Card :  IComparable<Card>
    {
        public int Rank { get; set; }
        public char Suit { get; set; }
        public int Value { get; set; }

        private const string Values = "23456789TJQKA";
        private const string Suits = "CDHS";
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
