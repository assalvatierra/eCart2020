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
    public class RiderDetailsController : Controller
    {
        private StoreFactory store = new StoreFactory();

        // GET: Admin/RiderDetails
        public ActionResult Index()
        {
            var riderDetails = store.AdminMgr.GetRiderDetailsList();
            return View(riderDetails.ToList());
        }

        // GET: Admin/RiderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiderDetail riderDetail = store.AdminMgr.GetRiderDetails((int)id);
            if (riderDetail == null)
            {
                return HttpNotFound();
            }
            return View(riderDetail);
        }

        // GET: Admin/RiderDetails/Create
        public ActionResult Create()
        {
            ViewBag.MasterCityId = new SelectList(store.RefDbLayer.GetMasterCities(), "Id", "Name");
            ViewBag.RiderStatusId = new SelectList(store.RefDbLayer.GetRiderStatus(), "Id", "Name");
            return View();
        }

        // POST: Admin/RiderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Name,Address,Mobile,Remarks,RiderStatusId,MasterCityId,Mobile2")] RiderDetail riderDetail)
        {
            if (ModelState.IsValid)
            {
                if(store.AdminMgr.AddRiderDetails(riderDetail))
                    return RedirectToAction("Index");
            }

            ViewBag.MasterCityId = new SelectList(store.RefDbLayer.GetMasterCities(), "Id", "Name", riderDetail.MasterCityId);
            ViewBag.RiderStatusId = new SelectList(store.RefDbLayer.GetRiderStatus(), "Id", "Name", riderDetail.RiderStatusId);
            return View(riderDetail);
        }

        // GET: Admin/RiderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiderDetail riderDetail = store.AdminMgr.GetRiderDetails((int)id);
            if (riderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.MasterCityId = new SelectList(store.RefDbLayer.GetMasterCities(), "Id", "Name", riderDetail.MasterCityId);
            ViewBag.RiderStatusId = new SelectList(store.RefDbLayer.GetRiderStatus(), "Id", "Name", riderDetail.RiderStatusId);
            return View(riderDetail);
        }

        // POST: Admin/RiderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Name,Address,Mobile,Remarks,RiderStatusId,MasterCityId,Mobile2")] RiderDetail riderDetail)
        {
            if (ModelState.IsValid)
            {
                if (store.AdminMgr.EditRiderDetails(riderDetail))
                    return RedirectToAction("Index");
            }
            ViewBag.MasterCityId = new SelectList(store.RefDbLayer.GetMasterCities(), "Id", "Name", riderDetail.MasterCityId);
            ViewBag.RiderStatusId = new SelectList(store.RefDbLayer.GetRiderStatus(), "Id", "Name", riderDetail.RiderStatusId);
            return View(riderDetail);
        }

        // GET: Admin/RiderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiderDetail riderDetail = store.AdminMgr.GetRiderDetails((int)id);
            if (riderDetail == null)
            {
                return HttpNotFound();
            }
            return View(riderDetail);
        }

        // POST: Admin/RiderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RiderDetail riderDetail = store.AdminMgr.GetRiderDetails((int)id);
            if (store.AdminMgr.RemoveRiderDetails(riderDetail))
                return RedirectToAction("Index");
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                store.AdminMgr.DbDispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult CartDetails(int id, int riderId)
        {
            var cartDetail = store.CartMgr.GetCartDetail(id) ;

            ViewBag.RiderId = riderId;
            ViewBag.StoreId = cartDetail.StoreDetailId;
            ViewBag.Store = cartDetail.StoreDetail.Name;
            //ViewBag.PaymentReceiverList = db.PaymentReceivers.ToList();
            //ViewBag.PaymentPartyList = db.PaymentParties.ToList();
            //ViewBag.PaymentStatusList = db.PaymentStatus.ToList();
            ViewBag.PaymentDetails = store.AdminMgr.GetPaymentDetails(id);
            //ViewBag.CartDelivery = db.CartDeliveries.Where(s => s.CartDetailId == id).ToList();
            ViewBag.RiderList = store.AdminMgr.GetRiderDetailsList().Where(r => r.RiderStatusId == 1).ToList();


            return View(cartDetail);
        }
    }
}
