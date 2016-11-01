using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaJiang.Model.Enums;

namespace MaJiang.Model.EventArgs
{
    public class PlayerInitialWinEventArgs : System.EventArgs
    {
        private List<Meld> _melds;

        public List<Meld> Melds
        {
            get
            {
                if (_melds == null)
                {
                    _melds = new List<Meld>();
                }
                return _melds;
            }
        }

        private List<Suit> _lackSuits;

        public List<Suit> LackSuits
        {
            get
            {
                if (_lackSuits == null)
                {
                    _lackSuits = new List<Suit>();
                }
                return _lackSuits;
            }
        }

        public InitialWinType Type { get; private set; }


        public int Count
        {
            get { return Melds.Count; }
        }

        public PlayerInitialWinEventArgs(InitialWinType type, List<Meld> melds)
        {
            _melds = melds;
            Type = type;
        }

        public PlayerInitialWinEventArgs(InitialWinType type, List<Suit> lackSuits)
        {
            Type = type;
            _lackSuits = lackSuits;
        }

        public PlayerInitialWinEventArgs(InitialWinType type)
        {
            Type = type;
        }
    }
}
