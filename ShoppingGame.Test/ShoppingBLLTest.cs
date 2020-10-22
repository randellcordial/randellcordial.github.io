using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingGame.Common.Entity;
using System.Collections.Generic;
using ShoppingGame.BLL;
using System.Linq;
using ShoppingGame.Common;

namespace ShoppingGame.Test
{
    /// <summary>
    /// NOTE: These test cases are based on the static data defined on BLL, any changes or modification on static data can affect the result of these test cases
    /// </summary>
    [TestClass]
    public class ShoppingBLLTest : BaseTest
    {
        [TestMethod]
        public void CheckoutWithoutPromoButHasActivePromo()
        {
            IList<ShoppingItemsEntity> items = new List<ShoppingItemsEntity>();

            ShoppingItemBLL itemBLL = new ShoppingItemBLL();

            items.Add(SuperIpad()); //-- 549.99

            items.Add(MacbookPro());//-- 1399.99
            items.Add(MacbookPro());//-- 1399.99
            
            items.Add(AppleTV());   //-- 109.50

            items.Add(MacbookPro());//-- 1399.99
                                    //---------------
                                    //   4,859.46 Since regular price was applied

            CheckOutBLL bll = new CheckOutBLL();

            decimal computedAmount = bll.GetTotal(items);

            decimal actualAmt = items.Sum(x => x.Price);

            Assert.AreNotEqual(0, computedAmount, "Should be not be equal to zero since there were items added on the cart");
            Assert.AreEqual(actualAmt, computedAmount, "Expected amount should be  4,859.46");
        }

        [TestMethod]
        public void CheckoutWithPromoFor3AppleTV()
        {
            IList<ShoppingItemsEntity> items = new List<ShoppingItemsEntity>();

            ShoppingItemBLL itemBLL = new ShoppingItemBLL();

            //--Total of 3 SuperIpad , means no discount (should be more than 4). Regular price must be applied
            //--Total of 3 AppleTV (109.50 x 3 = 328.5). Price should be 219 since 3 AppleTV were bought
            //--Total of 1 MacbookPro, if there is VGA on the cart, price will be deducted (for VGA). If there is no VGA, then price will be the same. (Please read note below)

            //-- NOTE: (We can develop additional feature that check if the item on cart has free item, so that the cashier can punch the free item and price will be deducted (since there is VGA on cart)

            items.Add(SuperIpad());  //-- 549.99

            items.Add(MacbookPro()); //-- 1399.99
            items.Add(AppleTV());    //-- 109.50

            items.Add(SuperIpad());  //-- 549.99
            items.Add(SuperIpad());  //-- 549.99

            items.Add(AppleTV());    //-- 109.50
            items.Add(AppleTV());    //-- 109.50
                                     //--------------------
                                     //   3,378.46 - 109.50 = 3,268.96
                                          

            CheckOutBLL bll = new CheckOutBLL();

            decimal computedAmount = bll.GetTotal(items);

            decimal actualAmt = 3268.96m; //-- based on computation above

            Assert.AreNotEqual(0, computedAmount, "Should be not be equal to zero since there were items added on the cart");
            Assert.AreEqual(actualAmt, computedAmount, "Expected amount should be 3,268.96");
        }

        [TestMethod]
        public void CheckoutWithPromoFor4AppleTV()
        {
            IList<ShoppingItemsEntity> items = new List<ShoppingItemsEntity>();

            ShoppingItemBLL itemBLL = new ShoppingItemBLL();

            //--Total of 3 SuperIpad , means no discount (should be more than 4). Regular price must be applied
            //--Total of 4 AppleTV (109.50 x 3 = 328.5). Price should be 328.5 since 4 AppleTV were bought (less 1 AppleTV
            //--Total of 1 MacbookPro, if there is VGA on the cart, price will be deducted (for VGA). If there is no VGA, then price will be the same. (Please read note below)

            //-- NOTE: (We can develop additional feature that check if the item on cart has free item, so that the cashier can punch the free item and price will be deducted (since there is VGA on cart)

            items.Add(SuperIpad());  //-- 549.99

            items.Add(MacbookPro()); //-- 1399.99
            items.Add(AppleTV());    //-- 109.50

            items.Add(SuperIpad());  //-- 549.99
            items.Add(AppleTV());    //-- 109.50
            items.Add(SuperIpad());  //-- 549.99

            items.Add(AppleTV());    //-- 109.50
            items.Add(AppleTV());    //-- 109.50
                                     //--------------------
                                     //  3,487.96 - 109.50 = 3,378.46 (Even its 4, only item should be free because 3 item for 2 price)


            CheckOutBLL bll = new CheckOutBLL();

            decimal computedAmount = bll.GetTotal(items);

            decimal actualAmt = 3378.46m; //-- based on computation above

            Assert.AreNotEqual(0, computedAmount, "Should be not be equal to zero since there were items added on the cart");
            Assert.AreEqual(actualAmt, computedAmount, "Expected amount should be 3,378.46");
        }

