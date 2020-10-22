using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingGame.Common.Entity
{
    public class PromoEntity
    {
        public PromoEntity() { }

        public int PromoID { get; set; }
        public int ItemID { get; set; }
        public short MinimumQuantity { get; set; }
        public  decimal DiscountedPrice { get; set; }
        public int FreeItemID { get; set; }
        public short FreeItemQuantity { get; set; }
        public decimal FreeItemAmount { get; set; }
        public bool IsActive { get; set; }
        public DateTime PromoStartDate { get; set; }
        public DateTime PromoEndDate { get; set; }
        public byte PromoType { get; set; }
    }
}
