using System;
using System.Collections.Generic;
using System.Text;

namespace UNO.Domain
{
    public class Deck
    {
        public List<Card> Cards { get; set; }

        public Deck()
        {
            Cards = new List<Card>();

            // 1 Zero card for each color
            foreach (Card.Colors color in Enum.GetValues(typeof(Card.Colors)))
            {
                if (color != Card.Colors.None)
                {
                    Cards.Add(new Card(Card.Numbers.Zero, color));
                }
            }

            // 2 numbered (1-9) cards for each color
            foreach (Card.Colors color in Enum.GetValues(typeof(Card.Colors)))
            {
                foreach (Card.Numbers number in Enum.GetValues(typeof(Card.Numbers)))
                {
                    if (number != Card.Numbers.None && number != Card.Numbers.Zero && color != Card.Colors.None)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            Cards.Add(new Card(number, color));
                        }
                    }
                }
            }

            // 2 of each action card for each color
            foreach (Card.Colors color in Enum.GetValues(typeof(Card.Colors)))
            {
                foreach (Card.Actions action in Enum.GetValues(typeof(Card.Actions)))
                {
                    if (color != Card.Colors.None && action != Card.Actions.None)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            Cards.Add(new Card(color, action));
                        }
                    }
                }
            }

            // 4 of each wild card
            for (int j = 0; j < 4; j++)
            {
                foreach (Card.Wilds wild in Enum.GetValues(typeof(Card.Wilds)))
                {
                    if (wild != Card.Wilds.None)
                    {
                        Cards.Add(new Card(wild));
                    }
                }
            }
        }

        public void Show()
        {
            foreach (Card card in Cards)
            {
                Console.WriteLine(card);
            }
        }
        public int Size()
        {
            return Cards.Count;
        }
        public void Shuffle()
        {
            Random random = new Random();
            for (int i = Cards.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                Card temp = Cards[i];
                Cards[i] = Cards[j];
                Cards[j] = temp;
            }
        }
        public void Distribute(Hand playerHand, Hand pcHand)
        {
            for (int i = Cards.Count - 1; i >= 101; i--)
            {
                playerHand.Cards.Add(Cards[i]);
                Cards.RemoveAt(i);
            }
            for (int i = Cards.Count - 1; i >= 94; i--)
            {
                pcHand.Cards.Add(Cards[i]);
                Cards.RemoveAt(i);
            }
        }     
        public void Take(Hand hand)
        {
            hand.Cards.Add(Cards[0]); // Dando erros quando as cartas do PC esgotam
            Cards.RemoveAt(0);
        }
        public Card Take()
        {
            Card card = Cards[0];
            Cards.RemoveAt(0);
            return card;
        }
    }
}
