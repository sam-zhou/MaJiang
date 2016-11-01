using System;
using System.ComponentModel;
using MaJiang.Model.EnumAttributes;

namespace MaJiang.Model.Enums
{
    public enum Rank
    {
        [Description("一")]
        [IsJiang(false)]
        One = 1,

        [Description("二")]
        [IsJiang(true)]
        Two = 2,

        [Description("三")]
        [IsJiang(false)]
        Three = 3,

        [Description("四")]
        [IsJiang(false)]
        Four = 4,

        [Description("五")]
        [IsJiang(true)]
        Five = 5,

        [Description("六")]
        [IsJiang(false)]
        Six = 6,

        [Description("七")]
        [IsJiang(false)]
        Seven = 7,

        [Description("八")]
        [IsJiang(true)]
        Eight = 8,

        [Description("九")]
        [IsJiang(false)]
        Nine = 9
 
    }
}