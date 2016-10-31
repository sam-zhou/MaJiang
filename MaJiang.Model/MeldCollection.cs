using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaJiang.Model
{
    public class MeldCollection
    {
        private List<Meld> _melds;
        private List<Tile> _tilesLeft;
        private readonly Tile _draw;
        private WinningTile _winningTile;

        public List<Meld> Melds
        {
            get
            {
                if (_melds == null)
                {
                    _melds = new List<Meld>();
                }
                return _melds;
            }
        }

        public Tile Draw
        {
            get { return _draw; }
        }

        public List<Tile> TilesLeft
        {
            get
            {
                if (_tilesLeft == null)
                {
                    _tilesLeft = new List<Tile>();
                }
                return _tilesLeft;
            }
        }

        public WinningTile WinningTile { get; set; }

        public bool Successful
        {
            get { return TilesLeft.Count == 0 && Melds.Any(); }
        }

        public MeldCollection(List<Meld> melds, List<Tile> tilesLeft, Tile draw = null)
        {
            _melds = melds;
            _tilesLeft = tilesLeft;
            _draw = draw;
        }

        public void CreateMeld(Meld meld)
        {
            Melds.Add(meld);
            foreach (var tile in meld.Tiles)
            {
                var found = TilesLeft.FirstOrDefault(q => q.Rank.Equals(tile.Rank) && q.Suit.Equals(tile.Suit));
                if (found != null)
                {
                    TilesLeft.Remove(found);
                }
                else
                {
                    throw new Exception("Could not create meld");
                }
            }
        }
    }
}
