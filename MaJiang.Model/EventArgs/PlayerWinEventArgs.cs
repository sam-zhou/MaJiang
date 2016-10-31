using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaJiang.Model.EventArgs
{
    public class PlayerWinEventArgs : System.EventArgs
    {
        private IEnumerable<WinningTile> _winningTiles;

        public long DiscardByUserId { get; set; }

        public IEnumerable<WinningTile> WinningTiles
        {
            get
            {
                if (_winningTiles == null)
                {
                    _winningTiles = new List<WinningTile>();
                }
                return _winningTiles;
            }
            set { _winningTiles = value; }
        }
    }
}
