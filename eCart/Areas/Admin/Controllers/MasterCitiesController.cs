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
    public class MasterCitiesController : Controller
    {
        private ecartdbContainer db = new ecartdbContainer();

        // GET: Admin/MasterCities
        public ActionResult Index()
        {
            return View(db.MasterCities.ToList());
        }

        // GET: Admin/MasterCities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterCity masterCity = db.MasterCities.Find(id);
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
                db.MasterCities.Add(masterCity);
                db.SaveChanges();
                return RedirectToAction("Index");
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
            MasterCity masterCity = db.MasterCities.Find(id);
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
                db.Entry(masterCity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
            MasterCity masterCity = db.MasterCities.Find(id);
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
            MasterCity masterCity = db.MasterCities.Find(id);
            db.MasterCities.Remove(masterCity);
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
