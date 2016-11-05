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

        public event EventHandler<PlayerActionableEventArgs> PlayerActionable;
        public event EventHandler<PlayerWinEventArgs> PlayerWin;
        public event EventHandler<PlayerInitialWinEventArgs> PlayerInitalWin;
        public event EventHandler<PlayerDiscardEventArgs> PlayerDiscard;

        public string Name { get; private set; }

        private WinProcessorFactory WinProcessorFactory { get; set; }

        public string Id { get; private set; }

        public PlayerDirection PlayerDirection { get; private set; }

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

        public void SetDirection(PlayerDirection direction)
        {
            PlayerDirection = direction;
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


            if (PlayerInitalWin != null)
            {

                var kongMelds = MaJiangAlgorithm.GetKongs(tiles);

                if (kongMelds.Any())
                {
                    PlayerInitalWin(this, new PlayerInitialWinEventArgs(InitialWinType.DaSiXi, kongMelds));
                }

                var triplets = MaJiangAlgorithm.GetTriplets(tiles);
                if (triplets.Count >= 2)
                {
                    PlayerInitalWin(this, new PlayerInitialWinEventArgs(InitialWinType.LiuLiuShun, triplets));
                }

                var lackSuits = MaJiangAlgorithm.GetLackSuits(tiles);
                if (lackSuits.Count > 0)
                {
                    PlayerInitalWin(this, new PlayerInitialWinEventArgs(InitialWinType.QueYiSe, lackSuits));
                }

                if (MaJiangAlgorithm.IsBanBanHu(tiles))
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
        }

        public void Kong(Tile tile)
        {
            Tiles.Add(tile);
        }

        public void Chow(Tile tile)
        {
            Tiles.Add(tile);
        }

        public void Discard(Tile tile)
        {
            Tiles.Remove(tile);

            Order();

            if (PlayerDiscard != null)
            {
                PlayerDiscard(this, new PlayerDiscardEventArgs
                {
                    Tile = tile
                });
            }
        }

        public void DiscardByOther(Tile tile, bool chowable = false)
        {
            var winProcessorFactory = new WinProcessorFactory();
            var result = winProcessorFactory.Validate(Tiles, new List<Tile> { tile });

            if (result.Any() && PlayerWin != null)
            {
                PlayerWin(this, new PlayerWinEventArgs { WinningTiles = result });
            }

            var kongableMelds = MaJiangAlgorithm.GetKongableMelds(Tiles, tile);

            if (kongableMelds.Any())
            {
                FireEvent(PlayerAction.Kong, kongableMelds, tile);
            }

            var pongableMelds = MaJiangAlgorithm.GetPongableMelds(Tiles, tile);
            if (pongableMelds.Any())
            {
                FireEvent(PlayerAction.Pong, pongableMelds, tile);
            }

            if (chowable)
            {
                var chowableMelds = MaJiangAlgorithm.GetChowableMelds(Tiles, tile);
                if (chowableMelds.Any())
                {
                    FireEvent(PlayerAction.Chow, chowableMelds, tile);
                }
            }

        }

        private void FireEvent(PlayerAction action, IEnumerable<Meld> melds, Tile actionOnTile)
        {
            if (PlayerActionable != null)
            {
                PlayerActionable(this, new PlayerActionableEventArgs(action, melds, actionOnTile));

                Console.WriteLine("Press Y To Confirm：");
                var input = Console.ReadLine();

                if (input.ToLower() == "y")
                {
                    switch (action)
                    {
                        case PlayerAction.Chow:
                            Chow(actionOnTile);
                            break;
                        case PlayerAction.Pong:
                            Pong(actionOnTile);
                            break;
                        case PlayerAction.Kong:
                            Kong(actionOnTile);
                            break;
                    }
                }
            }


        }
    }
}
