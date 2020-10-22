using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingGame.Common.Entity
{
    public class CheckOutEntity
    {
        public CheckOutEntity() { }

        public int ShoppingGameID { get; set; }
        public IList<ShoppingItemsEntity> Items { get; set; }
    }
}
