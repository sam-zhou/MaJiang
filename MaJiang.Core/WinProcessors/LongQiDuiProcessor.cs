using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MaJiang.Model;
using MaJiang.Model.Enums;

namespace MaJiang.Core.WinProcessors
{
    public class LongQiDuiProcessor : QiXiaoDuiProcessor
    {
        public override WinType Type
        {
            get
            {
                return WinType.LongQiDui;
            }
        }

        protected override List<WinningTile> Process()
        {
            var result = base.Process();
            var output = new List<WinningTile>();
            foreach (var winningTile in result)
            {
                
            }

            return output;
        }
    }
}
