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
    public class UserDetailsController : Controller
    {
        private ecartdbContainer db = new ecartdbContainer();

        // GET: Admin/UserDetails
        public ActionResult Index()
        {
            var userDetails = db.UserDetails.Include(u => u.MasterArea).Include(u => u.MasterCity).Include(u => u.UserStatu);
            return View(userDetails.ToList());
        }

        // GET: Admin/UserDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userDetail = db.UserDetails.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }

        // GET: Admin/UserDetails/Create
        public ActionResult Create()
        {
            ViewBag.MasterAreaId = new SelectList(db.MasterAreas, "Id", "Name");
            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name");
            ViewBag.UserStatusId = new SelectList(db.UserStatus, "Id", "Name");
            return View();
        }

        // POST: Admin/UserDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Name,Address,Email,Mobile,Remarks,UserStatusId,MasterCityId,MasterAreaId")] UserDetail userDetail)
        {
            if (ModelState.IsValid)
            {
                db.UserDetails.Add(userDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MasterAreaId = new SelectList(db.MasterAreas, "Id", "Name", userDetail.MasterAreaId);
            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name", userDetail.MasterCityId);
            ViewBag.UserStatusId = new SelectList(db.UserStatus, "Id", "Name", userDetail.UserStatusId);
            return View(userDetail);
        }

        // GET: Admin/UserDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userDetail = db.UserDetails.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.MasterAreaId = new SelectList(db.MasterAreas, "Id", "Name", userDetail.MasterAreaId);
            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name", userDetail.MasterCityId);
            ViewBag.UserStatusId = new SelectList(db.UserStatus, "Id", "Name", userDetail.UserStatusId);
            return View(userDetail);
        }

        // POST: Admin/UserDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Name,Address,Email,Mobile,Remarks,UserStatusId,MasterCityId,MasterAreaId")] UserDetail userDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MasterAreaId = new SelectList(db.MasterAreas, "Id", "Name", userDetail.MasterAreaId);
            ViewBag.MasterCityId = new SelectList(db.MasterCities, "Id", "Name", userDetail.MasterCityId);
            ViewBag.UserStatusId = new SelectList(db.UserStatus, "Id", "Name", userDetail.UserStatusId);
            return View(userDetail);
        }

        // GET: Admin/UserDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userDetail = db.UserDetails.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }

        // POST: Admin/UserDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserDetail userDetail = db.UserDetails.Find(id);
            db.UserDetails.Remove(userDetail);
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
