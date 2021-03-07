using System;
using UNO.Domain;

namespace UNO
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();
            deck.Shuffle();

            Hand playerHand = new Hand();
            Hand pcHand = new Hand();
            deck.Distribute(playerHand, pcHand);

            Game game = new Game(deck, playerHand, pcHand);

            game.Start();
        }
    }
}
