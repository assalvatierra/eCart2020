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

namespace eCart.Areas.Admin.Controllers
{
    public class UserDetailsController : Controller
    {
        private StoreFactory store = new StoreFactory();


        // GET: Admin/UserDetails
        public ActionResult Index()
        {
            var userDetails = store.AdminMgr.GetUserDetailList();
            return View(userDetails.ToList());
        }

        // GET: Admin/UserDetails/Details/5
        public ActionResult Details(int? id)
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
            return View(userDetail);
        }

        // GET: Admin/UserDetails/Create
        public ActionResult Create()
        {
            ViewBag.MasterAreaId = new SelectList(store.RefDbLayer.GetMasterAreas(), "Id", "Name");
            ViewBag.MasterCityId = new SelectList(store.RefDbLayer.GetMasterCities(), "Id", "Name");
            ViewBag.UserStatusId = new SelectList(store.RefDbLayer.GetUserStatusList(), "Id", "Name");
            return View();
        }

        // POST: Admin/UserDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Name,Address,Email,Mobile,Remarks,UserStatusId,MasterCityId,MasterAreaId")] UserDetail userDetail)
        {
            if (ModelState.IsValid)
            {
                if (store.AdminMgr.AddUserDetails(userDetail))
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.MasterAreaId = new SelectList(store.RefDbLayer.GetMasterAreas(), "Id", "Name", userDetail.MasterAreaId);
            ViewBag.MasterCityId = new SelectList(store.RefDbLayer.GetMasterCities(), "Id", "Name", userDetail.MasterCityId);
            ViewBag.UserStatusId = new SelectList(store.RefDbLayer.GetUserStatusList(), "Id", "Name", userDetail.UserStatusId);
            return View(userDetail);
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

        // GET: Admin/UserDetails/Delete/5
        public ActionResult Delete(int? id)
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
            return View(userDetail);
        }

        // POST: Admin/UserDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserDetail userDetail = store.AdminMgr.GetUserDetail((int)id);
            if (store.AdminMgr.RemoveUserDetails(userDetail))
            {
                return RedirectToAction("Index");
            }
            return View(userDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                store.AdminMgr.DbDispose();
            }
            base.Dispose(disposing);
        }
    }
}
