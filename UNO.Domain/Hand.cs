using System;
using System.Collections.Generic;
using System.Text;

namespace UNO.Domain
{
    public class Hand
    {
        public List<Card> Cards { get; set; }

        public Hand()
        {
            Cards = new List<Card>();
        }

        public void Draw(int index)
        {
            Cards.RemoveAt(index);
        }
        public void Draw(Card card)
        {
            Cards.Remove(card);
        }
        public void Show()
        {
            int i = 0;
            foreach (Card card in Cards)
            {
                Console.Write(i + "-[" + card + "] ");
                i++;
            }
            Console.WriteLine();
        }
    }
}
