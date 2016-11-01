using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaJiang.Model.EventArgs
{
    public class PlayerDiscardEventArgs:System.EventArgs
    {
        public Tile Tile { get; set; }
    }
}
