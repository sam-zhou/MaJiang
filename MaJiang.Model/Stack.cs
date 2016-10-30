using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaJiang.Model
{
    public class Stack: TileGroup
    {
        public Stack(Tile [] tiles)
        {
            foreach (var tile in tiles)
            {
                Tiles.Add(tile);
            }
        }



        public void RemoveTile()
        {
            if (!IsEmpty)
            {
                Tiles.RemoveAt(0);
            }
            else
            {
                throw new Exception("Cannot remove tile");
            }
        }

        public void RemoveTileAt(int index)
        {
            if (!IsEmpty && Tiles.Count > index)
            {
                Tiles.RemoveAt(index);
            }
            else
            {
                throw new Exception("Cannot remove tile");
            }
        }

        public bool IsEmpty
        {
            get { return Tiles.Count == 0; }
        }
    }
}
