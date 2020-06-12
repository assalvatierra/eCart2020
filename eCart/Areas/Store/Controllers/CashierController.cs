using eCartServices;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCart.Areas.Store.Controllers
{
    public class CashierController : Controller
    {
        private StoreFactory store = new StoreFactory();
        private int CASHIER = 3;

        // GET: Store/Cashier
        public ActionResult Index()
        {
            var userid = HttpContext.User.Identity.GetUserId();
            var storeDetail = store.StoreMgr.GetStoreDetailByLoginId(userid);
            var storeId = 0;
            if (storeDetail != null)
            {
                storeId = storeDetail.Id;
            }
            else
            {
                var storeUser = store.StoreMgr.GetStoreUser(userid);
                storeDetail = storeUser.StoreDetail;
                if (storeDetail != null)
                {
                    storeId = storeDetail.Id;
                    var isUserCashier = store.StoreMgr.CheckStoreUserType(userid, CASHIER);
                    if (!isUserCashier)
                        return RedirectToAction("NoUserStore");
                }
                else
                {
                    return RedirectToAction("NoUserStore");
                }

            }

            ViewBag.StoreId = storeId;
            ViewBag.StoreImage = storeDetail.StoreImages.FirstOrDefault().ImageUrl;
            ViewBag.User = HttpContext.User.Identity.Name;
            return View();
        }
        [HttpPost]
        public ActionResult Index(int cartId)
        { 
            if (ModelState.IsValid )
            {

                var userid = HttpContext.User.Identity.GetUserId();

                var storeDetail = store.StoreMgr.GetStoreDetailByLoginId(userid);
                var storeId = 0;
                if (storeDetail != null)
                {
                    storeId = storeDetail.Id;
                }
                else
                {
                    var storeUser = store.StoreMgr.GetStoreUser(userid);
                    storeDetail = storeUser.StoreDetail;
                    if (storeDetail != null)
                    {
                        storeId = storeDetail.Id;
                        var isUserCashier = store.StoreMgr.CheckStoreUserType(userid, CASHIER);
                        if (!isUserCashier)
                            return RedirectToAction("NoUserStore");
                    }
                    else
                    {
                        return RedirectToAction("NoUserStore");
                    }

                }

                var cart = store.CartMgr.GetCartDetail(cartId);
                ViewBag.StoreId = storeId;
                ViewBag.StoreImage = storeDetail.StoreImages.FirstOrDefault().ImageUrl;
                ViewBag.User = HttpContext.User.Identity.Name;

                if (cart != null && cart.StoreDetailId == storeDetail.Id)
                {
                    return RedirectToAction("Index", new { id = cartId });
                    //return View();
                }
                else
                {
                    ModelState.AddModelError("", "No existing cart found");
                    return RedirectToAction("Index", new { id = cartId });
                }
            }

            return View();
        }

        public ActionResult CartDetails(int id)
        {

            var userid = HttpContext.User.Identity.GetUserId();
            var storeDetail = store.StoreMgr.GetStoreDetailByLoginId(userid);
            if(storeDetail == null)
            {
                var storeUser = store.StoreMgr.GetStoreUser(userid);
                storeDetail = storeUser.StoreDetail;
            }

            var cart = store.CartMgr.GetCartDetail(id);
            if (cart != null && cart.StoreDetailId == storeDetail.Id)
            {

                ViewBag.StoreId = cart.StoreDetailId;
                ViewBag.Store = cart.StoreDetail.Name;
                ViewBag.PaymentReceiverList = store.RefDbLayer.GetPaymentReceivers().ToList();
                ViewBag.PaymentStatusList = store.RefDbLayer.GetPaymentStatus().ToList();
                ViewBag.PaymentPartyList = store.RefDbLayer.GetPaymentParties().ToList();
                ViewBag.PaymentDetails = store.CartMgr.GetCartPaymentDetails((int)id);
                ViewBag.User = HttpContext.User.Identity.Name;
                return View(cart);
            }

            return View();
        }

    }
}