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
using eCartServices;

namespace eCart.Areas.Admin.Controllers
{
    public class ItemCategoriesController : Controller
    {
        private StoreFactory store = new StoreFactory();

        // GET: Admin/ItemCategories
        public ActionResult Index()
        {
            var itemCategories = store.AdminMgr.GetItemCategoriesList();
            return View(itemCategories);
        }

        // GET: Admin/ItemCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCategory itemCategory = store.AdminMgr.GetItemCategory((int)id);
            if (itemCategory == null)
            {
                return HttpNotFound();
            }
            return View(itemCategory);
        }

        // GET: Admin/ItemCategories/Create
        public ActionResult Create()
        {
            ViewBag.ItemCatGroupId = new SelectList(store.RefDbLayer.GetItemCatGroups(), "Id", "Name");
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
                if (store.AdminMgr.AddItemCategory(itemCategory))
                    return RedirectToAction("Index");
            }

            ViewBag.ItemCatGroupId = new SelectList(store.RefDbLayer.GetItemCatGroups(), "Id", "Name", itemCategory.ItemCatGroupId);
            return View(itemCategory);
        }

        // GET: Admin/ItemCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCategory itemCategory = store.AdminMgr.GetItemCategory((int)id);
            if (itemCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemCatGroupId = new SelectList(store.RefDbLayer.GetItemCatGroups(), "Id", "Name", itemCategory.ItemCatGroupId);
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
                if (store.AdminMgr.EditItemCategory(itemCategory))
                    return RedirectToAction("Index");
            }
            ViewBag.ItemCatGroupId = new SelectList(store.RefDbLayer.GetItemCatGroups(), "Id", "Name", itemCategory.ItemCatGroupId);
            return View(itemCategory);
        }

        // GET: Admin/ItemCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCategory itemCategory = store.AdminMgr.GetItemCategory((int)id);
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
            ItemCategory itemCategory = store.AdminMgr.GetItemCategory((int)id);
            if (store.AdminMgr.RemoveItemCategory(itemCategory))
                return RedirectToAction("Index");
            return HttpNotFound();
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
