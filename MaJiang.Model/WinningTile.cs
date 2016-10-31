using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaJiang.Model.Enums;

namespace MaJiang.Model
{
    public class WinningTile
    {
        private readonly MeldCollection _meldCollection;
        private readonly WinType _type;

        public int Power { get; set; }

        public MeldCollection MeldCollection
        {
            get { return _meldCollection; }
        }

        public WinType Type
        {
            get { return _type; }
        }

        public WinningTile(MeldCollection meldCollection, WinType type, int power = 1)
        {
            _meldCollection = meldCollection;
            _type = type;
            Power = power;
        }

        public override string ToString()
        {
            return string.Format("{0} win by {1}", Type, MeldCollection.Draw);
        }
    }
}
