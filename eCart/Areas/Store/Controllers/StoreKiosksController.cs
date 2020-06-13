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
using Microsoft.AspNet.Identity;

namespace eCart.Areas.Store.Controllers
{
    public class StoreKiosksController : Controller
    {
        private ecartdbContainer db = new ecartdbContainer();
        private StoreFactory store = new StoreFactory();

        private int CASHIER = 2;
        private static List<SelectListItem> SettingsTypes = new List<SelectListItem>()
        {
          new SelectListItem{Text = "Layout-A", Value = "1"},
          new SelectListItem{Text = "Layout-B", Value = "2"},
          new SelectListItem{Text = "Layout-C", Value = "3"}
        };

        // GET: Store/StoreKiosks
        public ActionResult Index()
        {

            var userid = HttpContext.User.Identity.GetUserId();
            var storeDetail = store.StoreMgr.GetStoreDetailByLoginId(userid);
            if (storeDetail == null)
            {
               return RedirectToAction("NoUserStore", "Store",null);
            }

            var storeKiosks = db.StoreKiosks.Include(s => s.StoreDetail).Where(s=>s.StoreDetailId == storeDetail.Id);
            return View(storeKiosks.ToList());
        }

        // GET: Store/StoreKiosks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreKiosk storeKiosk = db.StoreKiosks.Find(id);
            if (storeKiosk == null)
            {
                return HttpNotFound();
            }
            return View(storeKiosk);
        }

        // GET: Store/StoreKiosks/Create
        public ActionResult Create()
        {
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId");
            ViewBag.SettingId = new SelectList(SettingsTypes, "Value", "Text");
            return View();
        }

        // POST: Store/StoreKiosks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StoreDetailId,KioskName,SettingId")] StoreKiosk storeKiosk)
        {
            if (ModelState.IsValid)
            {
                db.StoreKiosks.Add(storeKiosk);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId", storeKiosk.StoreDetailId);
            ViewBag.SettingId = new SelectList(SettingsTypes, "Value", "Text", storeKiosk.SettingId);
            return View(storeKiosk);
        }

        // GET: Store/StoreKiosks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreKiosk storeKiosk = db.StoreKiosks.Find(id);
            if (storeKiosk == null)
            {
                return HttpNotFound();
            }
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId", storeKiosk.StoreDetailId);
            ViewBag.SettingId = new SelectList(SettingsTypes, "Value", "Text", storeKiosk.SettingId);
            return View(storeKiosk);
        }

        // POST: Store/StoreKiosks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StoreDetailId,KioskName,SettingId")] StoreKiosk storeKiosk)
        {
            if (ModelState.IsValid)
            {
                db.Entry(storeKiosk).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId", storeKiosk.StoreDetailId);
            ViewBag.SettingId = new SelectList(SettingsTypes, "Value", "Text", storeKiosk.SettingId);
            return View(storeKiosk);
        }

        // GET: Store/StoreKiosks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreKiosk storeKiosk = db.StoreKiosks.Find(id);
            if (storeKiosk == null)
            {
                return HttpNotFound();
            }
            return View(storeKiosk);
        }

        // POST: Store/StoreKiosks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StoreKiosk storeKiosk = db.StoreKiosks.Find(id);
            db.StoreKiosks.Remove(storeKiosk);
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
