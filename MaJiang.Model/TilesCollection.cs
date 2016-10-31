using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaJiang.Extention;
using MaJiang.Model.Enums;
using MaJiang.Model.EventArgs;
using MaJiang.Model.Interfaces;

namespace MaJiang.Model
{
    public class TilesCollection: TileGroup, ICountableTileCollection
    {
        public event EventHandler<TileRemovedEventArgs> TileRemoved;

        public event EventHandler TileShuffled;


        public TilesCollection()
        {
            Tiles = new List<Tile>();
            for (int i = 1; i <= 9; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Tiles.Add(new Tile(Suit.Dot, i));
                    Tiles.Add(new Tile(Suit.Bamboo, i));
                    Tiles.Add(new Tile(Suit.Character, i));
                }
            }
        }

        private void RemoveAt(int index)
        {
            if (index < Count)
            {
                Tiles.RemoveAt(index);
                if (TileRemoved != null)
                {
                    TileRemoved(this, new TileRemovedEventArgs(index));
                }
            }
        }

        public void Shuffle()
        {
            Tiles.Shuffle();
            if (TileShuffled != null)
            {
                TileShuffled(this, null);
            }
        }

        public List<Stack> GetStacks()
        {
            var output = new List<Stack>();


            for (int i = 0; i < Count; i += 2)
            {
                if (i + 1 < Count)
                {
                    output.Add(new Stack(new[] { Tiles[i], Tiles[i + 1] }));
                }
                else
                {
                    output.Add(new Stack(new[] { Tiles[i] }));
                }
            }

            return output;
        }

        public Tile GetTileAt(int index)
        {
            if (Count > index && index >= 0)
            {
                var output = Tiles[index];
                RemoveAt(index);

                return output;
            }
            throw new Exception("Tile index out of range");
        }
    }
}
