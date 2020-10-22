using ShoppingGame.BLL;
using ShoppingGame.Common;
using ShoppingGame.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingGame.Test
{
    public class BaseTest
    {
        public ShoppingItemsEntity SuperIpad()
        {
            ShoppingItemBLL bll = new ShoppingItemBLL();
            return bll.GetByID((int)EnumItems.SuperIpad);
        }

        public ShoppingItemsEntity MacbookPro()
        {
            ShoppingItemBLL bll = new ShoppingItemBLL();
            return bll.GetByID((int)EnumItems.MacbookPro);
        }

        public ShoppingItemsEntity AppleTV()
        {
            ShoppingItemBLL bll = new ShoppingItemBLL();
            return bll.GetByID((int)EnumItems.AppleTV);
        }

        public ShoppingItemsEntity VGAAdapter()
        {
            ShoppingItemBLL bll = new ShoppingItemBLL();
            return bll.GetByID((int)EnumItems.VGA);
        }
    }
}
