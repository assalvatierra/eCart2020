using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using eCartModels;
using eCartServices;
using Microsoft.Ajax.Utilities;

namespace eCart.Areas.Admin.Controllers
{
    public class StoreDetailsController : Controller
    {
        private StoreFactory store = new StoreFactory();

        // GET: Admin/StoreDetails
        public ActionResult Index()
        {
            var storeDetails = store.RefDbLayer.GetStoreDetails().Include(s => s.MasterArea).Include(s => s.MasterCity).Include(s => s.StoreCategory).Include(s => s.StoreStatu);
            return View(storeDetails.ToList());
        }

        // GET: Admin/StoreDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreDetail storeDetail = store.RefDbLayer.GetStoreDetails().Where(s=>s.Id == id).FirstOrDefault();
            if (storeDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.StoreKiosks = store.StoreMgr.GetStoreKiosks(storeDetail.Id);

            return View(storeDetail);
        }

        // GET: Admin/StoreDetails/Create
        public ActionResult Create()
        {

            ViewBag.LoginId = new SelectList(store.RefDbLayer.GetAspNetUsers().ToList());
            ViewBag.MasterAreaId = new SelectList(store.RefDbLayer.GetMasterAreas(), "Id", "Name");
            ViewBag.MasterCityId = new SelectList(store.RefDbLayer.GetMasterCities(), "Id", "Name");
            ViewBag.StoreCategoryId = new SelectList(store.RefDbLayer.GetStoreCategories(), "Id", "Name");
            ViewBag.StoreStatusId = new SelectList(store.RefDbLayer.GetStoreStatus(), "Id", "Name");
            return View();
        }

        // POST: Admin/StoreDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LoginId,Name,Address,Remarks,StoreStatusId,StoreCategoryId,MasterCityId,MasterAreaId")] StoreDetail storeDetail)
        {
            if (ModelState.IsValid)
            {

                if (store.AdminMgr.AddStoreDetails(storeDetail))
                {
                    return RedirectToAction("Index");
                }

            }

            ViewBag.LoginId = new SelectList(store.RefDbLayer.GetAspNetUsers().ToList(), storeDetail.LoginId);
            ViewBag.MasterAreaId = new SelectList(store.RefDbLayer.GetMasterAreas(), "Id", "Name", storeDetail.MasterAreaId);
            ViewBag.MasterCityId = new SelectList(store.RefDbLayer.GetMasterCities(), "Id", "Name", storeDetail.MasterCityId);
            ViewBag.StoreCategoryId = new SelectList(store.RefDbLayer.GetStoreCategories(), "Id", "Name", storeDetail.StoreCategoryId);
            ViewBag.StoreStatusId = new SelectList(store.RefDbLayer.GetStoreStatus(), "Id", "Name", storeDetail.StoreStatusId);
            return View(storeDetail);
        }

        // GET: Admin/StoreDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreDetail storeDetail = store.AdminMgr.GetStoreDetail((int)id);
            if (storeDetail == null)
            {
                return HttpNotFound();
            }

            var identityUSers = store.RefDbLayer.GetAspNetUsers();
            List<SelectListItem> userList = new List<SelectListItem>();
            foreach(IdentityUSer user in identityUSers)
            {
                userList.Add(new SelectListItem()
                {
                    Text = user.UserName,
                    Value = user.LoginId
                });
            }
            ViewBag.LoginId = new SelectList(userList,"Value","Text", storeDetail.LoginId);
            ViewBag.MasterAreaId = new SelectList(store.RefDbLayer.GetMasterAreas(), "Id", "Name", storeDetail.MasterAreaId);
            ViewBag.MasterCityId = new SelectList(store.RefDbLayer.GetMasterCities(), "Id", "Name", storeDetail.MasterCityId);
            ViewBag.StoreCategoryId = new SelectList(store.RefDbLayer.GetStoreCategories(), "Id", "Name", storeDetail.StoreCategoryId);
            ViewBag.StoreStatusId = new SelectList(store.RefDbLayer.GetStoreStatus(), "Id", "Name", storeDetail.StoreStatusId);
            return View(storeDetail);
        }

        // POST: Admin/StoreDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LoginId,Name,Address,Remarks,StoreStatusId,StoreCategoryId,MasterCityId,MasterAreaId")] StoreDetail storeDetail)
        {
            if (ModelState.IsValid)
            {
                
                if (store.AdminMgr.EditStoreDetails(storeDetail))
                {
                    return RedirectToAction("Index");
                }
            }

            var identityUSers = store.RefDbLayer.GetAspNetUsers();
            List<SelectListItem> userList = new List<SelectListItem>();
            foreach (IdentityUSer user in identityUSers)
            {
                userList.Add(new SelectListItem()
                {
                    Text = user.UserName,
                    Value = user.LoginId
                });
            }
            ViewBag.LoginId = new SelectList(userList, "Value", "Text", storeDetail.LoginId);

            ViewBag.MasterAreaId = new SelectList(store.RefDbLayer.GetMasterAreas(), "Id", "Name", storeDetail.MasterAreaId);
            ViewBag.MasterCityId = new SelectList(store.RefDbLayer.GetMasterCities(), "Id", "Name", storeDetail.MasterCityId);
            ViewBag.StoreCategoryId = new SelectList(store.RefDbLayer.GetStoreCategories(), "Id", "Name", storeDetail.StoreCategoryId);
            ViewBag.StoreStatusId = new SelectList(store.RefDbLayer.GetStoreStatus(), "Id", "Name", storeDetail.StoreStatusId);
            return View(storeDetail);
        }

        // GET: Admin/StoreDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreDetail storeDetail = store.AdminMgr.GetStoreDetail((int)id);
            if (storeDetail == null)
            {
                return HttpNotFound();
            }
            return View(storeDetail);
        }

        // POST: Admin/StoreDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StoreDetail storeDetail = store.AdminMgr.GetStoreDetail((int)id);

            if (store.AdminMgr.RemovestoreDetails(storeDetail))
            {
                return RedirectToAction("Index");
            }

            return View(storeDetail);
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
