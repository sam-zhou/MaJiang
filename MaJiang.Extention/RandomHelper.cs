using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaJiang.Extention
{
    public static class RandomHelper
    {

        public static int GetRandomDice()
        {
            var rInt = new Random().Next(1, 6); //for ints
            return rInt;
        }

        public static int GetRandomTwoDices()
        {
            return GetRandomDice() + GetRandomDice();
        }
    }
}
