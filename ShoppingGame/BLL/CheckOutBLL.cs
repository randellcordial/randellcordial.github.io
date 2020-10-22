using ShoppingGame.Common;
using ShoppingGame.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingGame.BLL
{
    public class CheckOutBLL
    {
        /// <summary>
        /// I just use this property so that my test cases will include scenario like, if the promo is deactivated or expired.
        /// </summary>
        public IList<int> DeactivatedPromoItemID
        {
            get;
            set;
        }
        
        /// <summary>
        /// Get the total amount of the items in cart
        /// </summary>
        /// <param name="items">Items to be computed</param>
        /// <returns></returns>
        public decimal GetTotal(IList<ShoppingItemsEntity> items)
        {
            if (items == null)
                return 0;

            decimal totalAmount = 0;

            try
            {
                totalAmount = items.Sum(x => x.Price);

                IList<PromoEntity> promos = new PromoBLL().GetAllActive();

                //-- This is not necessary in actual system, since on the above code, we can get only all active from DB. I just added this code to manually deactive the promo
                if (DeactivatedPromoItemID != null && DeactivatedPromoItemID.Count > 0)
                    promos = DeactivatePromo(promos);

                if (promos.Count > 0)
                   totalAmount = GetDiscountedPrice(items, promos, totalAmount);
            }
            catch
            {
                //Log Exception
                throw;
            }

            return totalAmount;
        }

        private IList<PromoEntity> DeactivatePromo(IList<PromoEntity> promos)
        {
            IList<PromoEntity> newList = promos
                .Where(x => !DeactivatedPromoItemID.Any(y => y == x.ItemID)).ToList();

            return newList;
        }

        private decimal GetDiscountedPrice(IList<ShoppingItemsEntity> items, IList<PromoEntity> promos, decimal totalAmount)
        {
            decimal totalDiscount = 0;
            IList<int> itemProcessed = new List<int>();
            IList<PromoEntity> itemPromo = new List<PromoEntity>();

            foreach (ShoppingItemsEntity item in items)
            {
                if (itemProcessed.Contains(item.ItemID))
                    continue;

                //-- Get All promos per item
                itemPromo = promos.Where(x => x.ItemID == item.ItemID).ToList();

                totalDiscount += GetTotalDiscountPerItemPromo(items, itemPromo, item.ItemID);

                itemProcessed.Add(item.ItemID);
            }

            return totalAmount - totalDiscount;
        }

        private decimal GetTotalDiscountPerItemPromo(IList<ShoppingItemsEntity> items, IList<PromoEntity> itemPromos, int itemID)
        {
            decimal totalAmount = 0;

            IList<ShoppingItemsEntity> selectedItems = items.Where(x => x.ItemID == itemID).ToList();

            foreach (PromoEntity promo in itemPromos)
            {
                if (promo.PromoType == (byte)EnumPromoType.FreeItem && selectedItems.Count >= promo.MinimumQuantity)
                {
                    totalAmount += GetTotalDiscountForPromoWithFreeItem(selectedItems, items, promo);
                }
                else if (promo.PromoType == (byte)EnumPromoType.Discount && selectedItems.Count >= promo.MinimumQuantity)
                {
                    totalAmount += selectedItems.Sum(x => x.Price) - (selectedItems.Count * promo.DiscountedPrice);
                }
                else
                {
                    totalAmount = 0;
                }
            }

            return totalAmount;
        }

        private decimal GetTotalDiscountForPromoWithFreeItem(IList<ShoppingItemsEntity> selectedItems, IList<ShoppingItemsEntity> items, PromoEntity promo)
        {
            decimal totalDiscount = 0;

            if (items.Select(x => x.ItemID).Contains(promo.FreeItemID))
            {
                totalDiscount = Math.Truncate(Convert.ToDecimal(selectedItems.Count / promo.MinimumQuantity)) * promo.FreeItemAmount * promo.FreeItemQuantity;
            }
            else
                totalDiscount = 0;

            return totalDiscount;
        }
    }
}
