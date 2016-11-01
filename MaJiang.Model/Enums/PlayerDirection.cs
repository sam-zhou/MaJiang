using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaJiang.Model.Enums
{
    public enum PlayerDirection
    {
        [Description("东")]
        East = 0,

        [Description("北")]
        North =1,

        [Description("西")]
        West = 2,

        [Description("南")]
        South = 3
    }

    public static class PlayerDirectionHelper
    {
        public static PlayerDirection GetNextDirection(this PlayerDirection direction)
        {
            switch (direction)
            {
                case PlayerDirection.East:
                    return PlayerDirection.North;
                case PlayerDirection.North:
                    return PlayerDirection.West;
                case PlayerDirection.West:
                    return PlayerDirection.South;
                case PlayerDirection.South:
                    return PlayerDirection.East;
                default:
                    throw new Exception("Direction not found");
            }
        }

    }
}
