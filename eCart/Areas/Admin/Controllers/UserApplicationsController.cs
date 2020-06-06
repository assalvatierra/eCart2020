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
using Microsoft.Ajax.Utilities;

namespace eCart.Areas.Admin.Controllers
{
    public class UserApplicationsController : Controller
    {
        private StoreFactory store = new StoreFactory();

        // GET: Admin/UserApplications
        public ActionResult Index()
        {
            var userApplications = store.AdminMgr.GetUserApplicationList(); 
            return View(userApplications.ToList());
        }

        // GET: Admin/UserApplications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserApplication userApplication = store.AdminMgr.GetUserApplication((int)id);
            if (userApplication == null)
            {
                return HttpNotFound();
            }
            return View(userApplication);
        }

        // GET: Admin/UserApplications/Create
        public ActionResult Create()
        {
            ViewBag.UserDetailId = new SelectList(store.RefDbLayer.GetUserDetails(), "Id", "UserId");
            ViewBag.UserApplicationStatusId = new SelectList(store.RefDbLayer.GetUserApplicationStatus(), "Id", "Name");
            ViewBag.UserApplicationTypeId = new SelectList(store.RefDbLayer.GetUserApplicationTypes(), "Id", "Name");
            return View();
        }

        // POST: Admin/UserApplications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DtApplied,Email,Mobile,UserDetailId,UserApplicationStatusId,UserApplicationTypeId")] UserApplication userApplication)
        {
            if (ModelState.IsValid)
            {
                if(store.AdminMgr.AddUserApplication(userApplication))
                    return RedirectToAction("Index");
            }

            ViewBag.UserDetailId = new SelectList(store.RefDbLayer.GetUserDetails(), "Id", "UserId", userApplication.UserDetailId);
            ViewBag.UserApplicationStatusId = new SelectList(store.RefDbLayer.GetUserApplicationStatus(), "Id", "Name", userApplication.UserApplicationStatusId);
            ViewBag.UserApplicationTypeId = new SelectList(store.RefDbLayer.GetUserApplicationTypes(), "Id", "Name", userApplication.UserApplicationTypeId);
            return View(userApplication);
        }

        // GET: Admin/UserApplications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserApplication userApplication = store.AdminMgr.GetUserApplication((int)id);
            if (userApplication == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserDetailId = new SelectList(store.RefDbLayer.GetUserDetails(), "Id", "UserId", userApplication.UserDetailId);
            ViewBag.UserApplicationStatusId = new SelectList(store.RefDbLayer.GetUserApplicationStatus(), "Id", "Name", userApplication.UserApplicationStatusId);
            ViewBag.UserApplicationTypeId = new SelectList(store.RefDbLayer.GetUserApplicationTypes(), "Id", "Name", userApplication.UserApplicationTypeId);
            return View(userApplication);
        }

        // POST: Admin/UserApplications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DtApplied,Email,Mobile,UserDetailId,UserApplicationStatusId,UserApplicationTypeId")] UserApplication userApplication)
        {
            if (ModelState.IsValid)
            {
                if (store.AdminMgr.EditUserApplication(userApplication))
                    return RedirectToAction("Index");
            }
            ViewBag.UserDetailId = new SelectList(store.RefDbLayer.GetUserDetails(), "Id", "UserId", userApplication.UserDetailId);
            ViewBag.UserApplicationStatusId = new SelectList(store.RefDbLayer.GetUserApplicationStatus(), "Id", "Name", userApplication.UserApplicationStatusId);
            ViewBag.UserApplicationTypeId = new SelectList(store.RefDbLayer.GetUserApplicationTypes(), "Id", "Name", userApplication.UserApplicationTypeId);
            return View(userApplication);
        }

