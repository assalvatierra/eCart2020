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
    public class StorePaymentsController : Controller
    {
        private ecartdbContainer db = new ecartdbContainer();
        private StoreFactory storeFactory = new StoreFactory();

        // GET: Admin/StorePayments
        public ActionResult Index()
        {
            var storePayments = db.StorePayments.Include(s => s.StoreDetail).Include(s => s.StorePaymentStatu).Include(s => s.StorePaymentType);
            return View(storePayments.OrderByDescending(s=>s.dtPosted).ToList());
        }

        // GET: Admin/StorePayments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorePayment storePayment = db.StorePayments.Find(id);
            if (storePayment == null)
            {
                return HttpNotFound();
            }
            return View(storePayment);
        }

        // GET: Admin/StorePayments/Create
        public ActionResult Create()
        {
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "Name");
            ViewBag.StorePaymentStatusId = new SelectList(db.StorePaymentStatus, "Id", "Name");
            ViewBag.StorePaymentTypeId = new SelectList(db.StorePaymentTypes, "Id", "Description");
            return View();
        }

        // POST: Admin/StorePayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StoreDetailId,dtPayment,Amount,StorePaymentTypeId,Remarks,dtPosted,StorePaymentStatusId")] StorePayment storePayment)
        {
            if (ModelState.IsValid)
            {
                db.StorePayments.Add(storePayment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "Name", storePayment.StoreDetailId);
            ViewBag.StorePaymentStatusId = new SelectList(db.StorePaymentStatus, "Id", "Name", storePayment.StorePaymentStatusId);
            ViewBag.StorePaymentTypeId = new SelectList(db.StorePaymentTypes, "Id", "Description", storePayment.StorePaymentTypeId);
            return View(storePayment);
        }

        // GET: Admin/StorePayments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorePayment storePayment = db.StorePayments.Find(id);
            if (storePayment == null)
            {
                return HttpNotFound();
            }
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "Name", storePayment.StoreDetailId);
            ViewBag.StorePaymentStatusId = new SelectList(db.StorePaymentStatus, "Id", "Name", storePayment.StorePaymentStatusId);
            ViewBag.StorePaymentTypeId = new SelectList(db.StorePaymentTypes, "Id", "Description", storePayment.StorePaymentTypeId);
            return View(storePayment);
        }

        // POST: Admin/StorePayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StoreDetailId,dtPayment,Amount,StorePaymentTypeId,Remarks,dtPosted,StorePaymentStatusId")] StorePayment storePayment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(storePayment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "Name", storePayment.StoreDetailId);
            ViewBag.StorePaymentStatusId = new SelectList(db.StorePaymentStatus, "Id", "Name", storePayment.StorePaymentStatusId);
            ViewBag.StorePaymentTypeId = new SelectList(db.StorePaymentTypes, "Id", "Description", storePayment.StorePaymentTypeId);
            return View(storePayment);
        }

        // GET: Admin/StorePayments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorePayment storePayment = db.StorePayments.Find(id);
            if (storePayment == null)
            {
                return HttpNotFound();
            }
            return View(storePayment);
        }

        // POST: Admin/StorePayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StorePayment storePayment = db.StorePayments.Find(id);
            db.StorePayments.Remove(storePayment);
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

        [HttpPost]
        public void AcceptPayment(int id)
        {
            var storePayment = db.StorePayments.Find(id);
            storePayment.StorePaymentStatusId = 2;  //accepted
            db.Entry(storePayment).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
