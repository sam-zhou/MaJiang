using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MaJiang.Model;
using MaJiang.Model.Enums;

namespace MaJiang.Core.WinProcessors
{
    public class PengPengHuProcessor : NormalWinProcessor
    {
        public override WinType Type
        {
            get
            {
                return WinType.PengPengHu;
            }
        }

        protected override List<WinningTile> Process()
        {
            var output = base.Process().Where(q => q.MeldCollection.IsPengPengHu).ToList();
            return output;
        }
    }
}