        [TestMethod]
        public void CheckoutWithPromoFor6AppleTV()
        {
            IList<ShoppingItemsEntity> items = new List<ShoppingItemsEntity>();

            ShoppingItemBLL itemBLL = new ShoppingItemBLL();

            //--Total of 3 SuperIpad , means no discount (should be more than 4). Regular price must be applied
            //--Total of 6 AppleTV (109.50 x 3 = 328.5). Price should be 328.5 since 4 AppleTV were bought (less 1 AppleTV
            //--Total of 1 MacbookPro, if there is VGA on the cart, price will be deducted (for VGA). If there is no VGA, then price will be the same. (Please read note below)

            //-- NOTE: (We can develop additional feature that check if the item on cart has free item, so that the cashier can punch the free item and price will be deducted (since there is VGA on cart)

            items.Add(AppleTV());    //-- 109.50
            items.Add(AppleTV());    //-- 109.50
            items.Add(SuperIpad());  //-- 549.99

            items.Add(MacbookPro()); //-- 1399.99
            items.Add(AppleTV());    //-- 109.50

            items.Add(SuperIpad());  //-- 549.99
            items.Add(AppleTV());    //-- 109.50
            items.Add(SuperIpad());  //-- 549.99

            items.Add(AppleTV());    //-- 109.50
            items.Add(AppleTV());    //-- 109.50
                                     //--------------------
                                     //  3,706.96 - 219 = 3,487.96 (minus 219 since 6 apple tv were bought)


            CheckOutBLL bll = new CheckOutBLL();

            decimal computedAmount = bll.GetTotal(items);

            decimal actualAmt = 3487.96m; //-- based on computation above

            Assert.AreNotEqual(0, computedAmount, "Should be not be equal to zero since there were items added on the cart");
            Assert.AreEqual(actualAmt, computedAmount, "Expected amount should be 3,487.96");
        }

        [TestMethod]
        public void CheckoutWithPromoFor7AppleTV()
        {
            IList<ShoppingItemsEntity> items = new List<ShoppingItemsEntity>();

            ShoppingItemBLL itemBLL = new ShoppingItemBLL();

            //--Total of 3 SuperIpad , means no discount (should be more than 4). Regular price must be applied
            //--Total of 6 AppleTV (109.50 x 3 = 328.5). Price should be 328.5 since 4 AppleTV were bought (less 1 AppleTV
            //--Total of 1 MacbookPro, if there is VGA on the cart, price will be deducted (for VGA). If there is no VGA, then price will be the same. (Please read note below)

            //-- NOTE: (We can develop additional feature that check if the item on cart has free item, so that the cashier can punch the free item and price will be deducted (since there is VGA on cart)

            items.Add(AppleTV());    //-- 109.50
            items.Add(AppleTV());    //-- 109.50
            items.Add(SuperIpad());  //-- 549.99

            items.Add(MacbookPro()); //-- 1399.99
            items.Add(AppleTV());    //-- 109.50

            items.Add(SuperIpad());  //-- 549.99
            items.Add(AppleTV());    //-- 109.50
            items.Add(AppleTV());    //-- 109.50
            items.Add(SuperIpad());  //-- 549.99

            items.Add(AppleTV());    //-- 109.50
            items.Add(AppleTV());    //-- 109.50
                                     //--------------------
                                     //  3,816.46 - 219 = 3,597.46 (minus 219 only since 7 apple tv were bought)


            CheckOutBLL bll = new CheckOutBLL();

            decimal computedAmount = bll.GetTotal(items);

            decimal actualAmt = 3597.46m; //-- based on computation above

            Assert.AreNotEqual(0, computedAmount, "Should be not be equal to zero since there were items added on the cart");
            Assert.AreEqual(actualAmt, computedAmount, "Expected amount should be 3,597.46");
        }


