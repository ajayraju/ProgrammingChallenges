using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingChallengesCode.DataStructures.StackEmUp
{
    public class Deck
    {
        /// <summary>
        /// Deck card count.
        /// </summary>
        private const int DeckCardCount = 52;
        
        /// <summary>
        /// Collection of cards.
        /// </summary>
        public Card[] Cards { get; set; }

        /// <summary>
        /// Known values in a deck of cards.
        /// </summary>
        private readonly string[] Values =
        {
            "2","3","4","5","6","7","8","9","10","Jack","Queen","King","Ace"
        };

        /// <summary>
        /// Known suits in a deck of cards.
        /// </summary>
        private readonly string[] Suits = {"Clubs", "Diamonds", "Hearts", "Spades"};

        /// <summary>
        /// Initializes a new deck of cards.
        /// </summary>
        public Deck()
        {
            this.Cards =  new Card[DeckCardCount];
            int current = 0;
            for (int i = 0; i < Suits.Length; i++)
            {
                for (int j = 0; j < Values.Length; j++)
                {
                    this.Cards[current++] = new Card(Values[j],Suits[i]);
                }
            }
        }

        /// <summary>
        /// Method which transforms a sorted deck into a shuffled one 
        /// based on the series of shuffles presented.
        /// </summary>
        /// <param name="shuffleOrder">shuffle order.</param>
        /// <param name="shuffles">Shuffle array.</param>
        /// <returns></returns>
        public Deck Shuffle(int shuffleOrder, int[][] shuffles)
        {
            // get a sorted deck of cards.
            Deck tempDeck =  new Deck();
            Card[] newCards =  new Card[DeckCardCount];
            for (int i = 0; i < DeckCardCount; i++)
            {
                // Gets the shuffle order element, based on the zero=based shuffle order element.
                int itemToMove = shuffles[shuffleOrder - 1][i];
                // zero based shuffle item.
                newCards[i] = this.Cards[itemToMove - 1];
            }

            tempDeck.Cards = newCards;
            return tempDeck;
        }

        /// <summary>
        /// Print a deck of cards.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < Cards.Length; i++)
            {
                builder.Append(Cards[i].ToString());
                if (i < Cards.Length - 1) builder.Append("\n");
            }
            return builder.ToString();
        }
    }
}
