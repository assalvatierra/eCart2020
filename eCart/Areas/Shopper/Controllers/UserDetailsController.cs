using eCartModels;
using eCartServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCart.Areas.Shopper.Controllers
{
    public class UserDetailsController : Controller
    {

        StoreFactory store = new StoreFactory();

        // GET: Shopper/UserDetails
        public ActionResult Index(int id)
        {
            var userDetails = store.UserMgr.GetUserDetails(id);

            if (userDetails == null)
            {
                return RedirectToAction("Error", "Accounts");
            }

            return View(userDetails);
        }
    }
}