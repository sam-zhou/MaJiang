using System;

namespace MaJiang.Model.Enums
{
    public class Suit : IComparable
    {
        public string Name { get; private set; }

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
            if (Name == "饼")
            {
                return 1;
            }
            if (Name == "条")
            {
                return 2;
            }
            if (Name == "万")
            {
                return 3;
            }

            return 0;
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
