using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCartModels;

namespace eCart
{
    public interface iCartManager
    {
        CartDetail RequestNewCart();
        int AddItemtoCart(int StoreId, int ItemId, int Qty, decimal price);
    }


    public class CartManager
    {
        List<CartDetail> cartlist;
        public CartManager()
        {
            this.cartlist = new List<CartDetail>();
        }


        public CartDetail RequestNewCart(int StoreId)
        {
            CartDetail cart = new CartDetail();
            cart.StoreDetailId = StoreId;
            this.cartlist.Add(cart);
            return cart;
        }

        private CartDetail _findcart(int storeid)
        {
            CartDetail cart = this.cartlist.Where(c => c.StoreDetailId == storeid).FirstOrDefault();
            if(cart==null)
            {
                cart = this.RequestNewCart(storeid);
            }

            return cart;
        }
        public int AddItemtoCart(int storeId, int ItemId, int Qty, decimal price)
        {
            int iret = 0;
            //CartItem item = new CartItem
            //{
            // Store   
            //    StoreItem = ItemId,
            //    ItemQty = qty,
            //    StoreItemId = id,
            //    CartItemStatusId = 1
            //};


            //CartDetail cart = this._findcart(storeId);
            //cart.CartItems.Add

            return iret;
        }


    }

    public class CartClass
    {
        private CartDetail cartDetails;

        public CartClass()
        {
            this.cartDetails = new CartDetail();
            return;
        }
    }
}