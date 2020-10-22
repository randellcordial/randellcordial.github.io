using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingGame.Common
{
    public enum EnumItems : int
    {
        None = 0,
        SuperIpad,
        MacbookPro,
        AppleTV,
        VGA
    }

    public enum EnumPromoType : byte
    {
        None,
        FreeItem,
        Discount
    }
}
