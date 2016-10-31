using System;

namespace MaJiang.Model.Enums
{
    public class Rank : IComparable
    {
        private readonly string _value;

        public int Value { get; private set; }

        public Rank(int value)
        {
            Value = value;
            switch (value)
            {
                case 1:
                    _value = "一";
                    break;
                case 2:
                    _value = "二";
                    break;
                case 3:
                    _value = "三";
                    break;
                case 4:
                    _value = "四";
                    break;
                case 5:
                    _value = "五";
                    break;
                case 6:
                    _value = "六";
                    break;
                case 7:
                    _value = "七";
                    break;
                case 8:
                    _value = "八";
                    break;
                case 9:
                    _value = "九";
                    break;
                default:
                    throw new Exception("Value out of range");
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
                var p = (Rank)obj;
                return p.Value == Value;
            }
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public override string ToString()
        {
            return _value;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            var anotherRank = obj as Rank;
            if (anotherRank != null)
            {

                return Value.CompareTo(anotherRank.Value);
            }
            else
            {
                throw new ArgumentException("Object is not a Rank");
            }
        }



        public static Rank Zero
        {
            get { return new Rank(0);}
        }

        public static Rank One
        {
            get { return new Rank(1); }
        }

        public static Rank Two
        {
            get { return new Rank(2); }
        }

        public static Rank Three
        {
            get { return new Rank(3); }
        }

        public static Rank Four
        {
            get { return new Rank(4); }
        }

        public static Rank Five
        {
            get { return new Rank(5); }
        }

        public static Rank Six
        {
            get { return new Rank(6); }
        }

        public static Rank Seven
        {
            get { return new Rank(7); }
        }

        public static Rank Eight
        {
            get { return new Rank(8); }
        }

        public static Rank Nine
        {
            get { return new Rank(9); }
        }

        public static Rank GetTileValue(int value)
        {
            switch (value)
            {
                case 1:
                    return One;
                case 2:
                    return Two;
                case 3:
                    return Three;
                case 4:
                    return Four;
                case 5:
                    return Five;
                case 6:
                    return Six;
                case 7:
                    return Seven;
                case 8:
                    return Eight;
                case 9:
                    return Nine;
                default:
                    throw new Exception("Value out of range");
            }
        }
    }
}