using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCart.Areas.Store.Controllers
{
    public class CashierController : Controller
    {
        // GET: Store/Cashier
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string cartId)
        {
            if (ModelState.IsValid)
            {

                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult CartDetails()
        {
            return View();
        }
    }
}