        [TestMethod]
        public void CheckoutWithPromoForSuperIpadAndAppleTV()
        {
            IList<ShoppingItemsEntity> items = new List<ShoppingItemsEntity>();

            ShoppingItemBLL itemBLL = new ShoppingItemBLL();

            //--Total of 6 SuperIpad (which means 6 * 499.99)
            //--Total of 3 AppleTV (109.50 x 3 = 328.5). Price should be 219 since 3 AppleTV were bought
            //--Total of 1 MacbookPro, if there is VGA on the cart, price will be deducted. If there is no VGA, then price will be the same. (Please read note below)

            //-- NOTE: (We can develop additional feature that check if the item on cart has free item, so that the cashier can punch the free item, and price will be deducted (since there is VGA on cart)

            items.Add(SuperIpad());  //-- 499.99 since it is more than 4
            items.Add(SuperIpad());  //-- 499.99
            items.Add(MacbookPro()); //-- 1399.99
            items.Add(AppleTV());    //-- 109.50
            items.Add(SuperIpad());  //-- 499.99
            items.Add(SuperIpad());  //-- 499.99
            items.Add(SuperIpad());  //-- 499.99

            items.Add(AppleTV());    //-- 109.50
            items.Add(AppleTV());    //-- 109.50
            items.Add(SuperIpad());  //-- 499.99
                                     //--------------------
                                     //   4,728.43 - (109.50 AppleTV) = 4618.93


            CheckOutBLL bll = new CheckOutBLL();

            decimal computedAmount = bll.GetTotal(items);

            decimal actualAmt = 4618.93m; //-- based on computation above

            Assert.AreNotEqual(0, computedAmount, "Should be not be equal to zero since there were items added on the cart");
            Assert.AreEqual(actualAmt, computedAmount, "Expected amount should be 4,618.93");
        }

        [TestMethod]
        public void CheckoutWithPromoFor6SuperIpad()
        {
            IList<ShoppingItemsEntity> items = new List<ShoppingItemsEntity>();

            ShoppingItemBLL itemBLL = new ShoppingItemBLL();

            //--Total of 6 SuperIpad (which means 6 * 499.99)
            //--Total of 2 AppleTV (109.50 x 2 = 219).
            //--Total of 1 MacbookPro, if there is VGA on the cart, price will be deducted. If there is no VGA, then price will be the same. (Please read note below)

            //-- NOTE: (We can develop additional feature that check if the item on cart has free item, so that the cashier can punch the free item, and price will be deducted (since there is VGA on cart)

            items.Add(SuperIpad());  //-- 499.99 since it is more than 4
            items.Add(SuperIpad());  //-- 499.99

            items.Add(MacbookPro()); //-- 1399.99

            items.Add(SuperIpad());  //-- 499.99
            items.Add(SuperIpad());  //-- 499.99
            items.Add(SuperIpad());  //-- 499.99

            items.Add(AppleTV());    //-- 109.50
            items.Add(AppleTV());    //-- 109.50

            items.Add(SuperIpad());  //-- 499.99
                                     //--------------------
                                     //   4,618.93 


            CheckOutBLL bll = new CheckOutBLL();

            decimal computedAmount = bll.GetTotal(items);

            decimal actualAmt = 4618.93m; //-- based on computation above

            Assert.AreNotEqual(0, computedAmount, "Should be not be equal to zero since there were items added on the cart");
            Assert.AreEqual(actualAmt, computedAmount, "Expected amount should be 4,618.93");
        }

        [TestMethod]
        public void CheckoutWithPromoFor4SuperIpad()
        {
            IList<ShoppingItemsEntity> items = new List<ShoppingItemsEntity>();

            ShoppingItemBLL itemBLL = new ShoppingItemBLL();

            //--Total of 4 SuperIpad (which means 4 * 549.99)
            //--Total of 2 AppleTV (109.50 x 2 = 219).
            //--Total of 1 MacbookPro, if there is VGA on the cart, price will be deducted. If there is no VGA, then price will be the same. (Please read note below)

            //-- NOTE: (We can develop additional feature that check if the item on cart has free item, so that the cashier can punch the free item, and price will be deducted (since there is VGA on cart)

            items.Add(SuperIpad());  //-- 549.99 since it is more than 4
            items.Add(SuperIpad());  //-- 549.99

            items.Add(MacbookPro()); //-- 1399.99
            
            items.Add(SuperIpad());  //-- 549.99

            items.Add(AppleTV());    //-- 109.50
            items.Add(AppleTV());    //-- 109.50

            items.Add(SuperIpad());  //-- 499.99
                                     //--------------------
                                     //   3,818.95


            CheckOutBLL bll = new CheckOutBLL();

            decimal computedAmount = bll.GetTotal(items);

            decimal actualAmt = 3818.95m; //-- based on computation above

            Assert.AreNotEqual(0, computedAmount, "Should be not be equal to zero since there were items added on the cart");
            Assert.AreEqual(actualAmt, computedAmount, "Expected amount should be 3,818.95");
        }

