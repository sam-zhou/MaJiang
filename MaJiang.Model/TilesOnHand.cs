using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MaJiang.Model.Annotations;
using MaJiang.Model.EventArgs;
using MaJiang.Model.Interfaces;

namespace MaJiang.Model
{
    public class TilesOnHand : TileGroup, IPlayerAction
    {

        private List<Meld> _melds;
        private bool _isWinable;
        private bool _isPongable;
        private bool _isChowable;
        private bool _isKongable;

        public event EventHandler<PlayerActionableEventArgs> PlayerActionable;


        public bool IsWinable
        {
            get { return _isWinable; }
            set
            {
                _isWinable = value;
                if (_isWinable)
                {
                    if (PlayerActionable != null)
                    {
                        PlayerActionable(this, new PlayerActionableEventArgs(PlayerAction.Win));
                    }
                }
            }
        }

        public bool IsPongable
        {
            get { return _isPongable; }
            set
            {
                _isPongable = value;
                if (_isPongable)
                {
                    if (PlayerActionable != null)
                    {
                        PlayerActionable(this, new PlayerActionableEventArgs(PlayerAction.Pong));
                    }
                }
            }
        }

        public bool IsChowable
        {
            get { return _isChowable; }
            set
            {
                _isChowable = value;
                if (_isChowable)
                {
                    if (PlayerActionable != null)
                    {
                        PlayerActionable(this, new PlayerActionableEventArgs(PlayerAction.Chow));
                    }
                }
            }
        }

        public bool IsKongable
        {
            get { return _isKongable; }
            set
            {
                _isKongable = value;
                if (_isKongable)
                {
                    if (PlayerActionable != null)
                    {
                        PlayerActionable(this, new PlayerActionableEventArgs(PlayerAction.Kong));
                    }
                }
            }
        }

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



        private void Order()
        {
            Tiles = Tiles.OrderBy(q => q.Suit).ThenBy(q => q.Rank).ToList();
        }

        public void InitialDraw(List<Tile> tiles)
        {
            Tiles.AddRange(tiles);
            Order();
        }

        public void DiscardByOther(Tile tile)
        {
            var list = new List<Tile>();

            list = Tiles.ToList();

            list.Add(tile);

            list = list.OrderBy(q => q.Suit).ThenBy(q => q.Rank).ToList();

            Tile tmp = null;
            var count = 1;
            foreach (var listTile in list)
            {
                if (!listTile.Equals(tmp))
                {
                    count = 1;
                    tmp = listTile;
                }
                else
                {
                    count ++;
                }

                if (count == 4)
                {
                    IsKongable = true;
                }
            }
        }

        public void Discard(Tile tile)
        {
            Tiles.Remove(tile);

            Order();
        }

        public void Draw(Tile tile)
        {
            Tiles.Add(tile);
        }



        public void Pong(Tile tile)
        {
            Tiles.Add(tile);
        }

        public void Kong(Tile tile)
        {
            Tiles.Add(tile);
        }

        public void Chow(Tile tile)
        {
            Tiles.Add(tile);
        }
    }
}