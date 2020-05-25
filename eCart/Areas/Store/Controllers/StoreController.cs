using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCartModels;
using eCartDbLayer;
using eCartInterfaces;
using eCartServices;
using Microsoft.AspNet.Identity;

namespace eCart.Areas.Store.Controllers
{
    public class StoreController : Controller
    {
        StoreFactory storeFactory = new StoreFactory();

        // GET: Store/Home
        public ActionResult Index(int? id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var storeMgr = storeFactory.StoreMgr;
                // string STOREID = Session["STOREID"] != null ? Session["STOREID"].ToString() : id.ToString();

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