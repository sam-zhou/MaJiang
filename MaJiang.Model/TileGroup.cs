using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaJiang.Model
{
    public class TileGroup
    {
        private List<Tile> _tiles;

        public List<Tile> Tiles
        {
            get
            {
                if (_tiles == null)
                {
                    _tiles = new List<Tile>();
                }
                return _tiles;
            }
            set { _tiles = value; }
        }

        public int Count
        {
            get { return Tiles.Count; }
        }

        public override string ToString()
        {
            var output = string.Empty;
            foreach (var item in Tiles)
            {
                if (output != string.Empty)
                {
                    output += ", ";
                }
                output += item.ToString();
            }
            return output;
        }

    }
}
