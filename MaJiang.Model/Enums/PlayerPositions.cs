using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaJiang.Model.Enums
{
    public enum PlayerPositions
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
        public static PlayerPositions GetNextPosition(this PlayerPositions positions)
        {
            switch (positions)
            {
                case PlayerPositions.East:
                    return PlayerPositions.North;
                case PlayerPositions.North:
                    return PlayerPositions.West;
                case PlayerPositions.West:
                    return PlayerPositions.South;
                case PlayerPositions.South:
                    return PlayerPositions.East;
                default:
                    throw new Exception("PlayerPosition not found");
            }
        }


        public static int GetBetween(this PlayerPositions playerPosition, PlayerPositions currentPlayerPosition)
        {
            var between = (int) playerPosition - (int) currentPlayerPosition;

            if (between < 0)
            {
                between += 4;
            }

            return between;
        }
    }
}
