using System;

namespace ConsoleApp1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    interface IShufflable
    {
        void Shuffle();
    }

    interface IDrawable
    {
        void Draw();
    }

    class Card { }

    class CardPile : IShufflable, IDrawable
    {
        public void Draw()
        {
            throw new NotImplementedException();
        }

        public void Shuffle()
        {
            Console.WriteLine("Shuffling cards.");
        }
    }

    class Coin { }

    class CoinPile : IShufflable
    {
        public void Shuffle()
        {
            Console.WriteLine("Shuffling coins.");
        }
    }

    class Deck : IShufflable, IDrawable
    {
        private CardPile cp;

        public Deck(CardPile cp)
        {
            this.cp = cp;
        }

        public void Draw()
        {
            cp.Draw();
        }

        public void Shuffle()
        {
            cp.Shuffle();
        }
    }

    class Program
    { 
        static void ShuffleAll(IEnumerable<IShufflable> list)
        {
            foreach (var o in list)
                o.Shuffle();
        }

        static void Print(object o)
        {
            Console.WriteLine(o as int?);
        }

        static void Main(string[] args)
        {
            var cp = new CardPile();
            IShufflable cp2 = cp;
            var deck = new Deck(cp);

            Print(deck);

            var list = new List<IShufflable>();
            list.Add(new CardPile());
            list.Add(new CoinPile());
            list.Add(deck);

            ShuffleAll(list);

            var strings = new List<string>();
            IEnumerable<object> objects = strings;

            Action<IShufflable> act1 = (IShufflable o) => { Console.WriteLine(o); };
            Action<Deck> act2 = act1;

            act1(cp);
            act2(deck);
        }
    }
}
