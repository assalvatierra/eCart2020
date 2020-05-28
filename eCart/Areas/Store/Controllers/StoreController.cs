using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCartModels;
using eCartServices;
using Microsoft.AspNet.Identity;

namespace eCart.Areas.Store.Controllers
{
    public class StoreController : Controller
    {
        StoreFactory storeFactory = new StoreFactory();

        // GET: Store/Home
        public ActionResult Index(int? cartStatus)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var storeMgr = storeFactory.StoreMgr;
                var userid = HttpContext.User.Identity.GetUserId();

                var store = storeMgr.GetStoreDetailByLoginId(userid);

                if (store != null)
                {
                    ViewBag.StoreId = store.Id;
                }
                else
                {
                    return RedirectToAction("NoUserStore");
                }


                var cartList = new List<CartDetail>();
                if (cartStatus != null)
                {
                
                    //cartlist
                    if (cartStatus == 1)
                    {
                        cartList = storeMgr.getStoreActiveCarts(store.Id);
                    }
                    else if (cartStatus == 5)
                    {
                        cartList = storeMgr.getStoreCarts(store.Id, (int)cartStatus);
                    }
                    else if (cartStatus == 6)
                    {
                        cartList = storeMgr.getStoreCarts(store.Id, (int)cartStatus);
                    }
                }
                else
                {
                    cartList = storeMgr.getStoreActiveCarts(store.Id);
                }

                ViewBag.CartList = cartList;
                return View(store);
            }
            else
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

        }

        public ActionResult NoUserStore()
        {
            return View();
        }
    }
}