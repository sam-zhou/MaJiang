using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaJiang.Model.Enums;

namespace MaJiang.Model
{
    public class AvailablePlayerAction
    {

        public Tile ActionOnTile { get; set; }

        public PlayerAction PlayerAction { get; set; }


        public PlayerDirection Direction { get; set; }

        public bool Accepted { get; set; }

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


    }
}
