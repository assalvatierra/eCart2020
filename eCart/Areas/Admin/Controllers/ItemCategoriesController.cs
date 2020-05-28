using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eCartModels;
using eCartDBLayer;

namespace eCart.Areas.Admin.Controllers
{
    public class ItemCategoriesController : Controller
    {
        private ecartdbContainer db = new ecartdbContainer();

        // GET: Admin/ItemCategories
        public ActionResult Index()
        {
            var itemCategories = db.ItemCategories.Include(i => i.ItemCatGroup);
            return View(itemCategories.ToList());
        }

        // GET: Admin/ItemCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCategory itemCategory = db.ItemCategories.Find(id);
            if (itemCategory == null)
            {
                return HttpNotFound();
            }
            return View(itemCategory);
        }

        // GET: Admin/ItemCategories/Create
        public ActionResult Create()
        {
            ViewBag.ItemCatGroupId = new SelectList(db.ItemCatGroups, "Id", "Name");
            return View();
        }

        // POST: Admin/ItemCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,SortOrder,ItemCatGroupId")] ItemCategory itemCategory)
        {
            if (ModelState.IsValid)
            {
                db.ItemCategories.Add(itemCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemCatGroupId = new SelectList(db.ItemCatGroups, "Id", "Name", itemCategory.ItemCatGroupId);
            return View(itemCategory);
        }

        // GET: Admin/ItemCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCategory itemCategory = db.ItemCategories.Find(id);
            if (itemCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemCatGroupId = new SelectList(db.ItemCatGroups, "Id", "Name", itemCategory.ItemCatGroupId);
            return View(itemCategory);
        }

        // POST: Admin/ItemCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,SortOrder,ItemCatGroupId")] ItemCategory itemCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemCatGroupId = new SelectList(db.ItemCatGroups, "Id", "Name", itemCategory.ItemCatGroupId);
            return View(itemCategory);
        }

        // GET: Admin/ItemCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCategory itemCategory = db.ItemCategories.Find(id);
            if (itemCategory == null)
            {
                return HttpNotFound();
            }
            return View(itemCategory);
        }

        // POST: Admin/ItemCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemCategory itemCategory = db.ItemCategories.Find(id);
            db.ItemCategories.Remove(itemCategory);
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
