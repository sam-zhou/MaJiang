using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaJiang.Extention;
using MaJiang.Model.EnumAttributes;
using MaJiang.Model.Enums;

namespace MaJiang.Model
{
    public class Meld : TileGroup
    {
        public MeldType Type { get; set; }

        public bool IsExposed { get; set; }

        public Tile LastDraw { get; set; }

        public bool IsJiang
        {
            get
            {
                if (Type == MeldType.Eye)
                {
                    if (Tiles.Any(q => q.Rank.GetAttribute<IsJiangAttribute>().IsJiang))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public int TilesCount
        {
            get
            {
                if (Type == MeldType.Eye)
                {
                    return 2;
                }
                return 3;
            }
        }

    }
}
