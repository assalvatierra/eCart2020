using eCartModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCart.Areas.Shopper.Controllers
{
    public class UserDetailsController : Controller
    {
        private ecartdbContainer db = new ecartdbContainer();

        // GET: Shopper/UserDetails
        public ActionResult Index(int id)
        {
            var userDetails = db.UserDetails.Find(id);
            return View(userDetails);
        }
    }
}