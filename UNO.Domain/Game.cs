using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Threading;

namespace UNO.Domain
{
    public class Game
    {
        public Deck Deck { get; set; }
        public Hand PlayerHand { get; set; }
        public Hand PcHand { get; set; }
        public Card TableCard { get; set; }
        public bool PlayerTurn { get; set; }
        public bool Active { get; set; }

        public Game(Deck deck, Hand playerHand, Hand pcHand)
        {
            Deck = deck;
            PlayerHand = playerHand;
            PcHand = pcHand;
            PlayerTurn = true; // Player always starts
            Active = true;
        }

        public void Start()
        {
            TableCard = Deck.Cards.FirstOrDefault(
                card => card.Type != Card.Types.ActionCard &&
                card.Type != Card.Types.WildCard);
            
            ShowTable();
            
            while (Active == true)
            {
                TakeTurns();
            }
        }
        public void HandEmpty(Hand hand)
        {
            if (!hand.Cards.Any())
            {
                EndGame();
            }
        }
        public void EndGame()
        {
            if (!PlayerHand.Cards.Any())
            {
                Console.WriteLine("---YOU WIN!---");
                Active = false;
            }
            else if (!PcHand.Cards.Any())
            {
                Console.WriteLine("---PC WINS!---");
                Active = false;
            }
        }
        public void ChangeTurn()
        {
            if (PlayerTurn == true)
            {
                PlayerTurn = false;
            }
            else
            {
                PlayerTurn = true;
            }
        }
        public void Wait()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.Write(".");
                Thread.Sleep(1000);
            }
        }
        public void ShowTable()
        {
            Console.WriteLine("\n Table: [ " + TableCard + " ]\n");
        }
        public void PlayerChoose()
        {
            Console.Write("Press card number or Enter to buy: ");
            string index = Console.ReadLine();

            try
            {
                if (index == "" || int.Parse(index) > PlayerHand.Cards.Count - 1)
                {
                    Deck.Take(PlayerHand);
                }
                else
                {
                    Card chosen = PlayerHand.Cards[int.Parse(index)];

                    if (ValidCard(chosen))
                    {
                        TableCard = chosen;
                        PlayerHand.Draw(chosen);
                    }
                    else
                    {
                        Deck.Take(PlayerHand);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("You must choose a number or type Enter.");
            }
        }
        public void PcDraws(Card chosen)
        {
            TableCard = chosen;
            PcHand.Draw(chosen);
            Console.Write("\nPC chooses:");
            Wait();
            Console.Write(" " + chosen + "\n");
        }
        public bool ValidCard(Card card) // For testing purposes it accepts any card
        {
            if (card.Color == TableCard.Color ||
                card.Number == TableCard.Number ||
                card.Type == Card.Types.WildCard ||
                card.Action == TableCard.Action)
            {
                return true;
            }
            return false;
        }
        public void PcChoose()
        {
            // First it verifies if there is a match
            Card chosen = PcHand.Cards.FirstOrDefault(
                card => (card.Number == TableCard.Number && card.Number != Card.Numbers.None) ||
                (card.Color == TableCard.Color && card.Color != Card.Colors.None) ||
                card.Action == TableCard.Action && card.Action != Card.Actions.None);
            
            if (chosen == null)
            {
                // It there isn't any, it verifies if there's a wild card
                chosen = PcHand.Cards.FirstOrDefault(card => card.Type == Card.Types.WildCard);
                if (chosen == null)
                {
                    Deck.Take(PcHand);
                }
                else
                {
                    PcDraws(chosen);
                    HandEmpty(PcHand);
                    PcChooseAny();
                }
            }
            else
            {
                PcDraws(chosen);
            }
        }
        public void PlayerCardEffect()
        {
            if (TableCard.Type == Card.Types.ActionCard)
            {
                if (TableCard.Action == Card.Actions.Skip ||
                    TableCard.Action == Card.Actions.Reverse)
                {
                    return;
                }
                else if (TableCard.Action == Card.Actions.Draw2)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Deck.Take(PcHand);
                    }
                    return;
                }
            }
            else if (TableCard.Type == Card.Types.WildCard)
            {
                if (TableCard.Wild == Card.Wilds.Wild)
                {
                    PlayerChooseAny();
                }
                else if (TableCard.Wild == Card.Wilds.WildDraw4)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Deck.Take(PcHand);
                    }
                    PlayerChooseAny();
                }
            }
            else
            {
                PlayerTurn = false;
            }
        }

        // Under construction
        public void PcCardEffect()
        {
            if (TableCard.Type == Card.Types.ActionCard)
            {
                if (TableCard.Action == Card.Actions.Skip ||
                    TableCard.Action == Card.Actions.Reverse)
                {
                    return;
                }
                else if (TableCard.Action == Card.Actions.Draw2)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Deck.Take(PlayerHand);
                    }
                    return;
                }
            }
            else if (TableCard.Type == Card.Types.WildCard)
            {
                if (TableCard.Wild == Card.Wilds.Wild)
                {
                    PcChooseAny();
                }
                else if (TableCard.Wild == Card.Wilds.WildDraw4)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Deck.Take(PlayerHand);
                    }
                    PcChooseAny();
                }
            }
            else
            {
                PlayerTurn = true;
            }
        }

        public void PlayerChooseAny()
        {
            PlayerHand.Show();
            Console.Write("Press card number or Enter to buy: ");
            string index = Console.ReadLine();

            Choose:
            try
            {
                if (index == "" || int.Parse(index) > PlayerHand.Cards.Count - 1)
                {
                    Deck.Take(PlayerHand);
                    PlayerTurn = false;
                }
                else
                {
                    Card chosen = PlayerHand.Cards[int.Parse(index)];
                    TableCard = chosen;
                    PlayerHand.Draw(chosen);
                    return;
                }
                ShowTable();
            }
            catch (Exception)
            {
                Console.WriteLine("You must choose a number or press Enter.");
                goto Choose;
            }
        }

        public void PcChooseAny()
        {
            if (PcHand.Cards.Any())
            {
                PcHand.Draw(0);
            }
            else
            {
                EndGame();
            }
        }

        public void TakeTurns()
        {
            while (PlayerTurn == true)
            {
                PlayerHand.Show();
                HandEmpty(PlayerHand);
                PlayerChoose();
                ShowTable();
                PlayerCardEffect();
            }
            while (PlayerTurn == false)
            {
                Console.Write("PC Hand: ");
                PcHand.Show();

                HandEmpty(PcHand);
                PcChoose();
                ShowTable();
                PcCardEffect();
            }
        }
    }
}
