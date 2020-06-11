using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using eCartModels;
using eCartServices;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace eCart.Areas.Shopper.Controllers
{
    public class AccountsController : Controller
    {
        //AccMgr accMgr = new AccMgr();
        StoreFactory store = new StoreFactory();

        //Authentication
        private readonly ecartdbContainer _dbContext = new ecartdbContainer();

        // User Roles
        //private int ADMIN = 1;
        //private int STORE_ADMIN = 2;
        //private int STORE_MERCHANDISER = 3;
        private int SHOPPER = 4;
        //private int RIDER = 5;

        // GET: Shopper/Accounts
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Login function 
        /// </summary>
        /// <param name="usertype">Integer: 15-admin, 1-Store, 0 or 2-shopper, 3-rider </param>
        /// <returns></returns>
        //public ActionResult Login(int? usertype)
        //{
        //    ViewBag.UserType = usertype; //use for altering the login page info/images and other info to fit the type of user
        //    return View();
        //}



        // GET: Admin/UserDetails/Create
        public ActionResult Register()
        {
            UserDetail user = new UserDetail();
            user.UserId = HttpContext.User.Identity.GetUserId();

            ViewBag.MasterAreaId = new SelectList(store.RefDbLayer.GetMasterAreas(), "Id", "Name");
            ViewBag.MasterCityId = new SelectList(store.RefDbLayer.GetMasterCities(), "Id", "Name");
            ViewBag.UserStatusId = new SelectList(store.RefDbLayer.GetUserStatusList(), "Id", "Name");
            return View(user);
        }

        // POST: Admin/UserDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Id,UserId,Name,Address,Email,Mobile,Remarks,UserStatusId,MasterCityId,MasterAreaId")] UserDetail userDetail)
        {
            if (ModelState.IsValid && checkRegistrationFields(userDetail))
            {
                if (store.AdminMgr.AddUserDetails(userDetail))
                {
                    return RedirectToAction("Index", "Home",new { area = "" });
                }
            }

            ViewBag.MasterAreaId = new SelectList(store.RefDbLayer.GetMasterAreas(), "Id", "Name", userDetail.MasterAreaId);
            ViewBag.MasterCityId = new SelectList(store.RefDbLayer.GetMasterCities(), "Id", "Name", userDetail.MasterCityId);
            ViewBag.UserStatusId = new SelectList(store.RefDbLayer.GetUserStatusList(), "Id", "Name", userDetail.UserStatusId);
            return View(userDetail);
        }

        public bool checkRegistrationFields(UserDetail userDetail)
        {
            bool isValid = true;
            if (userDetail.Name.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Name", "Name field is empty.");
                isValid = false;
            }
            if (userDetail.Address.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Address", "Address field is empty.");
                isValid = false;
            }

            if (userDetail.Email.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Email", "Email field is empty.");
                isValid = false;
            }
            else 
            {
                if (store.AdminMgr.IsUserEmailExist(userDetail.Email))
                {
                    ModelState.AddModelError("Email", "Email is already registered.");
                    isValid = false;
                }
            }

            if (userDetail.Mobile.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Mobile", "Mobile field is empty.");
                isValid = false;
            }
            else
            {
                if (store.AdminMgr.IsUserMobileExist(userDetail.Mobile))
                {
                    ModelState.AddModelError("Mobile", "Mobile is already registered.");
                    isValid = false;
                }
            }

            if (userDetail.Email.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Email", "Email field is empty.");
                isValid = false;
            }
            return isValid;
        }


        public bool CreateCart()
        {
            try
            {

                List<cCart> cartItems = new List<cCart>();
                Session["MYCART"] = (List<cCart>)cartItems;

                List<cCartDetails> cartDetails = new List<cCartDetails>();
                Session["CARTDETAILS"] = (List<cCartDetails>)cartDetails;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int CreateUserDetails(int userId)
        {
            try
            {

                UserDetail userDetail = new UserDetail()
                {
                    UserId = userId.ToString(),
                    Name = "",
                    Email = "",
                    Address = "",
                    Mobile = "",
                    Remarks = "",
                    MasterAreaId = 1,
                    UserStatusId = 1,
                    MasterCityId = 1,
                };

                //edb.UserDetails.Add(userDetail);
                //edb.SaveChanges();

                return userDetail.Id;
            }
            catch (Exception)
            {
                return 0;
            }

        }


        //
        // POST: /Account/LogOff
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            //clear cart and user session
            Session["CARTDETAILS"] = null;
            Session["USER"] = null;
            Session["USERID"] = null;

            return RedirectToAction("Index", "Home", new { area = "" });
        }


        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Error()
        {
            return View();
        }

    }
}
