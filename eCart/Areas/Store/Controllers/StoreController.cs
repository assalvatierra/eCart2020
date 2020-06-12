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
        private int MERCHANDISER = 1;
        private int KIOSK = 2;
        private int CASHIER = 3;

        // GET: Store/Home
        public ActionResult Index(int? cartStatus)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var storeMgr = store.StoreMgr;
                var userid = HttpContext.User.Identity.GetUserId();
                var storeDetail = storeMgr.GetStoreDetailByLoginId(userid);
                var storeId = 0;
                var userType = "NA";
                var isUserCashier = false;
                var isUserMerchandiser = false;
                var isUserKiosk = false;
                var isUserAdmin = false;

                if (storeDetail != null)
                {
                    storeId = storeDetail.Id;
                    isUserAdmin = true;
                }
                else
                {
                    //check if user is assigned to company as CASHIER
                    var storeUser = storeMgr.GetStoreUser(userid);
                    if (storeUser != null)
                    {
                        storeId = storeUser.StoreDetail.Id;
                        userType = storeUser.StoreUserType.Name;
                        storeDetail = storeUser.StoreDetail;

                        //get user types
                        isUserCashier = storeMgr.CheckStoreUserType(userid, CASHIER);
                        isUserMerchandiser = storeMgr.CheckStoreUserType(userid, MERCHANDISER);
                        isUserKiosk = storeMgr.CheckStoreUserType(userid, KIOSK);

                        if (isUserCashier && !isUserMerchandiser && !isUserKiosk)
                            return RedirectToAction("Index","Cashier", new { id = storeId });
                    }
                    else
                    {
                        return RedirectToAction("NoUserStore");
                    }

                }

                //cartList
                var cartList = new List<CartDetail>();
                cartList = GetFilteredCart(storeDetail.Id, cartStatus);
                if (isUserKiosk && !isUserMerchandiser)
                    cartList = cartList.Where(s => s.DeliveryType == "Kiosk").ToList();

                ViewBag.StoreId = storeId;
                ViewBag.IsUserAdmin = isUserAdmin;
                ViewBag.IsUserCashier = isUserCashier;
                ViewBag.IsUserMerchandiser = isUserMerchandiser;
                ViewBag.IsUserKiosk = isUserKiosk;
                ViewBag.CartList = cartList;
                ViewBag.PaymentReceiverList = store.RefDbLayer.GetPaymentReceivers().ToList();
                ViewBag.PaymentStatusList = store.RefDbLayer.GetPaymentStatus().ToList();
                ViewBag.PaymentPartyList = store.RefDbLayer.GetPaymentParties().ToList();
                ViewBag.User = HttpContext.User.Identity.Name;
                return View(storeDetail);
            }
            else
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

        }

        private List<CartDetail> GetFilteredCart(int storeId, int? cartStatus)
        {
            var storeMgr = store.StoreMgr;
            var cartList = new List<CartDetail>();
            if (cartStatus != null)
            {
                switch (cartStatus)
                {
                    case 5:
                        cartList = storeMgr.getStoreCarts(storeId, 5);
                        break;
                    case 6:
                        cartList = storeMgr.getStoreCarts(storeId, 6);
                        break;
                    default:
                        cartList.AddRange(storeMgr.getStoreActiveKioskOrders(storeId));
                        cartList.AddRange(storeMgr.getStoreActiveCarts(storeId));
                        break;
                }
            }
            else
            {
                cartList.AddRange(storeMgr.getStoreActiveKioskOrders(storeId));
                cartList.AddRange(storeMgr.getStoreActiveCarts(storeId));
            }

            return cartList;
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

        public string getKioskOrderName(int id)
        {
            return store.StoreMgr.GetKioskOrder(id).Customer;
        }

        public ActionResult NoUserStore()
        {
            return View();
        }
    }
}