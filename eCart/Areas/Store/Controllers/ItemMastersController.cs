using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
//using eCart.Areas.Store.Models;
using eCartModels;
using eCartDbLayer;

namespace eCart.Areas.Store.Controllers
{
    public class ItemMastersController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Store/ItemMasters
        public ActionResult Index()
        {
            return View(db.ItemMasters.ToList());
        }


        // GET: Store/ItemMasters/Details/5
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

        // GET: Store/ItemMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Store/ItemMasters/Create
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

        // GET: Store/ItemMasters/Edit/5
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

        // POST: Store/ItemMasters/Edit/5
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

        // GET: Store/ItemMasters/Delete/5
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

        // POST: Store/ItemMasters/Delete/5
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
