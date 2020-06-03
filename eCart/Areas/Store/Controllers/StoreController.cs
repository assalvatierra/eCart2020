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
        StoreFactory store = new StoreFactory();

        // GET: Store/Home
        public ActionResult Index(int? cartStatus)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var storeMgr = store.StoreMgr;
                var userid = HttpContext.User.Identity.GetUserId();

                var storeDetail = storeMgr.GetStoreDetailByLoginId(userid);

                if (storeDetail != null)
                {
                    ViewBag.StoreId = storeDetail.Id;
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
                        cartList = storeMgr.getStoreActiveCarts(storeDetail.Id);
                    }
                    else if (cartStatus == 5)
                    {
                        cartList = storeMgr.getStoreCarts(storeDetail.Id, (int)cartStatus);
                    }
                    else if (cartStatus == 6)
                    {
                        cartList = storeMgr.getStoreCarts(storeDetail.Id, (int)cartStatus);
                    }
                }
                else
                {
                    cartList = storeMgr.getStoreActiveCarts(storeDetail.Id);
                }

                ViewBag.CartList = cartList;
                ViewBag.PaymentReceiverList = store.RefDbLayer.GetPaymentReceivers().ToList();
                ViewBag.PaymentStatusList = store.RefDbLayer.GetPaymentStatus().ToList();
                ViewBag.PaymentPartyList = store.RefDbLayer.GetPaymentParties().ToList();
                return View(storeDetail);
            }
            else
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

        }


        // GET: Store/Home
        public PartialViewResult StoreHeader()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var storeMgr = store.StoreMgr;
                var userid = HttpContext.User.Identity.GetUserId();

                var storeDetail = storeMgr.GetStoreDetailByLoginId(userid);

                if (storeDetail != null)
                {
                    ViewBag.StoreId = storeDetail.Id;
                }
               

                return PartialView(storeDetail);
            }

            return PartialView();

        }

        public ActionResult NoUserStore()
        {
            return View();
        }
    }
}