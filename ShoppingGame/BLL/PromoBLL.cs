using ShoppingGame.Common;
using ShoppingGame.Common.Entity;
using ShoppingGame.DAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingGame
{
    public class PromoBLL
    {
        public PromoBLL() { }

        public PromoEntity GetByItemID(int itemID)
        {
            PromoSetupDummyData promo = new PromoSetupDummyData();

            return promo._promos.Where(x => x.ItemID == itemID).FirstOrDefault();
        }
        public IList<PromoEntity> GetAllActive()
        {
            PromoSetupDummyData promo = new PromoSetupDummyData();

            return promo._promos.Where(x => x.IsActive == true).ToList();
        }

    }

    public class PromoSetupDummyData
    {
        public IList<PromoEntity> _promos = new List<PromoEntity>();

        public PromoSetupDummyData()
        {
            _promos.Add(PromoForAppleTV());
            _promos.Add(PromoForSuperIpad());
            _promos.Add(PromoForMacBook());
        }

        private PromoEntity PromoForAppleTV()
        {
            PromoEntity entity = new PromoEntity();

            entity.PromoID = 1;
            entity.ItemID = (int)EnumItems.AppleTV;
            entity.MinimumQuantity = 3;
            entity.IsActive = true;
            entity.PromoStartDate = DateTime.Now;
            entity.PromoEndDate = DateTime.Now.AddMonths(1);

            entity.PromoType = (byte)EnumPromoType.FreeItem;
            entity.FreeItemID = (int)EnumItems.AppleTV;
            entity.FreeItemQuantity = 1;
            entity.FreeItemAmount = 109.50m; //-- Price of AppleTV

            return entity;
        }

        private PromoEntity PromoForSuperIpad()
        {
            PromoEntity entity = new PromoEntity();

            entity.PromoID = 2;
            entity.ItemID = (int)EnumItems.SuperIpad;
            entity.MinimumQuantity = 5;
            entity.IsActive = true;
            entity.PromoStartDate = DateTime.Now;
            entity.PromoEndDate = DateTime.Now.AddMonths(1);

            //-- Bulk promo
            entity.PromoType = (byte)EnumPromoType.Discount;
            entity.DiscountedPrice = 499.99m; //-- Discounted Price for SuperIpad

            return entity;
        }

        private PromoEntity PromoForMacBook()
        {
            PromoEntity entity = new PromoEntity();

            entity.PromoID = 3;
            entity.ItemID = (int)EnumItems.MacbookPro;
            entity.MinimumQuantity = 1;
            entity.IsActive = true;
            entity.PromoStartDate = DateTime.Now;
            entity.PromoEndDate = DateTime.Now.AddMonths(1);
            entity.DiscountedPrice = 0;

            //--Promo is like free item for every purchase
            entity.PromoType = (byte)EnumPromoType.FreeItem;
            entity.FreeItemID = (int)EnumItems.VGA;
            entity.FreeItemQuantity = 1;
            entity.FreeItemAmount = 30.00m; //-- Amount of VGA

            return entity;
        }
    }
}
