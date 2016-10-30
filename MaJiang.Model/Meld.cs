using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaJiang.Model
{
    public class Meld : TileGroup
    {
        public MeldType Type { get; set; }

        public bool IsExposed { get; set; }

    }
}
