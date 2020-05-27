using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCartModels;

namespace eCart
{
    public interface iCartManager
    {
        int AddItem(int storeid, int storeitemid, decimal qty);
        int RemoveItem(int storeid, int storeitemid);

        List<mvCartDetail> GetCartList();
    }


    public class CartManager : iCartManager
    {
        List<mvCartDetail> cartlist;

        public CartManager()
        {
            this.cartlist = new List<mvCartDetail>();

        }

        public int AddItem(int storeid, int storeitemid, decimal Qty)
        {
            int iret = 0;

            try
            {
                //find cart of the store
                mvCartDetail cart = cartlist.Find(c => c.StoreId == storeid);
                if (cart == null)
                {
                    //create cart for the store
                    cart = new mvCartDetail();
                    cart.itemList = new List<mvCartItem>();

                }

                mvCartItem item = cart.itemList.Find(c => c.StoreItemId == storeitemid);
                if (item != null)
                {
                    item.ItemQuantity = item.ItemQuantity + Qty;
                    iret = 1;
                }
                else
                {
                    item = new mvCartItem
                    {
                        StoreId = storeid,
                        StoreItemId = storeitemid,
                        ItemQuantity = Qty
                    };

                    iret = 1;

                }
            }
            catch(Exception e)
            {
                iret = -1;
            }

            return iret;
        }

        public List<mvCartDetail> GetCartList()
        {
            return cartlist;
        }

        public int RemoveItem(int storeId, int storeitemid)
        {
            int iret = 0;
            try
            {
                mvCartDetail cart = cartlist.Find(c => c.StoreId == storeId);
                if (cart != null)
                {
                    mvCartItem item = cart.itemList.Find(c => c.StoreItemId == storeitemid);
                    if (item != null)
                    {
                        cart.itemList.Remove(item);
                        iret = 1;
                    }
                }

            }
            catch (Exception e)
            {
                iret = -1;
            }

            return iret;

        }
    }



    public class mvCartItem
    {
        public int StoreId;
        public int StoreItemId;
        public Decimal ItemQuantity;
    }

    public class mvCartDetail
    {
        public int StoreId;
        public int StatusId;
        public List<mvCartItem> itemList; 
    }
}