using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingGame.Common.Entity
{
    public class ShoppingItemsEntity
    {
        public ShoppingItemsEntity() { }

        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
    }
}
