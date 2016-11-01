using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaJiang.Model.Enums;

namespace MaJiang.Model
{
    public class MeldCollection
    {
        private List<Meld> _melds;
        private List<Tile> _tilesLeft;
        private readonly Tile _draw;

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

        public bool IsPengPengHu
        {
            get
            {
                if (Successful)
                {
                    if (Melds.All(q => q.Type != MeldType.Sequence) && Melds.Count(q => q.Type == MeldType.Eye) == 1)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool IsQiXiaoDui
        {
            get
            {
                if (Successful)
                {
                    if (Melds.Count(q => q.Type == MeldType.Eye) == 7)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public MeldCollection(List<Meld> melds, List<Tile> tilesLeft, Tile draw = null)
        {
            _melds = melds;
            _tilesLeft = tilesLeft;
            _draw = draw;
        }

        public List<Tile> GetAllTiles()
        {
            var output = TilesLeft.ToList();

            foreach (var meld in Melds)
            {
                output.AddRange(meld.Tiles);
            }
            return output;
        } 

        public void CreateMeld(Meld meld)
        {
            Melds.Add(meld);
            foreach (var tile in meld.Tiles)
            {
                var found = TilesLeft.FirstOrDefault(q => q.Rank == tile.Rank && q.Suit == tile.Suit);
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
