using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpressionsLambda
{
    class Program
    {
        static void Main(string[] args)
        {
            Action action = () => Console.WriteLine("Hello Lambda");
            Action<bool> action2 = b => Console.WriteLine(b);
          
            Func<int, bool> evenFilter = i => i % 2 == 0;

            var integers = new int[] { 1, 1, 3, 3, 3, 5, 6 };

            integers
                .Distinct()
                .OrderByDescending(orderingI => integers.Count(countingI => countingI == orderingI))
                .Iterate(Console.WriteLine);
        }
    }
    static class LinqExtension
    {
        public static void Iterate<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var element in list)
                action(element);
        }
    }
}
