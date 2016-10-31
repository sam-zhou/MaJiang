﻿using System;
using System.Collections.Generic;
using System.Linq;
using MaJiang.Core.WinProcessors;
using MaJiang.Model;
using MaJiang.Model.Enums;
using MaJiang.Model.EventArgs;
using MaJiang.Model.Interfaces;

namespace MaJiang.Core
{
    public class TilesOnHand : TileGroup, IPlayerAction
    {

        private List<Meld> _melds;

        public event EventHandler<PlayerActionableEventArgs> PlayerActionable;
        public event EventHandler<PlayerWinEventArgs> PlayerWin;
        public event EventHandler<PlayerInitalWinEventArgs> PlayerInitalWin;

        public bool IsWinable { get; set; }

        public bool IsPongable { get; set; }

        public bool IsChowable { get; set; }

        public bool IsKongable { get; set; }

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

        public TilesOnHand()
        {
            WinProcessorFactory = new WinProcessorFactory();
        }

        public WinProcessorFactory WinProcessorFactory { get; set; }


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
            var winProcessorFactory = new WinProcessorFactory();
            var result = new List<WinningTile>();
            foreach (var winProcessor in winProcessorFactory.WinProcessors)
            {
                result.AddRange(winProcessor.Validate(Tiles, new List<Tile> { tile }));
            }

            if (result.Any() && PlayerWin != null)
            {
                PlayerWin(this, new PlayerWinEventArgs{WinningTiles = result});
            }

            var kongableMelds = MaJiangAlgorithm.GetKongableMelds(Tiles, tile);

            if (kongableMelds.Any())
            {
                FireEvent(PlayerAction.Kong, kongableMelds);
            }


            var pongableMelds = MaJiangAlgorithm.GetPongableMelds(Tiles, tile);
            if (pongableMelds.Any())
            {
                FireEvent(PlayerAction.Pong, pongableMelds);
            }

            var chowableMelds = MaJiangAlgorithm.GetChowableMelds(Tiles, tile);
            if (chowableMelds.Any())
            {
                FireEvent(PlayerAction.Chow, chowableMelds);
            }
        }

        private void FireEvent(PlayerAction action, IEnumerable<Meld> melds)
        {
            if (PlayerActionable != null)
            {
                PlayerActionable(this, new PlayerActionableEventArgs(action, melds));
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