using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    if (Tiles.Any(q => q.Rank.Value == 5 || q.Rank.Value == 8 || q.Rank.Value == 2))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

    }
}
