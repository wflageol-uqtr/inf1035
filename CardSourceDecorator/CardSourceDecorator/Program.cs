using System;

namespace CardSourceDecorator
{
    interface ICardSource
    {
        bool IsEmpty();
        Card Draw();
    }

    class Card { }

    class Deck : ICardSource
    {
        public Card Draw()
        {
            Console.WriteLine("Drew a card.");
            return new Card();
        }

        public bool IsEmpty() => false;
    }

    class JournalizingSource : ICardSource
    {
        private ICardSource source;

        public JournalizingSource(ICardSource source)
        {
            this.source = source;
        }

        public Card Draw()
        {
            var card = source.Draw();
            Console.WriteLine("Logged card.");
            return card;
        }

        public bool IsEmpty() => source.IsEmpty();
    }


    class MemorizingSource : ICardSource
    {
        private ICardSource source;

        public MemorizingSource(ICardSource source)
        {
            this.source = source;
        }

        public Card Draw()
        {
            var card = source.Draw();
            Console.WriteLine("Memorized card.");
            return card;
        }

        public bool IsEmpty() => source.IsEmpty();
    }

    class Program
    {
        static void Main(string[] args)
        {
            ICardSource deck = new Deck();
            ICardSource journalizingDeck = new JournalizingSource(deck);
            journalizingDeck = new MemorizingSource(journalizingDeck);

            deck.Draw();
            journalizingDeck.Draw();
        }
    }
}
