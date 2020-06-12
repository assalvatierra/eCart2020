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

        // GET: Store/Cashier
        public ActionResult Index()
        {
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

                if (storeDetail != null)
                {
                    ViewBag.StoreId = storeDetail.Id;
                }
                else
                {
                    return RedirectToAction("NoUserStore");
                }

                var cart = store.CartMgr.GetCartDetail(cartId);

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