        [TestMethod]
        public void CheckoutWithPromoFor1MacProBookWithoutVGA()
        {
            IList<ShoppingItemsEntity> items = new List<ShoppingItemsEntity>();

            ShoppingItemBLL itemBLL = new ShoppingItemBLL();

            //--Total of 4 SuperIpad (which means 6 * 499.99)
            //--Total of 2 AppleTV (109.50 x 2 = 219).
            //--Total of 1 MacbookPro, if there is VGA on the cart, price will be deducted. If there is no VGA, then price will be the same. (Please read note below)

            //-- NOTE: (We can develop additional feature that check if the item on cart has free item, so that the cashier can punch the free item (VGA), and price will be deducted (since there is VGA on cart)

            items.Add(SuperIpad());  //-- 549.99
            
            items.Add(MacbookPro()); //-- 1399.99
            items.Add(SuperIpad());  //-- 549.99
            items.Add(SuperIpad());  //-- 549.99

            items.Add(AppleTV());    //-- 109.50
            items.Add(AppleTV());    //-- 109.50
            items.Add(SuperIpad());  //-- 549.99
                                     //--------------------
                                     //   3,818.95


            CheckOutBLL bll = new CheckOutBLL();

            decimal computedAmount = bll.GetTotal(items);

            decimal actualAmt = 3818.95m; //-- based on computation above

            Assert.AreNotEqual(0, computedAmount, "Should be not be equal to zero since there were items added on the cart");
            Assert.AreEqual(actualAmt, computedAmount, "Should be equal to 3,818.95");
        }

        [TestMethod]
        public void CheckoutWithPromoFor1MacProBookWith1VGA()
        {
            IList<ShoppingItemsEntity> items = new List<ShoppingItemsEntity>();

            ShoppingItemBLL itemBLL = new ShoppingItemBLL();

            //--Total of 4 SuperIpad (which means 6 * 499.99)
            //--Total of 2 AppleTV (109.50 x 2 = 219).
            //--Total of 1 MacbookPro, if there is VGA on the cart, price will be deducted. If there is no VGA, then price will be the same. (Please read note below)
            //--Total of 1 VGA 30.00 (since there is a macbook pro, even we add 1 vga on the cart the price is still  zero for VGA)

            //-- NOTE: (We can develop additional feature that check if the item on cart has free item, so that the cashier can punch the free item, and price will be deducted (since there is VGA on cart)

            items.Add(SuperIpad());  //-- 549.99

            items.Add(MacbookPro()); //-- 1399.99
            items.Add(SuperIpad());  //-- 549.99
            items.Add(SuperIpad());  //-- 549.99

            items.Add(VGAAdapter());  //-- 30.00

            items.Add(AppleTV());    //-- 109.50
            items.Add(AppleTV());    //-- 109.50
            items.Add(SuperIpad());  //-- 549.99
                                     //--------------------
                                     //   3848.95 - 30 = 3,818.95 


            CheckOutBLL bll = new CheckOutBLL();

            decimal computedAmount = bll.GetTotal(items);

            decimal actualAmt = 3818.95m; //-- based on computation above

            Assert.AreNotEqual(0, computedAmount, "Should be not be equal to zero since there were items added on the cart");
            Assert.AreEqual(actualAmt, computedAmount, "Should be equal to 3,818.95");
        }

