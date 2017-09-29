namespace ProgrammingChallengesCode.DataStructures.StackEmUp
{
    public class Card
    {
        /// <summary>Suit of the card./// 
        /// </summary>
        private string Suit;
       
        private string Value;

        public Card(string value, string suit)
        {
            this.Value = value;
            this.Suit = suit;
        }

        public override string ToString()
        {
            return this.Value + "" + this.Suit;
        }
    }
}
