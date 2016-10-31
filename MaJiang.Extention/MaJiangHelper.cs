using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaJiang.Extention
{
    public static class MaJiangHelper
    {
        private static readonly Random Random = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = Random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static int GetRandomDice()
        {
            var rInt = new Random().Next(1, 6); //for ints
            return rInt;
        }

        public static int GetRandomTwoDices()
        {
            return GetRandomDice() + GetRandomDice();
        }

        public static string GetString<T>(this IEnumerable<T> list)
        {
            var output = string.Empty;
            foreach (var item in list)
            {
                if (output != string.Empty)
                {
                    output += ", ";
                }
                output += item.ToString();
            }
            return output;
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
