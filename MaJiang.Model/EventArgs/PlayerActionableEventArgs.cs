using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaJiang.Model.Enums;

namespace MaJiang.Model.EventArgs
{
    public class PlayerActionableEventArgs: System.EventArgs
    {
        private IEnumerable<Meld> _melds; 

        public PlayerAction PlayerAction { get; set; }

        public Tile ActionOnTile { get; set; }

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
            set { _melds = value; }
        }

        public PlayerActionableEventArgs(PlayerAction playerAction, IEnumerable<Meld> melds, Tile actionOnTile = null)
        {
            PlayerAction = playerAction;
            Melds = melds;
            ActionOnTile = actionOnTile;
        }
    }
}