        [TestMethod]
        public void CheckoutWithPromoFor1MacProBookWithMoreThan1VGA()
        {
            IList<ShoppingItemsEntity> items = new List<ShoppingItemsEntity>();

            ShoppingItemBLL itemBLL = new ShoppingItemBLL();

            //--Total of 4 SuperIpad (which means 6 * 499.99)
            //--Total of 2 AppleTV (109.50 x 2 = 219).
            //--Total of 1 MacbookPro, if there is VGA on the cart, price will be deducted. If there is no VGA, then price will be the same. (Please read note below)
            //--Total of 4 VGA 30.00 (120.00) (since there is a macbook pro, Total price should be 30.00 (since 1 macbook only))

            //-- NOTE: (We can develop additional feature that check if the item on cart has free item, so that the cashier can punch the free item, and price will be deducted (since there is VGA on cart)

            items.Add(SuperIpad());  //-- 549.99

            items.Add(MacbookPro()); //-- 1399.99
            items.Add(SuperIpad());  //-- 549.99
            items.Add(SuperIpad());  //-- 549.99

            items.Add(VGAAdapter());  //-- 30.00
            items.Add(VGAAdapter());  //-- 30.00
            items.Add(VGAAdapter());  //-- 30.00
            items.Add(VGAAdapter());  //-- 30.00

            items.Add(AppleTV());    //-- 109.50
            items.Add(AppleTV());    //-- 109.50
            items.Add(SuperIpad());  //-- 549.99
                                     //--------------------
                                     //   3,938.95 - 30 = 3,908.95 ( minus 1 VGA only since the macbook qty is 1)


            CheckOutBLL bll = new CheckOutBLL();

            decimal computedAmount = bll.GetTotal(items);

            decimal actualAmt = 3908.95m; //-- based on computation above

            Assert.AreNotEqual(0, computedAmount, "Should be not be equal to zero since there were items added on the cart");
            Assert.AreEqual(actualAmt, computedAmount, "Should be  equal to 3,908.95");
        }

        [TestMethod]
        public void CheckoutWithPromoFor3MacProBookWith3VGA()
        {
            IList<ShoppingItemsEntity> items = new List<ShoppingItemsEntity>();

            ShoppingItemBLL itemBLL = new ShoppingItemBLL();

            //--Total of 4 SuperIpad (which means 6 * 499.99)
            //--Total of 2 AppleTV (109.50 x 2 = 219).
            //--Total of 3 MacbookPro (4199.97), if there is VGA on the cart, price will be deducted. If there is no VGA, then price will be the same. (Please read note below)
            //--Total of 3 VGA 30.00 (90.00) (since there is a macbook pro, even we add 1 vga on the cart the price is still  zero for VGA)

            //-- NOTE: (We can develop additional feature that check if the item on cart has free item, so that the cashier can punch the free item, and price will be deducted (since there is VGA on cart)

            items.Add(SuperIpad());  //-- 549.99

            items.Add(MacbookPro()); //-- 1399.99

            items.Add(SuperIpad());  //-- 549.99
            items.Add(SuperIpad());  //-- 549.99

            items.Add(VGAAdapter());  //-- 30.00
            items.Add(VGAAdapter());  //-- 30.00
            items.Add(VGAAdapter());  //-- 30.00

            items.Add(AppleTV());    //-- 109.50
            items.Add(AppleTV());    //-- 109.50
            items.Add(SuperIpad());  //-- 549.99

            items.Add(MacbookPro()); //-- 1399.99
            items.Add(MacbookPro()); //-- 1399.99
                                     //--------------------
                                     //   6,708.93 - 90 = 6,618.93 ( minus 3 VGA since the macbook qty is 3 also)


            CheckOutBLL bll = new CheckOutBLL();

            decimal computedAmount = bll.GetTotal(items);

            decimal actualAmt = 6618.93m; //-- based on computation above

            Assert.AreNotEqual(0, computedAmount, "Should be not be equal to zero since there were items added on the cart");
            Assert.AreEqual(actualAmt, computedAmount, "Should be equal to 6,618.93");
        }

