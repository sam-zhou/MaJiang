using System;
using System.Collections.Generic;
using System.Linq;
using MaJiang.Core.WinProcessors;
using MaJiang.Model;
using MaJiang.Model.Enums;
using MaJiang.Model.EventArgs;
using MaJiang.Model.Interfaces;

namespace MaJiang.Core
{
    public class Player : TileGroup, IPlayerAction
    {
        private List<Meld> _melds;

        public event EventHandler<PlayerActionEventArgs> PlayerActionable;
        public event EventHandler<PlayerActionEventArgs> PlayerAction;
        public event EventHandler<PlayerWinEventArgs> PlayerWinable;
        public event EventHandler<PlayerInitialWinEventArgs> PlayerInitalWin;

        public string Name { get; private set; }

        private WinProcessorFactory WinProcessorFactory { get; set; }

        public string Id { get; private set; }

        public PlayerPositions PlayerPosition { get; private set; }

        public bool IsDealer { get; set; }

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
            private set { _melds = value; }
        }

        public Player(string name, string id)
        {
            Id = id;
            Name = name;
            WinProcessorFactory = new WinProcessorFactory();
        }

        public void SetDirection(PlayerPositions positions)
        {
            PlayerPosition = positions;
        }

        private void Order()
        {
            Tiles = Tiles.OrderBy(q => q.Suit).ThenBy(q => q.Rank).ToList();
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Name, base.ToString());
        }

        public void Reset()
        {
            Tiles = null;
            Melds = null;
        }

        public void InitialDraw(List<Tile> tiles)
        {
            Tiles.AddRange(tiles);


            if (PlayerInitalWin != null && Tiles.Count == (IsDealer? 14: 13))
            {

                var kongMelds = MaJiangAlgorithm.GetKongs(Tiles);

                if (kongMelds.Any())
                {
                    PlayerInitalWin(this, new PlayerInitialWinEventArgs(InitialWinType.DaSiXi, kongMelds));
                }

                var triplets = MaJiangAlgorithm.GetTriplets(Tiles);
                if (triplets.Count >= 2)
                {
                    PlayerInitalWin(this, new PlayerInitialWinEventArgs(InitialWinType.LiuLiuShun, triplets));
                }

                var lackSuits = MaJiangAlgorithm.GetLackSuits(Tiles);
                if (lackSuits.Count > 0)
                {
                    PlayerInitalWin(this, new PlayerInitialWinEventArgs(InitialWinType.QueYiSe, lackSuits));
                }

                if (MaJiangAlgorithm.IsBanBanHu(Tiles))
                {
                    PlayerInitalWin(this, new PlayerInitialWinEventArgs(InitialWinType.BanBanHu));
                }
            }



            Order();
        }

        public void Draw(Tile tile)
        {
            Tiles.Add(tile);
            
        }

        public void Pong(Tile tile)
        {
            Tiles.Add(tile);
            if (PlayerAction != null)
            {
                PlayerAction(this, new PlayerActionEventArgs(PlayerActions.Pong, new List<Meld>
                {
                    new Meld(new List<Tile>
                    {
                        tile, tile, tile
                    }, MeldType.Triplet, tile)
                }));
            }
        }

        public void Kong(Tile tile)
        {
            Tiles.Add(tile);
            if (PlayerAction != null)
            {
                PlayerAction(this, new PlayerActionEventArgs(PlayerActions.Pong, new List<Meld>
                {
                    new Meld(new List<Tile>
                    {
                        tile, tile, tile, tile
                    }, MeldType.Kong, tile)
                }));
            }
        }

        public void Chow(Tile tile)
        {
            Tiles.Add(tile);
        }

        public void Discard(Tile tile)
        {
            Tiles.Remove(tile);

            Order();

            if (PlayerAction != null)
            {
                PlayerAction(this, new PlayerActionEventArgs(PlayerActions.Discard, tile));
            }
        }

        public void DiscardByOther(Tile tile, bool chowable = false)
        {
            var winProcessorFactory = new WinProcessorFactory();
            var result = winProcessorFactory.Validate(Tiles, new List<Tile> { tile });

            if (result.Any() && PlayerWinable != null)
            {
                PlayerWinable(this, new PlayerWinEventArgs { WinningTiles = result });
            }

            var kongableMelds = MaJiangAlgorithm.GetKongableMelds(Tiles, tile);

            if (kongableMelds.Any())
            {
                FireEvent(PlayerActions.Kong, kongableMelds, tile);
            }

            var pongableMelds = MaJiangAlgorithm.GetPongableMelds(Tiles, tile);
            if (pongableMelds.Any())
            {
                FireEvent(PlayerActions.Pong, pongableMelds, tile);
            }

            if (chowable)
            {
                var chowableMelds = MaJiangAlgorithm.GetChowableMelds(Tiles, tile);
                if (chowableMelds.Any())
                {
                    FireEvent(PlayerActions.Chow, chowableMelds, tile);
                }
            }
        }

        private void FireEvent(PlayerActions actions, IEnumerable<Meld> melds, Tile actionOnTile)
        {
            if (PlayerActionable != null)
            {
                PlayerActionable(this, new PlayerActionEventArgs(actions, melds, actionOnTile));
            }
        }
    }
}
