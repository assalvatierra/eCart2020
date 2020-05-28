using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCart.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            var userId = HttpContext.User.Identity.GetUserId();
            if (userId != null)
            {
                return View();
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public ActionResult MasterFiles()
        {
            return View();
        }
    }
}