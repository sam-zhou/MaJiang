using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaJiang.Model
{
    public class Suit : IComparable
    {
        public string Name { get; }

        public Suit(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            var anotherSuit = obj as Suit;
            if (anotherSuit != null)
            {
                
                return string.Compare(Name, anotherSuit.Name, StringComparison.Ordinal);
            }
            else
            {
                throw new ArgumentException("Object is not a Suit");
            }
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                var p = (Suit)obj;
                return p.Name == Name;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static Suit Dot
        {
            get
            {
                return new Suit("饼");
            }
        }

        public static Suit Bamboo
        {
            get
            {
                return new Suit("条");
            }
        }

        public static Suit Character {
            get
            {
                return new Suit("万");
            }
        }
    }
}