        [TestMethod]
        public void CheckoutWithPromoFor3MacProBookWithMoreThan3VGA()
        {
            IList<ShoppingItemsEntity> items = new List<ShoppingItemsEntity>();

            ShoppingItemBLL itemBLL = new ShoppingItemBLL();

            //--Total of 4 SuperIpad (which means 6 * 499.99)
            //--Total of 2 AppleTV (109.50 x 2 = 219).
            //--Total of 3 MacbookPro (4199.97), if there is VGA on the cart, price will be deducted. If there is no VGA, then price will be the same. (Please read note below)
            //--Total of 5 VGA 30.00 (150.00) (since there is a macbook pro, even we add 1 vga on the cart the price is still  zero for VGA)

            //-- NOTE: (We can develop additional feature that check if the item on cart has free item, so that the cashier can punch the free item, and price will be deducted (since there is VGA on cart)

            items.Add(SuperIpad());  //-- 549.99

            items.Add(MacbookPro()); //-- 1399.99

            items.Add(SuperIpad());  //-- 549.99
            items.Add(SuperIpad());  //-- 549.99

            items.Add(VGAAdapter());  //-- 30.00
            items.Add(VGAAdapter());  //-- 30.00
            items.Add(VGAAdapter());  //-- 30.00
        

            items.Add(AppleTV());    //-- 109.50
            items.Add(AppleTV());    //-- 109.50
            items.Add(SuperIpad());  //-- 549.99

            items.Add(MacbookPro()); //-- 1399.99
            items.Add(MacbookPro()); //-- 1399.99

            items.Add(VGAAdapter());  //-- 30.00
            items.Add(VGAAdapter());  //-- 30.00
                                      //--------------------
                                      //   6768.93 - 90 = 6,678.93 ( minus 3 VGA since the macbook qty is 3 also)


            CheckOutBLL bll = new CheckOutBLL();

            decimal computedAmount = bll.GetTotal(items);

            decimal actualAmt = 6678.93m; //-- based on computation above

            Assert.AreNotEqual(0, computedAmount, "Should be not be equal to zero since there were items added on the cart");
            Assert.AreEqual(actualAmt, computedAmount, "Should be equal to 6,678.93");
        }

        [TestMethod]
        public void CheckoutWithExpiredPromoForAppleTV()
        {
            IList<ShoppingItemsEntity> items = new List<ShoppingItemsEntity>();

            ShoppingItemBLL itemBLL = new ShoppingItemBLL();

            //--Total of 3 SuperIpad , means no discount (should be more than 4). Regular price must be applied
            //--Total of 3 AppleTV (109.50 x 3 = 328.5). Price should be 219 if promo is still active.
            //--Total of 1 MacbookPro, if there is VGA on the cart, price will be deducted (for VGA). If there is no VGA, then price will be the same. (Please read note below)

            //-- NOTE: (We can develop additional feature that check if the item on cart has free item, so that the cashier can punch the free item and price will be deducted (since there is VGA on cart)

            items.Add(SuperIpad());  //-- 549.99

            items.Add(MacbookPro()); //-- 1399.99
            items.Add(AppleTV());    //-- 109.50

            items.Add(SuperIpad());  //-- 549.99
            items.Add(SuperIpad());  //-- 549.99

            items.Add(AppleTV());    //-- 109.50
            items.Add(AppleTV());    //-- 109.50
                                     //--------------------
                                     //   3,378.46 (since we will deactive the promo for AppleTV


            CheckOutBLL bll = new CheckOutBLL();

            bll.DeactivatedPromoItemID = new List<int>();
            bll.DeactivatedPromoItemID.Add((int)EnumItems.AppleTV);

            decimal computedAmount = bll.GetTotal(items);

            decimal actualAmt = 3378.46m; //-- based on computation above

            Assert.AreNotEqual(0, computedAmount, "Should be not be equal to zero since there were items added on the cart");
            Assert.AreEqual(actualAmt, computedAmount, "Expected amount should be 3,378.46");
        }

        [TestMethod]
        public void CheckoutWithExpiredPromoForSuperIpad()
        {
            IList<ShoppingItemsEntity> items = new List<ShoppingItemsEntity>();

            ShoppingItemBLL itemBLL = new ShoppingItemBLL();

            //--Total of 6 SuperIpad (which means 6 * 549.99)
            //--Total of 2 AppleTV (109.50 x 2 = 219).
            //--Total of 1 MacbookPro, if there is VGA on the cart, price will be deducted. If there is no VGA, then price will be the same. (Please read note below)

            //-- NOTE: (We can develop additional feature that check if the item on cart has free item, so that the cashier can punch the free item, and price will be deducted (since there is VGA on cart)

            items.Add(SuperIpad());  //-- 549.99 since it the promo was expired
            items.Add(SuperIpad());  //-- 549.99

            items.Add(MacbookPro()); //-- 1399.99

            items.Add(SuperIpad());  //-- 549.99
            items.Add(SuperIpad());  //-- 549.99
            items.Add(SuperIpad());  //-- 549.99

            items.Add(AppleTV());    //-- 109.50
            items.Add(AppleTV());    //-- 109.50

            items.Add(SuperIpad());  //-- 549.99
                                     //--------------------
                                     //   4,918.93


            CheckOutBLL bll = new CheckOutBLL();

            bll.DeactivatedPromoItemID = new List<int>();
            bll.DeactivatedPromoItemID.Add((int)EnumItems.SuperIpad);

            decimal computedAmount = bll.GetTotal(items);

            decimal actualAmt = 4918.93m; //-- based on computation above

            Assert.AreNotEqual(0, computedAmount, "Should be not be equal to zero since there were items added on the cart");
            Assert.AreEqual(actualAmt, computedAmount, "Expected amount should be 4,918.93");
        }

