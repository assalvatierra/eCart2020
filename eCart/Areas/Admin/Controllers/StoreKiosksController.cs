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
    public class StoreKiosksController : Controller
    {
        private ecartdbContainer db = new ecartdbContainer();
        private StoreFactory store = new StoreFactory();

        private static List<SelectListItem> SettingsTypes = new List<SelectListItem>()
        {
          new SelectListItem{Text = "Layout-A", Value = "1"},
          new SelectListItem{Text = "Layout-B", Value = "2"},
          new SelectListItem{Text = "Layout-C", Value = "3"}
        };

        // GET: Admin/StoreKiosks
        public ActionResult Index()
        {
            var storeKiosks = store.AdminMgr.GetStoreKioskList();
            return View(storeKiosks.ToList());
        }

        // GET: Admin/StoreKiosks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreKiosk storeKiosk = store.AdminMgr.GetStoreKiosk((int)id); 
            if (storeKiosk == null)
            {
                return HttpNotFound();
            }
            return View(storeKiosk);
        }

        // GET: Admin/StoreKiosks/Create
        public ActionResult Create(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId", id);
            ViewBag.SettingId = new SelectList(SettingsTypes, "Value", "Text");
            ViewBag.StoreId = id;
            return View();
        }

        // POST: Admin/StoreKiosks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StoreDetailId,KioskName,SettingId")] StoreKiosk storeKiosk)
        {
            if (ModelState.IsValid)
            {
               if(store.AdminMgr.AddStoreKiosk(storeKiosk))
                    return RedirectToAction("Details","StoreDetails", new { id = storeKiosk.StoreDetailId });
            }

            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId", storeKiosk.StoreDetailId);
            ViewBag.SettingId = new SelectList(SettingsTypes, "Value", "Text", storeKiosk.SettingId);
            ViewBag.StoreId = storeKiosk.StoreDetailId;
            return View(storeKiosk);
        }

        // GET: Admin/StoreKiosks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreKiosk storeKiosk = store.AdminMgr.GetStoreKiosk((int)id);
            if (storeKiosk == null)
            {
                return HttpNotFound();
            }
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId", storeKiosk.StoreDetailId);
            ViewBag.SettingId = new SelectList(SettingsTypes, "Value", "Text", storeKiosk.SettingId);
            ViewBag.StoreId = storeKiosk.StoreDetailId;
            return View(storeKiosk);
        }

        // POST: Admin/StoreKiosks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StoreDetailId,KioskName,SettingId")] StoreKiosk storeKiosk)
        {
            if (ModelState.IsValid)
            {
                if (store.AdminMgr.EditStoreKiosk(storeKiosk))
                    return RedirectToAction("Details", "StoreDetails", new { id = storeKiosk.StoreDetailId });
            }
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId", storeKiosk.StoreDetailId);
            ViewBag.SettingId = new SelectList(SettingsTypes, "Value", "Text", storeKiosk.SettingId);
            ViewBag.StoreId = storeKiosk.StoreDetailId;
            return View(storeKiosk);
        }

        // GET: Admin/StoreKiosks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreKiosk storeKiosk = store.AdminMgr.GetStoreKiosk((int)id);
            if (storeKiosk == null)
            {
                return HttpNotFound();
            }
            return View(storeKiosk);
        }

        // POST: Admin/StoreKiosks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StoreKiosk storeKiosk = store.AdminMgr.GetStoreKiosk((int)id);
            if (store.AdminMgr.RemoveStoreKiosk(storeKiosk))
                return RedirectToAction("Details", "StoreDetails", new { id = storeKiosk.StoreDetailId });
            return View(storeKiosk);
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
