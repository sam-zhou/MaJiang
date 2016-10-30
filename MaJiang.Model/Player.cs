using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaJiang.Model.EventArgs;
using MaJiang.Model.Interfaces;

namespace MaJiang.Model
{
    public class Player : IPlayerAction
    {
        public TilesOnHand TilesOnHand { get; set; }

        public event EventHandler<PlayerActionableEventArgs> PlayerActionable;

        public string Name { get; set; }

        public Player(string name)
        {
            Name = name;
            TilesOnHand = new TilesOnHand();
            TilesOnHand.PlayerActionable += TilesOnHandOnPlayerActionable;
        }

        private void TilesOnHandOnPlayerActionable(object sender, PlayerActionableEventArgs e)
        {
            if (PlayerActionable != null)
            {
                PlayerActionable(sender, e);
            }

        }


        public override string ToString()
        {
            return string.Format("{0}: {1}", Name, TilesOnHand);
        }

        public void Reset()
        {
            TilesOnHand = new TilesOnHand();
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
