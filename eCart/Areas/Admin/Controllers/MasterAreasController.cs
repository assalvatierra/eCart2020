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
    public class MasterAreasController : Controller
    {
        private StoreFactory store = new StoreFactory();
        // GET: Admin/MasterAreas
        public ActionResult Index()
        {
            var masterAreas = store.AdminMgr.GetMasterAreaList();
            return View(masterAreas.ToList());
        }

        // GET: Admin/MasterAreas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterArea masterArea = store.AdminMgr.GetMasterArea((int)id);
            if (masterArea == null)
            {
                return HttpNotFound();
            }
            return View(masterArea);
        }

        // GET: Admin/MasterAreas/Create
        public ActionResult Create()
        {
            ViewBag.MasterCityId = new SelectList(store.RefDbLayer.GetMasterCities(), "Id", "Name");
            return View();
        }

        // POST: Admin/MasterAreas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,MasterCityId")] MasterArea masterArea)
        {
            if (ModelState.IsValid)
            {
                if (store.AdminMgr.AddMasterArea(masterArea))
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.MasterCityId = new SelectList(store.RefDbLayer.GetMasterCities(), "Id", "Name", masterArea.MasterCityId);
            return View(masterArea);
        }

        // GET: Admin/MasterAreas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterArea masterArea = store.AdminMgr.GetMasterArea((int)id);
            if (masterArea == null)
            {
                return HttpNotFound();
            }
            ViewBag.MasterCityId = new SelectList(store.RefDbLayer.GetMasterCities(), "Id", "Name", masterArea.MasterCityId);
            return View(masterArea);
        }

        // POST: Admin/MasterAreas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,MasterCityId")] MasterArea masterArea)
        {
            if (ModelState.IsValid)
            {
                if (store.AdminMgr.EditMasterArea(masterArea))
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            ViewBag.MasterCityId = new SelectList(store.RefDbLayer.GetMasterCities(), "Id", "Name", masterArea.MasterCityId);
            return View(masterArea);
        }

        // GET: Admin/MasterAreas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterArea masterArea = store.AdminMgr.GetMasterArea((int)id);
            if (masterArea == null)
            {
                return HttpNotFound();
            }
            return View(masterArea);
        }

        // POST: Admin/MasterAreas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterArea masterArea = store.AdminMgr.GetMasterArea((int)id);
            if (store.AdminMgr.RemoveMasterArea(masterArea))
            {
                return RedirectToAction("Index");
            }
            return View(masterArea);
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
