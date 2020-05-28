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
    public class MasterAreasController : Controller
    {
        private ecartdbContainer db = new ecartdbContainer();

        // GET: Admin/MasterAreas
        public ActionResult Index()
        {
            var masterAreas = db.MasterAreas.Include(m => m.MasterCity);
            return View(masterAreas.ToList());
        }

        // GET: Admin/MasterAreas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterArea masterArea = db.MasterAreas.Find(id);
            if (masterArea == null)
            {
                return HttpNotFound();
            }
            return View(masterArea);
        }

        // GET: Admin/MasterAreas/Create
        public ActionResult Create()
        {
            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name");
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
                db.MasterAreas.Add(masterArea);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name", masterArea.MasterCityId);
            return View(masterArea);
        }

        // GET: Admin/MasterAreas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterArea masterArea = db.MasterAreas.Find(id);
            if (masterArea == null)
            {
                return HttpNotFound();
            }
            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name", masterArea.MasterCityId);
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
                db.Entry(masterArea).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name", masterArea.MasterCityId);
            return View(masterArea);
        }

        // GET: Admin/MasterAreas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterArea masterArea = db.MasterAreas.Find(id);
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
            MasterArea masterArea = db.MasterAreas.Find(id);
            db.MasterAreas.Remove(masterArea);
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
