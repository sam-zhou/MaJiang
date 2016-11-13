using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaJiang.Model.Enums;
using MaJiang.Model.EventArgs;

namespace MaJiang.Model
{
    public class AvailablePlayerAction
    {

        public Tile ActionOnTile { get; set; }

        public PlayerActions PlayerAction { get; set; }
        public PlayerPositions CurrentPosition { get; set; }

        public PlayerPositions CurrentPlayerPosition { get; set; }

        public PlayerPositions PlayerPosition { get; set; }

        private IEnumerable<Meld> _melds;

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

        public AvailablePlayerAction(PlayerActions playerAction, PlayerPositions currentPosition, PlayerPositions playerPosition, Tile actionOnTile, IEnumerable<Meld> melds)
        {
            PlayerAction = playerAction;
            CurrentPosition = currentPosition;
            PlayerPosition = playerPosition;
            ActionOnTile = actionOnTile;
            Melds = melds;

            Priority = GetPriority();
        }

        public int Priority { get; private set; }

        private int GetPriority()
        {

            var between = PlayerPosition.GetBetween(CurrentPlayerPosition);

            if (between == 0)
            {
                return 0;
            }

            if (PlayerAction == PlayerActions.Chow && between != 1)
            {
                return 0;
            }

            return (int)PlayerAction * 4 + (4 - between);
        }
    }
}
