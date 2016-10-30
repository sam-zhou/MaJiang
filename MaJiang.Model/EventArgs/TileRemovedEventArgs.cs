using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaJiang.Model.EventArgs
{
    public class TileRemovedEventArgs: System.EventArgs
    {
        public int Index { get; set; }

        public TileRemovedEventArgs(int index)
        {
            Index = index;
        }
    }
}
