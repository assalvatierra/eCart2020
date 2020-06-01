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
    public class ItemCatGroupsController : Controller
    {
        private StoreFactory store = new StoreFactory();

        // GET: Admin/ItemCatGroups
        public ActionResult Index()
        {
            return View(store.AdminMgr.GetItemCatGroupList());
        }

        // GET: Admin/ItemCatGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCatGroup itemCatGroup = store.AdminMgr.GetItemCatGroup((int)id);
            if (itemCatGroup == null)
            {
                return HttpNotFound();
            }
            return View(itemCatGroup);
        }

        // GET: Admin/ItemCatGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ItemCatGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,SortOrder")] ItemCatGroup itemCatGroup)
        {
            if (ModelState.IsValid)
            {
                if(store.AdminMgr.AddItemCatGroup(itemCatGroup))
                    return RedirectToAction("Index");
            }

            return View(itemCatGroup);
        }

        // GET: Admin/ItemCatGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCatGroup itemCatGroup = store.AdminMgr.GetItemCatGroup((int)id);
            if (itemCatGroup == null)
            {
                return HttpNotFound();
            }
            return View(itemCatGroup);
        }

        // POST: Admin/ItemCatGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,SortOrder")] ItemCatGroup itemCatGroup)
        {
            if (ModelState.IsValid)
            {
                if (store.AdminMgr.EditItemCatGroup(itemCatGroup))
                    return RedirectToAction("Index");
            }
            return View(itemCatGroup);
        }

        // GET: Admin/ItemCatGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCatGroup itemCatGroup = store.AdminMgr.GetItemCatGroup((int)id);
            if (itemCatGroup == null)
            {
                return HttpNotFound();
            }
            return View(itemCatGroup);
        }

        // POST: Admin/ItemCatGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemCatGroup itemCatGroup = store.AdminMgr.GetItemCatGroup((int)id);
            if (store.AdminMgr.RemoveItemCatGroup(itemCatGroup))
                return RedirectToAction("Index");
            return View(itemCatGroup);
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
