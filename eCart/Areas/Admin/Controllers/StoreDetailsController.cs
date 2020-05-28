using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eCartModels;

namespace eCart.Areas.Admin.Controllers
{
    public class StoreDetailsController : Controller
    {
        private ecartdbContainer db = new ecartdbContainer();

        // GET: Admin/StoreDetails
        public ActionResult Index()
        {
            var storeDetails = db.StoreDetails.Include(s => s.MasterArea).Include(s => s.MasterCity).Include(s => s.StoreCategory).Include(s => s.StoreStatu);
            return View(storeDetails.ToList());
        }

        // GET: Admin/StoreDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreDetail storeDetail = db.StoreDetails.Find(id);
            if (storeDetail == null)
            {
                return HttpNotFound();
            }
            return View(storeDetail);
        }

        // GET: Admin/StoreDetails/Create
        public ActionResult Create()
        {
            ViewBag.MasterAreaId = new SelectList(db.MasterAreas, "Id", "Name");
            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name");
            ViewBag.StoreCategoryId = new SelectList(db.StoreCategories, "Id", "Name");
            ViewBag.StoreStatusId = new SelectList(db.StoreStatus, "Id", "Name");
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
                db.StoreDetails.Add(storeDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MasterAreaId = new SelectList(db.MasterAreas, "Id", "Name", storeDetail.MasterAreaId);
            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name", storeDetail.MasterCityId);
            ViewBag.StoreCategoryId = new SelectList(db.StoreCategories, "Id", "Name", storeDetail.StoreCategoryId);
            ViewBag.StoreStatusId = new SelectList(db.StoreStatus, "Id", "Name", storeDetail.StoreStatusId);
            return View(storeDetail);
        }

        // GET: Admin/StoreDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreDetail storeDetail = db.StoreDetails.Find(id);
            if (storeDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.MasterAreaId = new SelectList(db.MasterAreas, "Id", "Name", storeDetail.MasterAreaId);
            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name", storeDetail.MasterCityId);
            ViewBag.StoreCategoryId = new SelectList(db.StoreCategories, "Id", "Name", storeDetail.StoreCategoryId);
            ViewBag.StoreStatusId = new SelectList(db.StoreStatus, "Id", "Name", storeDetail.StoreStatusId);
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
                db.Entry(storeDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MasterAreaId = new SelectList(db.MasterAreas, "Id", "Name", storeDetail.MasterAreaId);
            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name", storeDetail.MasterCityId);
            ViewBag.StoreCategoryId = new SelectList(db.StoreCategories, "Id", "Name", storeDetail.StoreCategoryId);
            ViewBag.StoreStatusId = new SelectList(db.StoreStatus, "Id", "Name", storeDetail.StoreStatusId);
            return View(storeDetail);
        }

        // GET: Admin/StoreDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreDetail storeDetail = db.StoreDetails.Find(id);
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
            StoreDetail storeDetail = db.StoreDetails.Find(id);
            db.StoreDetails.Remove(storeDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
