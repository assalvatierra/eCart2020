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
using Microsoft.AspNet.Identity;

namespace eCart.Areas.Store.Controllers
{
    public class StoreUsersController : Controller
    {
        private StoreFactory store = new StoreFactory();

        // GET: Store/StoreUsers
        public ActionResult Index()
        {

            var userid = HttpContext.User.Identity.GetUserId();
            var storeDetail = store.StoreMgr.GetStoreDetailByLoginId(userid);

            if (storeDetail != null)
            {
                ViewBag.StoreId = storeDetail.Id;
            }
            else
            {
                return RedirectToAction("NoUserStore","Store");
            }

            var storeUsers = store.StoreMgr.GetStoreUserList(storeDetail.Id);
            return View(storeUsers.ToList());
        }

        // GET: Store/StoreUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreUser storeUser = store.StoreMgr.GetStoreUser((int)id);
            if (storeUser == null)
            {
                return HttpNotFound();
            }
            return View(storeUser);
        }

        // GET: Store/StoreUsers/Create
        public ActionResult Create()
        {
            var userid = HttpContext.User.Identity.GetUserId();
            var storeDetail = store.StoreMgr.GetStoreDetailByLoginId(userid);

            if (storeDetail != null)
            {
                ViewBag.StoreId = storeDetail.Id;
            }
            else
            {
                return RedirectToAction("NoUserStore", "Store");
            }

            ViewBag.StoreDetailId = new SelectList(store.RefDbLayer.GetStoreDetails(), "Id", "Name", storeDetail.Id);
            ViewBag.StoreUserTypeId = new SelectList(store.RefDbLayer.GetStoreUserTypes(), "Id", "Name");
            ViewBag.UserDetailId = new SelectList(store.RefDbLayer.GetUserDetails(), "Id", "Name");
            return View();
        }

        // POST: Store/StoreUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StoreDetailId,StoreUserTypeId,UserDetailId")] StoreUser storeUser)
        {
            if (ModelState.IsValid)
            {
                if(store.StoreMgr.AddStoreUser(storeUser))
                    return RedirectToAction("Index");
            }

            ViewBag.StoreDetailId = new SelectList(store.RefDbLayer.GetStoreDetails(), "Id", "Name", storeUser.StoreDetailId);
            ViewBag.StoreUserTypeId = new SelectList(store.RefDbLayer.GetStoreUserTypes(), "Id", "Name", storeUser.StoreUserTypeId);
            ViewBag.UserDetailId = new SelectList(store.RefDbLayer.GetUserDetails(), "Id", "Name", storeUser.UserDetailId);
            return View(storeUser);
        }

        // GET: Store/StoreUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreUser storeUser = store.StoreMgr.GetStoreUser((int)id);
            if (storeUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.StoreDetailId = new SelectList(store.RefDbLayer.GetStoreDetails(), "Id", "Name", storeUser.StoreDetailId);
            ViewBag.StoreUserTypeId = new SelectList(store.RefDbLayer.GetStoreUserTypes(), "Id", "Name", storeUser.StoreUserTypeId);
            ViewBag.UserDetailId = new SelectList(store.RefDbLayer.GetUserDetails(), "Id", "Name", storeUser.UserDetailId);
            return View(storeUser);
        }

        // POST: Store/StoreUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StoreDetailId,StoreUserTypeId,UserDetailId")] StoreUser storeUser)
        {
            if (ModelState.IsValid)
            {
                if (store.StoreMgr.EditStoreUser(storeUser))
                    return RedirectToAction("Index");
            }
            ViewBag.StoreDetailId = new SelectList(store.RefDbLayer.GetStoreDetails(), "Id", "Name", storeUser.StoreDetailId);
            ViewBag.StoreUserTypeId = new SelectList(store.RefDbLayer.GetStoreUserTypes(), "Id", "Name", storeUser.StoreUserTypeId);
            ViewBag.UserDetailId = new SelectList(store.RefDbLayer.GetUserDetails(), "Id", "Name", storeUser.UserDetailId);
            return View(storeUser);
        }

        // GET: Store/StoreUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreUser storeUser = store.StoreMgr.GetStoreUser((int)id);
            if (storeUser == null)
            {
                return HttpNotFound();
            }
            return View(storeUser);
        }

        // POST: Store/StoreUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StoreUser storeUser = store.StoreMgr.GetStoreUser((int)id);
            if (store.StoreMgr.RemoveStoreUser(storeUser))
                return RedirectToAction("Index");
            return View(storeUser);
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
