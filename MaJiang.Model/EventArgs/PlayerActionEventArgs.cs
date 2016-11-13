using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaJiang.Model.Enums;

namespace MaJiang.Model.EventArgs
{
    public class PlayerActionEventArgs: System.EventArgs
    {
        private IEnumerable<Meld> _melds; 

        public PlayerActions PlayerAction { get; private set; }

        public Tile ActionOnTile { get; private set; }

        public IEnumerable<Meld> Melds
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


        public PlayerActionEventArgs(PlayerActions playerAction, IEnumerable<Meld> melds, Tile actionOnTile = null)
        {
            PlayerAction = playerAction;
            Melds = melds;
            ActionOnTile = actionOnTile;
        }

        public PlayerActionEventArgs(PlayerActions playerAction, Tile actionOnTile)
        {
            PlayerAction = playerAction;
            ActionOnTile = actionOnTile;
        }
    }
}
