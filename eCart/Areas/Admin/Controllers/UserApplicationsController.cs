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
    public class UserApplicationsController : Controller
    {
        private ecartdbContainer db = new ecartdbContainer();

        // GET: Admin/UserApplications
        public ActionResult Index()
        {
            var userApplications = db.UserApplications.Include(u => u.UserDetail).Include(u => u.UserApplicationStatu).Include(u => u.UserApplicationType);
            return View(userApplications.ToList());
        }

        // GET: Admin/UserApplications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserApplication userApplication = db.UserApplications.Find(id);
            if (userApplication == null)
            {
                return HttpNotFound();
            }
            return View(userApplication);
        }

        // GET: Admin/UserApplications/Create
        public ActionResult Create()
        {
            ViewBag.UserDetailId = new SelectList(db.UserDetails, "Id", "UserId");
            ViewBag.UserApplicationStatusId = new SelectList(db.UserApplicationStatus, "Id", "Name");
            ViewBag.UserApplicationTypeId = new SelectList(db.UserApplicationTypes, "Id", "Name");
            return View();
        }

        // POST: Admin/UserApplications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DtApplied,Email,Mobile,UserDetailId,UserApplicationStatusId,UserApplicationTypeId")] UserApplication userApplication)
        {
            if (ModelState.IsValid)
            {
                db.UserApplications.Add(userApplication);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserDetailId = new SelectList(db.UserDetails, "Id", "UserId", userApplication.UserDetailId);
            ViewBag.UserApplicationStatusId = new SelectList(db.UserApplicationStatus, "Id", "Name", userApplication.UserApplicationStatusId);
            ViewBag.UserApplicationTypeId = new SelectList(db.UserApplicationTypes, "Id", "Name", userApplication.UserApplicationTypeId);
            return View(userApplication);
        }

        // GET: Admin/UserApplications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserApplication userApplication = db.UserApplications.Find(id);
            if (userApplication == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserDetailId = new SelectList(db.UserDetails, "Id", "UserId", userApplication.UserDetailId);
            ViewBag.UserApplicationStatusId = new SelectList(db.UserApplicationStatus, "Id", "Name", userApplication.UserApplicationStatusId);
            ViewBag.UserApplicationTypeId = new SelectList(db.UserApplicationTypes, "Id", "Name", userApplication.UserApplicationTypeId);
            return View(userApplication);
        }

        // POST: Admin/UserApplications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DtApplied,Email,Mobile,UserDetailId,UserApplicationStatusId,UserApplicationTypeId")] UserApplication userApplication)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userApplication).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserDetailId = new SelectList(db.UserDetails, "Id", "UserId", userApplication.UserDetailId);
            ViewBag.UserApplicationStatusId = new SelectList(db.UserApplicationStatus, "Id", "Name", userApplication.UserApplicationStatusId);
            ViewBag.UserApplicationTypeId = new SelectList(db.UserApplicationTypes, "Id", "Name", userApplication.UserApplicationTypeId);
            return View(userApplication);
        }

        // GET: Admin/UserApplications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserApplication userApplication = db.UserApplications.Find(id);
            if (userApplication == null)
            {
                return HttpNotFound();
            }
            return View(userApplication);
        }

        // POST: Admin/UserApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserApplication userApplication = db.UserApplications.Find(id);
            db.UserApplications.Remove(userApplication);
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