        [TestMethod]
        public void CheckoutWithExpiredPromoForSuperIpadAndAppleTV()
        {
            IList<ShoppingItemsEntity> items = new List<ShoppingItemsEntity>();

            ShoppingItemBLL itemBLL = new ShoppingItemBLL();

            //--Total of 6 SuperIpad (which means 6 * 549.99) since promo for SuperIpad already expired
            //--Total of 3 AppleTV (109.50 x 3 = 328.5) since the AppleTV promo already expired
            //--Total of 1 MacbookPro, if there is VGA on the cart, price will be deducted. If there is no VGA, then price will be the same. (Please read note below)

            //-- NOTE: (We can develop additional feature that check if the item on cart has free item, so that the cashier can punch the free item, and price will be deducted (since there is VGA on cart)

            items.Add(SuperIpad());  //-- 549.99 since it the promo was expired
            items.Add(SuperIpad());  //-- 549.99

            items.Add(MacbookPro()); //-- 1399.99

            items.Add(SuperIpad());  //-- 549.99
            items.Add(SuperIpad());  //-- 549.99
            items.Add(SuperIpad());  //-- 549.99

            items.Add(AppleTV());    //-- 109.50
            items.Add(AppleTV());    //-- 109.50

            items.Add(SuperIpad());  //-- 549.99
            items.Add(AppleTV());    //-- 109.50
                                     //--------------------
                                     //   5,028.43


            CheckOutBLL bll = new CheckOutBLL();

            bll.DeactivatedPromoItemID = new List<int>();
            bll.DeactivatedPromoItemID.Add((int)EnumItems.SuperIpad);
            bll.DeactivatedPromoItemID.Add((int)EnumItems.AppleTV);

            decimal computedAmount = bll.GetTotal(items);

            decimal actualAmt = 5028.43m; //-- based on computation above

            Assert.AreNotEqual(0, computedAmount, "Should be not be equal to zero since there were items added on the cart");
            Assert.AreEqual(actualAmt, computedAmount, "Expected amount should be 5,028.43");
        }

        [TestMethod]
        public void CheckoutWithExpiredPromoForMacBookPro()
        {
            IList<ShoppingItemsEntity> items = new List<ShoppingItemsEntity>();

            ShoppingItemBLL itemBLL = new ShoppingItemBLL();

            //--Total of 6 SuperIpad (which means 6 * 499.99) 
            //--Total of 2 AppleTV (219) 
            //--Total of 1 MacbookPro, if there is VGA on the cart, price will be deducted. If there is no VGA, then price will be the same. (Please read note below)

            //-- NOTE: (We can develop additional feature that check if the item on cart has free item, so that the cashier can punch the free item, and price will be deducted (since there is VGA on cart)

            items.Add(SuperIpad());  //-- 499.99 since it the promo was expired
            items.Add(SuperIpad());  //-- 499.99

            items.Add(MacbookPro()); //-- 1399.99

            items.Add(SuperIpad());  //-- 499.99
            items.Add(SuperIpad());  //-- 499.99
            items.Add(SuperIpad());  //-- 499.99

            items.Add(VGAAdapter());  //-- 499.99

            items.Add(AppleTV());    //-- 109.50
            items.Add(AppleTV());    //-- 109.50

            items.Add(SuperIpad());  //-- 499.99
                                     //--------------------
                                     //   4,648.93


            CheckOutBLL bll = new CheckOutBLL();

            bll.DeactivatedPromoItemID = new List<int>();
            bll.DeactivatedPromoItemID.Add((int)EnumItems.MacbookPro);

            decimal computedAmount = bll.GetTotal(items);

            decimal actualAmt = 4648.93m; //-- based on computation above

            Assert.AreNotEqual(0, computedAmount, "Should be not be equal to zero since there were items added on the cart");
            Assert.AreEqual(actualAmt, computedAmount, "Expected amount should be 4,648.93");
        }
    }
}
