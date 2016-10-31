using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaJiang.Model.Enums;

namespace MaJiang.Model
{
    public class WinningTile
    {
        private readonly Tile _tile;
        private readonly WinType _type;

        public Tile Tile
        {
            get { return _tile; }
        }

        public WinType Type
        {
            get { return _type; }
        }

        public WinningTile(Tile tile, WinType type)
        {
            _tile = tile;
            _type = type;
        }

        public override string ToString()
        {
            return string.Format("{0} win by {1}", Type, Tile);
        }
    }
}
