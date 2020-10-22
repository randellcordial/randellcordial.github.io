using ShoppingGame.Common;
using ShoppingGame.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingGame.BLL
{
    public class ShoppingItemBLL
    {
        public ShoppingItemsEntity GetByID(int itemID)
        {
            ShoppingItemDummyData data = new ShoppingItemDummyData();

            return data._items.Where(x => x.ItemID == itemID).FirstOrDefault();
        }
    }
    

    /// <summary>
    /// Just a static data.
    /// </summary>
    public class ShoppingItemDummyData
    {
        public IList<ShoppingItemsEntity> _items = new List<ShoppingItemsEntity>();

        public ShoppingItemDummyData()
        {
            _items.Add(SuperIpad());
            _items.Add(MacbookPro());
            _items.Add(AppleTV());
            _items.Add(VGAAdapter());
        }     

        
        private ShoppingItemsEntity SuperIpad()
        {
            ShoppingItemsEntity item = new ShoppingItemsEntity();

            item.ItemID = (int)EnumItems.SuperIpad;
            item.ItemName = "Super iPad";
            item.Price = 549.99m;
            item.Code = "ipd";

            return item;
        }

        private ShoppingItemsEntity MacbookPro()
        {
            ShoppingItemsEntity item = new ShoppingItemsEntity();

            item.ItemID = (int)EnumItems.MacbookPro;
            item.ItemName = "MacBook Pro";
            item.Price = 1399.99m;
            item.Code = "mbp";

            return item;
        }

        private ShoppingItemsEntity AppleTV()
        {
            ShoppingItemsEntity item = new ShoppingItemsEntity();

            item.ItemID = (int)EnumItems.AppleTV;
            item.ItemName = "Apple TV";
            item.Price = 109.50m;
            item.Code = "atv";

            return item;
        }

        private ShoppingItemsEntity VGAAdapter()
        {
            ShoppingItemsEntity item = new ShoppingItemsEntity();

            item.ItemID = (int)EnumItems.VGA;
            item.ItemName = "VGA adapter";
            item.Price = 30.00m;
            item.Code = "vga";

            return item;
        }
    }
}
