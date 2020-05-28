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
    public class ItemMastersController : Controller
    {
        private ecartdbContainer db = new ecartdbContainer();

        // GET: Admin/ItemMasters
        public ActionResult Index()
        {
            return View(db.ItemMasters.ToList());
        }

        // GET: Admin/ItemMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemMaster itemMaster = db.ItemMasters.Find(id);
            if (itemMaster == null)
            {
                return HttpNotFound();
            }
            return View(itemMaster);
        }

        // GET: Admin/ItemMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ItemMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] ItemMaster itemMaster)
        {
            if (ModelState.IsValid)
            {
                db.ItemMasters.Add(itemMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(itemMaster);
        }

        // GET: Admin/ItemMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemMaster itemMaster = db.ItemMasters.Find(id);
            if (itemMaster == null)
            {
                return HttpNotFound();
            }
            return View(itemMaster);
        }

        // POST: Admin/ItemMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] ItemMaster itemMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(itemMaster);
        }

        // GET: Admin/ItemMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemMaster itemMaster = db.ItemMasters.Find(id);
            if (itemMaster == null)
            {
                return HttpNotFound();
            }
            return View(itemMaster);
        }

        // POST: Admin/ItemMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemMaster itemMaster = db.ItemMasters.Find(id);
            db.ItemMasters.Remove(itemMaster);
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
