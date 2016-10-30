using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaJiang.Model.EventArgs
{
    public class PlayerActionableEventArgs
    {
        public PlayerAction PlayerAction { get; set; }

        public PlayerActionableEventArgs(PlayerAction playerAction)
        {
            PlayerAction = playerAction;
        }
    }
}
