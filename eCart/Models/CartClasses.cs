using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCartModels;

namespace eCart
{
    public interface iCartManager
    {
        int AddItem(int stireitemid, decimal qty);
        int RemoveItem(int storeitemid);
    }


    public class CartManager
    {
        List<mvCartItem> cartlist;
        public CartManager()
        {
            this.cartlist = new List<mvCartItem>();
        }

        public int AddItem(int storeid, int storeitemid, decimal Qty)
        {
            int iret = 0;

            try
            {
                mvCartItem item = cartlist.Find(c => c.StoreItemId == storeitemid);
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

        public int RemoveItem(int storeitemid)
        {
            int iret = 0;
            try
            {
                mvCartItem item = cartlist.Find(c => c.StoreItemId == storeitemid);
                if (item != null)
                {
                    cartlist.Remove(item);
                    iret = 1;
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
}