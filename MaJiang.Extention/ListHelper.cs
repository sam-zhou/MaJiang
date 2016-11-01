using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaJiang.Extention
{
    public static class ListHelper
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

        public static IList<IList<TSource>> GetChildSets<TSource>(this IList<TSource> a, IList<TSource> b = null, int i = 1)
        {
            var output = new List<IList<TSource>>();

            if (b == null)
            {
                b = new List<TSource>();
            }

            if (i > a.Count) 
            {
                output.Add(b.ToList());
            }
            else
            {
                var x = a[i - 1];

                b.Add(x);
                output.AddRange(a.GetChildSets(b, i + 1));
                b.Remove(x);
                output.AddRange(a.GetChildSets(b, i + 1));
            }

            return output;
        }
    }
}
