using eCartModels;
using eCartServices;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace eCart.Areas.Shopper.Controllers
{
    public class UserDetailsController : Controller
    {

        StoreFactory store = new StoreFactory();

        // GET: Shopper/UserDetails
        public ActionResult Index()
        {

            var userId = HttpContext.User.Identity.GetUserId();
            var userDetails = store.UserMgr.GetUserDetailsbyUserId(userId);

            if (userDetails == null)
            {
                return RedirectToAction("Error", "Accounts");
            }

            ViewBag.AppTypes = new SelectList(store.RefDbLayer.GetUserApplicationTypes(), "Id", "Name");
            ViewBag.hasStoreApplication = store.UserMgr.HasStoreApplication(userDetails.Id);
            ViewBag.hasRiderApplication = store.UserMgr.HasRiderApplication(userDetails.Id);
            return View(userDetails);
        }



        // GET: Admin/UserDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userDetail = store.AdminMgr.GetUserDetail((int)id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }

            ViewBag.MasterAreaId = new SelectList(store.RefDbLayer.GetMasterAreas(), "Id", "Name", userDetail.MasterAreaId);
            ViewBag.MasterCityId = new SelectList(store.RefDbLayer.GetMasterCities(), "Id", "Name", userDetail.MasterCityId);
            ViewBag.UserStatusId = new SelectList(store.RefDbLayer.GetUserStatusList(), "Id", "Name", userDetail.UserStatusId);
            return View(userDetail);
        }

        // POST: Admin/UserDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Name,Address,Email,Mobile,Remarks,UserStatusId,MasterCityId,MasterAreaId")] UserDetail userDetail)
        {
            if (ModelState.IsValid)
            {
                if (store.AdminMgr.EditUserDetails(userDetail))
                {
                    return RedirectToAction("Index");
                }

            }

            ViewBag.MasterAreaId = new SelectList(store.RefDbLayer.GetMasterAreas(), "Id", "Name", userDetail.MasterAreaId);
            ViewBag.MasterCityId = new SelectList(store.RefDbLayer.GetMasterCities(), "Id", "Name", userDetail.MasterCityId);
            ViewBag.UserStatusId = new SelectList(store.RefDbLayer.GetUserStatusList(), "Id", "Name", userDetail.UserStatusId);
            return View(userDetail);
        }

        [HttpPost]
        public bool CreateAccountApp(int userId, string email, string mobile, int typeId)
        {
            try
            {

                //create application
                var newUserApplication = new UserApplication()
                {
                    DtApplied = DateTime.Now,
                    Email = email,
                    Mobile = mobile,
                    UserDetailId = userId,
                    UserApplicationTypeId = typeId,
                    UserApplicationStatusId = 1 //pending
                    
                };

                return store.UserMgr.AddUserApplication(newUserApplication);
            }
            catch
            {
                return false;
            }
        }

    }
}