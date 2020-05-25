using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using eCartModels;
using eCartServices;
using Microsoft.Ajax.Utilities;

namespace eCart.Areas.Shopper.Controllers
{
    public class AccountsController : Controller
    {
        ecartdbContainer edb = new ecartdbContainer();
        ShopperContext sdb = new ShopperContext();

        //AccMgr accMgr = new AccMgr();
        StoreFactory storeFactory = new StoreFactory();

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
        public ActionResult Login(int? usertype)
        {
            ViewBag.UserType = usertype; //use for altering the login page info/images and other info to fit the type of user
            return View();
        }



        public ActionResult Logout()
        {
            Session["CARTDETAILS"] = null;
            Session["USER"] = null;
            return RedirectToAction("Login");
        }

        public ActionResult Register()
        {
            ViewBag.UserStatusId = new SelectList(edb.UserStatus, "Id", "Name");
            ViewBag.MasterCityId = new SelectList(edb.MasterCities, "Id", "Name");
            ViewBag.MasterAreaId = new SelectList(edb.MasterAreas, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(AccountRegistration registration)
        {
            try
            {

                if (ModelState.IsValid && checkRegistrationFields(registration))
                {
                    if (checkEmail(registration.Username))
                    {
                        if (checkPassword(registration.Password, registration.Password2))
                        {
                            //var accMgr = storeFactory.AccMgr;

                            ////create user
                            //var userId = accMgr.CreateUser(registration.Username, registration.Password);

                            //if (userId != "Error")
                            //{
                            //    //assign user role
                            //    if (SetUserRoles(int.Parse(userId), SHOPPER))
                            //    {
                            //        //register account
                            //        registration.UserId = userId;
                            //        if (accMgr.RegisterAccount(registration))
                            //        {
                            //            // proceed to login
                            //            return RedirectToAction("Login");
                            //        }
                            //        else
                            //        {
                            //            ModelState.AddModelError("", "Unable to register a new shopper account.");
                            //        }
                            //    }
                            //}
                        }
                    }
                }
            }
            catch (Exception) { };



            ViewBag.UserStatusId = new SelectList(edb.UserStatus, "Id", "Name", registration.UserStatusId);
            ViewBag.MasterCityId = new SelectList(edb.MasterCities, "Id", "Name", registration.MasterCityId);
            ViewBag.MasterAreaId = new SelectList(edb.MasterAreas, "Id", "Name", registration.MasterAreaId);
            return View();
        }

        public bool checkRegistrationFields(AccountRegistration registration)
        {
            bool isValid = true;
            if (registration.Password.IsNullOrWhiteSpace() || registration.Password2.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Password", "Password field is empty.");
                isValid = false;
            }
            if (registration.Username.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Username", "Username field is empty.");
                isValid = false;
            }

            if (registration.Name.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Name", "Name field is empty.");
                isValid = false;
            }
            if (registration.Address.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Address", "Address field is empty.");
                isValid = false;
            }
            if (registration.Email.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Email", "Email field is empty.");
                isValid = false;
            }
            if (registration.Mobile.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Mobile", "Mobile field is empty.");
                isValid = false;
            }
            if (registration.Email.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Email", "Email field is empty.");
                isValid = false;
            }
            return isValid;
        }

        public bool checkPassword(string pass1, string pass2)
        {
            if (!pass1.IsNullOrWhiteSpace() || !pass2.IsNullOrWhiteSpace())
            {
                if (pass1 == pass2)
                {
                    return true;
                }
                else
                {
                    ModelState.AddModelError("Password", "Passwords does not match.");
                }
            }
            else
            {

                ModelState.AddModelError("Password", "Password field is empty");
            }
            return false;
        }

        public bool checkEmail(string username)
        {
            if (!username.IsNullOrWhiteSpace())
            {

                //var isUsernameExist = edb.Users.Any(u => u.Username.ToLower() == username.ToLower());
                //if (!isUsernameExist)
                //{
                //    return true;
                //}
                //else
                //{
                //    ModelState.AddModelError("Username", "Username already exists");
                //}

                ModelState.AddModelError("Username", "Username is empty");
            }
            return false;
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

                edb.UserDetails.Add(userDetail);
                edb.SaveChanges();

                return userDetail.Id;
            }
            catch (Exception)
            {
                return 0;
            }

        }

        public bool SetUserRoles(int userId, int roleId)
        {
            try
            {
                //if (edb.UserRolesMappings.Any(r => r.UserId == userId && r.RoleId == roleId))
                //{
                //    return true;
                //}

                //UserRolesMapping userRolesMapping = new UserRolesMapping()
                //{
                //    RoleId = roleId,
                //    UserId = userId
                //};
                //edb.UserRolesMappings.Add(userRolesMapping);
                //edb.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                ModelState.AddModelError(" ", "Error Creating new Shopper");
                return false;
            }
        }

        public bool checkUserRole(int userId, int roleId)
        {
            try
            {
                //if (edb.UserRolesMappings.Any(r => r.UserId == userId && r.RoleId == roleId))
                //{
                //    return true;
                //}

                ////check if user is admin
                //if (edb.UserRolesMappings.Any(r => r.UserId == userId && r.RoleId == 1))
                //{
                //    return true;
                //}

                ModelState.AddModelError("Password", "User is not a Shopper");
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
