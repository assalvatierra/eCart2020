using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eCartModels;
using eCart.Models;
using eCartDbLayer;

namespace eCart.Areas.Store.Controllers
{
    public class PaymentDetailsController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Store/PaymentDetails
        public ActionResult Index()
        {
            var paymentDetails = db.PaymentDetails.Include(p => p.CartDetail).Include(p => p.PaymentReceiver).Include(p => p.PaymentStatu).Include(p => p.PaymentParty);
            return View(paymentDetails.ToList());
        }

        // GET: Store/PaymentDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentDetail paymentDetail = db.PaymentDetails.Find(id);
            if (paymentDetail == null)
            {
                return HttpNotFound();
            }
            return View(paymentDetail);
        }

        // GET: Store/PaymentDetails/Create
        public ActionResult Create()
        {
            ViewBag.CartDetailId = new SelectList(db.CartDetails, "Id", "DeliveryType");
            ViewBag.PaymentReceiverId = new SelectList(db.PaymentReceivers, "Id", "Description");
            ViewBag.PaymentStatusId = new SelectList(db.PaymentStatus, "Id", "Name");
            ViewBag.PaymentPartyId = new SelectList(db.PaymentParties, "Id", "Name");
            return View();
        }

        // POST: Store/PaymentDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CartDetailId,Amount,dtPayment,PaymentReceiverId,ReceiverInfo,PaymentPartyId,PartyInfo,PaymentStatusId")] PaymentDetail paymentDetail)
        {
            if (ModelState.IsValid)
            {
                db.PaymentDetails.Add(paymentDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CartDetailId = new SelectList(db.CartDetails, "Id", "DeliveryType", paymentDetail.CartDetailId);
            ViewBag.PaymentReceiverId = new SelectList(db.PaymentReceivers, "Id", "Description", paymentDetail.PaymentReceiverId);
            ViewBag.PaymentStatusId = new SelectList(db.PaymentStatus, "Id", "Name", paymentDetail.PaymentStatusId);
            ViewBag.PaymentPartyId = new SelectList(db.PaymentParties, "Id", "Name", paymentDetail.PaymentPartyId);
            return View(paymentDetail);
        }

        // GET: Store/PaymentDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentDetail paymentDetail = db.PaymentDetails.Find(id);
            if (paymentDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.CartDetailId = new SelectList(db.CartDetails, "Id", "DeliveryType", paymentDetail.CartDetailId);
            ViewBag.PaymentReceiverId = new SelectList(db.PaymentReceivers, "Id", "Description", paymentDetail.PaymentReceiverId);
            ViewBag.PaymentStatusId = new SelectList(db.PaymentStatus, "Id", "Name", paymentDetail.PaymentStatusId);
            ViewBag.PaymentPartyId = new SelectList(db.PaymentParties, "Id", "Name", paymentDetail.PaymentPartyId);
            return View(paymentDetail);
        }

        // POST: Store/PaymentDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CartDetailId,Amount,dtPayment,PaymentReceiverId,ReceiverInfo,PaymentPartyId,PartyInfo,PaymentStatusId")] PaymentDetail paymentDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CartDetailId = new SelectList(db.CartDetails, "Id", "DeliveryType", paymentDetail.CartDetailId);
            ViewBag.PaymentReceiverId = new SelectList(db.PaymentReceivers, "Id", "Description", paymentDetail.PaymentReceiverId);
            ViewBag.PaymentStatusId = new SelectList(db.PaymentStatus, "Id", "Name", paymentDetail.PaymentStatusId);
            ViewBag.PaymentPartyId = new SelectList(db.PaymentParties, "Id", "Name", paymentDetail.PaymentPartyId);
            return View(paymentDetail);
        }

        // GET: Store/PaymentDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentDetail paymentDetail = db.PaymentDetails.Find(id);
            if (paymentDetail == null)
            {
                return HttpNotFound();
            }
            return View(paymentDetail);
        }

        // POST: Store/PaymentDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaymentDetail paymentDetail = db.PaymentDetails.Find(id);
            db.PaymentDetails.Remove(paymentDetail);
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
