using System.ComponentModel;

namespace MaJiang.Model.Enums
{
    public enum PlayerActions
    {
        [Description("吃")]
        Chow = 1,

        [Description("碰")]
        Pong = 2,

        [Description("杠")]
        Kong = 3,

        [Description("胡")]
        Win = 4,

        [Description("出牌")]
        Discard = 0,
    }
}