        // GET: Admin/UserApplications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserApplication userApplication = store.AdminMgr.GetUserApplication((int)id);
            if (userApplication == null)
            {
                return HttpNotFound();
            }
            return View(userApplication);
        }

        // POST: Admin/UserApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserApplication userApplication = store.AdminMgr.GetUserApplication((int)id);

            if (store.AdminMgr.RemoveUserApplication(userApplication))
                return RedirectToAction("Index");
            return View(userApplication);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                store.AdminMgr.DbDispose();
            }
            base.Dispose(disposing);
        }


        #region Create Store

        // GET: Admin/StoreDetails/Create
        public ActionResult CreateStore(int id)
        {
            var userAppDetails = store.AdminMgr.GetUserApplication(id);

            StoreDetail newStore = new StoreDetail()
            {
                LoginId = userAppDetails.UserDetail.UserId
            };

            ViewBag.MasterAreaId = new SelectList(store.RefDbLayer.GetMasterAreas(), "Id", "Name");
            ViewBag.MasterCityId = new SelectList(store.RefDbLayer.GetMasterCities(), "Id", "Name");
            ViewBag.StoreCategoryId = new SelectList(store.RefDbLayer.GetStoreCategories(), "Id", "Name");
            ViewBag.StoreStatusId = new SelectList(store.RefDbLayer.GetStoreStatus(), "Id", "Name");
            ViewBag.UserApplicationId = id;
            return View(newStore);
        }

        // POST: Admin/StoreDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStore([Bind(Include = "Id,LoginId,Name,Address,Remarks,StoreStatusId,StoreCategoryId,MasterCityId,MasterAreaId")] StoreDetail storeDetail, int? userApplicationId)
        {
            if (ModelState.IsValid)
            {
                if (CheckRegistrationFields(storeDetail, userApplicationId))
                {

                    if (store.AdminMgr.AddStoreDetails(storeDetail))
                    {
                        //update Application status
                        //get user application 
                        var userAppDetails = store.AdminMgr.GetUserApplication((int)userApplicationId);
                        userAppDetails.UserApplicationStatusId = 2; //Accepted
                        if (store.AdminMgr.EditUserApplication(userAppDetails))
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Unable to Update Application status to accepted");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to Add new Store Detail. ");
                    }

                }
            }

            ViewBag.MasterAreaId = new SelectList(store.RefDbLayer.GetMasterAreas(), "Id", "Name", storeDetail.MasterAreaId);
            ViewBag.MasterCityId = new SelectList(store.RefDbLayer.GetMasterCities(), "Id", "Name", storeDetail.MasterCityId);
            ViewBag.StoreCategoryId = new SelectList(store.RefDbLayer.GetStoreCategories(), "Id", "Name", storeDetail.StoreCategoryId);
            ViewBag.StoreStatusId = new SelectList(store.RefDbLayer.GetStoreStatus(), "Id", "Name", storeDetail.StoreStatusId);
            ViewBag.UserApplicationId = userApplicationId;
            return View(storeDetail);
        }


        public bool CheckRegistrationFields(StoreDetail storeDetail, int? appId)
        {
            bool isValid = true;
            if (storeDetail.Name.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Name", "Name field is empty.");
                isValid = false;
            }
            if (storeDetail.Address.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Address", "Address field is empty.");
                isValid = false;
            }
            if (storeDetail.LoginId.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("LoginId", "Email field is empty.");
                isValid = false;
            }

            if (appId == null)
            {
                ModelState.AddModelError("", "Application Id field is empty.");
                isValid = false;
            }
            return isValid;
        }
        #endregion


        #region Create Rider

        // GET: Admin/RiderDetails/Create
        public ActionResult CreateRider(int id)
        {
            var userAppDetails = store.AdminMgr.GetUserApplication(id);
            var userDetails = userAppDetails.UserDetail;
            RiderDetail newRider = new RiderDetail()
            {
                UserId = userDetails.UserId,
                Name = userDetails.Name,
                Address = userDetails.Address,
                Mobile = userDetails.Mobile
            };

            ViewBag.MasterCityId = new SelectList(store.RefDbLayer.GetMasterCities(), "Id", "Name", userDetails.MasterCityId);
            ViewBag.RiderStatusId = new SelectList(store.RefDbLayer.GetRiderStatus(), "Id", "Name", 1);
            ViewBag.UserApplicationId = id;
            return View(newRider);
        }

        // POST: Admin/RiderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRider([Bind(Include = "Id,UserId,Name,Address,Mobile,Remarks,RiderStatusId,MasterCityId,Mobile2")] RiderDetail riderDetail, int? userApplicationId)
        {
            if (ModelState.IsValid)
            {
                if (CheckRiderRegFields(riderDetail, userApplicationId))
                {

                    if (store.AdminMgr.AddRiderDetails(riderDetail))
                    {
                        //update Application status
                        //get user application 
                        var userAppDetails = store.AdminMgr.GetUserApplication((int)userApplicationId);
                        userAppDetails.UserApplicationStatusId = 2; //Accepted
                        if (store.AdminMgr.EditUserApplication(userAppDetails))
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Unable to Update Application status to accepted");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to Add new Store Detail. ");
                    }
                }
            }

            ViewBag.MasterCityId = new SelectList(store.RefDbLayer.GetMasterCities(), "Id", "Name", riderDetail.MasterCityId);
            ViewBag.RiderStatusId = new SelectList(store.RefDbLayer.GetRiderStatus(), "Id", "Name", riderDetail.RiderStatusId);
            ViewBag.UserApplicationId = userApplicationId;
            return View(riderDetail);
        }



        public bool CheckRiderRegFields(RiderDetail riderDetail, int? appId)
        {
            bool isValid = true;
            if (riderDetail.Name.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Name", "Name field is empty.");
                isValid = false;
            }

            if (riderDetail.Address.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Address", "Address field is empty.");
                isValid = false;
            }

            if (riderDetail.UserId.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("UserId", "UserId field is empty.");
                isValid = false;
            }

            if (riderDetail.Mobile.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("Mobile", "Mobile field is empty.");
                isValid = false;
            }

            if (appId == null)
            {
                ModelState.AddModelError("", "Application Id field is empty.");
                isValid = false;
            }

            return isValid;
        }
        #endregion
    }
}
