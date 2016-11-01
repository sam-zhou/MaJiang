using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaJiang.Model.EnumAttributes
{
    internal class IsJiangAttribute: Attribute
    {
        public bool IsJiang { get; private set; }

        internal IsJiangAttribute(bool isJiang)
        {
            IsJiang = IsJiang;
        }

    }
}
