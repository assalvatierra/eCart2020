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
        //private ecartdbContainer db = new ecartdbContainer();
        private StoreFactory store = new StoreFactory();

        // GET: Admin/StorePayments
        public ActionResult Index()
        {
            var storePayments = store.AdminMgr.GetStorePaymentList();
            return View(storePayments.OrderByDescending(s=>s.dtPosted).ToList());
        }

        // GET: Admin/StorePayments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorePayment storePayment = store.AdminMgr.GetStorePayment((int)id);
            if (storePayment == null)
            {
                return HttpNotFound();
            }
            return View(storePayment);
        }

        // GET: Admin/StorePayments/Create
        public ActionResult Create()
        {
            ViewBag.StoreDetailId = new SelectList(store.RefDbLayer.GetStoreDetails(), "Id", "Name");
            ViewBag.StorePaymentStatusId = new SelectList(store.RefDbLayer.GetStorePaymentStatus(), "Id", "Name");
            ViewBag.StorePaymentTypeId = new SelectList(store.RefDbLayer.GetStorePaymentTypes(), "Id", "Description");
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
                if(store.AdminMgr.AddStorePayment(storePayment))
                    return RedirectToAction("Index");
            }

            ViewBag.StoreDetailId = new SelectList(store.RefDbLayer.GetStoreDetails(), "Id", "Name", storePayment.StoreDetailId);
            ViewBag.StorePaymentStatusId = new SelectList(store.RefDbLayer.GetStorePaymentStatus(), "Id", "Name", storePayment.StorePaymentStatusId);
            ViewBag.StorePaymentTypeId = new SelectList(store.RefDbLayer.GetStorePaymentTypes(), "Id", "Description", storePayment.StorePaymentTypeId);
            return View(storePayment);
        }

        // GET: Admin/StorePayments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorePayment storePayment = store.AdminMgr.GetStorePayment((int)id);
            if (storePayment == null)
            {
                return HttpNotFound();
            }
            ViewBag.StoreDetailId = new SelectList(store.RefDbLayer.GetStoreDetails(), "Id", "Name", storePayment.StoreDetailId);
            ViewBag.StorePaymentStatusId = new SelectList(store.RefDbLayer.GetStorePaymentStatus(), "Id", "Name", storePayment.StorePaymentStatusId);
            ViewBag.StorePaymentTypeId = new SelectList(store.RefDbLayer.GetStorePaymentTypes(), "Id", "Description", storePayment.StorePaymentTypeId);
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
                if (store.AdminMgr.EditStorePayment(storePayment))
                    return RedirectToAction("Index");
            }
            ViewBag.StoreDetailId = new SelectList(store.RefDbLayer.GetStoreDetails(), "Id", "Name", storePayment.StoreDetailId);
            ViewBag.StorePaymentStatusId = new SelectList(store.RefDbLayer.GetStorePaymentStatus(), "Id", "Name", storePayment.StorePaymentStatusId);
            ViewBag.StorePaymentTypeId = new SelectList(store.RefDbLayer.GetStorePaymentTypes(), "Id", "Description", storePayment.StorePaymentTypeId);
            return View(storePayment);
        }

        // GET: Admin/StorePayments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorePayment storePayment = store.AdminMgr.GetStorePayment((int)id);
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
            StorePayment storePayment = store.AdminMgr.GetStorePayment((int)id);
            if (store.AdminMgr.RemoveStorePayment(storePayment))
                return RedirectToAction("Index");
            return View(storePayment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                store.AdminMgr.DbDispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public bool AcceptPayment(int id)
        {
            var storePayment = store.AdminMgr.GetStorePayment(id);
            storePayment.StorePaymentStatusId = 2;  //accepted

            if (store.AdminMgr.EditStorePayment(storePayment))
                return true;
            return false;
        }
    }
}
