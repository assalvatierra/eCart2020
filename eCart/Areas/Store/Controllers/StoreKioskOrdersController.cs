using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eCartModels;
using eCartServices;

namespace eCart.Areas.Store.Controllers
{
    public class StoreKioskOrdersController : Controller
    {
        private StoreFactory store = new StoreFactory();
        // GET: Store/StoreKioskOrders
        public ActionResult Index(int? id, int? orderStatus)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var storeKioskOrders = store.CartMgr.GetStoreKioskOrderList((int)id); 
            ViewBag.StoreId = id;
            return View(storeKioskOrders.ToList());
        }

    }
}
