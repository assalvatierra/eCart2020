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
    public class MasterCitiesController : Controller
    {
        private StoreFactory store = new StoreFactory();

        // GET: Admin/MasterCities
        public ActionResult Index()
        {
            return View(store.AdminMgr.GetMasterCitiesList());
        }

        // GET: Admin/MasterCities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterCity masterCity = store.AdminMgr.GetMasterCity((int)id);
            if (masterCity == null)
            {
                return HttpNotFound();
            }
            return View(masterCity);
        }

        // GET: Admin/MasterCities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/MasterCities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] MasterCity masterCity)
        {
            if (ModelState.IsValid)
            {
                if (store.AdminMgr.AddMasterCity(masterCity))
                {
                    return RedirectToAction("Index");
                }
            }

            return View(masterCity);
        }

        // GET: Admin/MasterCities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterCity masterCity = store.AdminMgr.GetMasterCity((int)id);
            if (masterCity == null)
            {
                return HttpNotFound();
            }
            return View(masterCity);
        }

        // POST: Admin/MasterCities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] MasterCity masterCity)
        {
            if (ModelState.IsValid)
            {
                if (store.AdminMgr.EditMasterCity(masterCity))
                {
                    return RedirectToAction("Index");
                }
            }
            return View(masterCity);
        }

        // GET: Admin/MasterCities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterCity masterCity = store.AdminMgr.GetMasterCity((int)id);
            if (masterCity == null)
            {
                return HttpNotFound();
            }
            return View(masterCity);
        }

        // POST: Admin/MasterCities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasterCity masterCity = store.AdminMgr.GetMasterCity(id);
            if (store.AdminMgr.RemoveMasterCity(masterCity))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
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
