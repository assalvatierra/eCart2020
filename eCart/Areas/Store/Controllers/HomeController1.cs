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
    public class HomeController1 : Controller
    {
        StoreFactory storeFactory = new StoreFactory();

        // GET: Store/Home
        public ActionResult Index(int? id)
        {
            if (id != null )
            {
                var storeMgr = storeFactory.StoreMgr;
                //                string STOREID = Session["STOREID"] != null ? Session["STOREID"].ToString() : id.ToString();


                var userid = HttpContext.User.Identity.GetUserId();
                var store = storeMgr.GetStoreDetailByLoginId(userid);
                ViewBag.StoreId = store.Id;
                return View(store);
            }
            else
            {
                return RedirectToAction("Login","Accounts", new { area = "Store" });
            }
            
        }
    }
}