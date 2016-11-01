using System;
using MaJiang.Model;
using MaJiang.Model.EventArgs;
using MaJiang.Model.Interfaces;

namespace MaJiang.Core
{
    public class Player : IPlayerAction
    {
        public long Id { get; set; }

        public TilesOnHand TilesOnHand { get; set; }

        public event EventHandler<PlayerActionableEventArgs> PlayerActionable;
        public event EventHandler<PlayerWinEventArgs> PlayerWin;
        public event EventHandler<PlayerInitialWinEventArgs> PlayerInitalWin;

        public string Name { get; set; }

        public bool IsDealer { get; set; }

        public Player(string name)
        {
            Id = new Random().Next(1000000000);
            Name = name;
            Reset();
        }

        private void TilesOnHandOnPlayerInitalWin(object sender, PlayerInitialWinEventArgs e)
        {
            if (PlayerInitalWin != null)
            {
                PlayerInitalWin(this, e);
            }
        }

        private void TilesOnHandOnPlayerWin(object sender, PlayerWinEventArgs e)
        {
            if (PlayerWin != null)
            {
                PlayerWin(this, e);
            }
        }

        private void TilesOnHandOnPlayerActionable(object sender, PlayerActionableEventArgs e)
        {
            if (PlayerActionable != null)
            {
                PlayerActionable(this, e);
            }

        }


        public override string ToString()
        {
            return string.Format("{0}: {1}", Name, TilesOnHand);
        }

        public void Reset()
        {
            TilesOnHand = new TilesOnHand();
            TilesOnHand.PlayerActionable += TilesOnHandOnPlayerActionable;
            TilesOnHand.PlayerWin += TilesOnHandOnPlayerWin;
            TilesOnHand.PlayerInitalWin += TilesOnHandOnPlayerInitalWin;
        }

        public void Draw(Tile tile)
        {
            TilesOnHand.Draw(tile);
        }

        public void Pong(Tile tile)
        {
            TilesOnHand.Pong(tile);
        }

        public void Kong(Tile tile)
        {
            TilesOnHand.Kong(tile);
        }

        public void Chow(Tile tile)
        {
            TilesOnHand.Chow(tile);
        }

        public void Discard(Tile tile)
        {
            TilesOnHand.Discard(tile);
        }

        public void DiscardByOther(Tile tile)
        {
            TilesOnHand.DiscardByOther(tile);
        }
    }
}
