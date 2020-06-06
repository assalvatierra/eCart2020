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

namespace eCart.Areas.Rider.Controllers
{
    public class RiderDetailsController : Controller
    {
        private StoreFactory store = new StoreFactory();

        // GET: Rider/RiderDetails
        public ActionResult Index()
        {
            var riderMgr = store.RiderMgr;
            var userid = HttpContext.User.Identity.GetUserId();

            var riderDetail = riderMgr.GetRiderDetailByLoginId(userid);

            if (riderDetail != null)
            {
                ViewBag.RiderId = riderDetail.Id;
            }
            else
            {
                return RedirectToAction("NoUserRider");
            }

            ViewBag.Rider = riderDetail;

            var cartDeliveries = riderDetail.CartDeliveries.ToList();


            var deliveredList = new List<int>();
            cartDeliveries.ForEach(c => {
                if (c.CartDetail.CartStatusId < 5)
                {
                    deliveredList.Add(c.Id);
                }
            });


            return View(cartDeliveries.Where(c => deliveredList.Contains(c.Id)).ToList());
        }

        // GET: Rider/RiderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiderDetail riderDetail = store.RiderMgr.GetRiderDetails((int)id);
            if (riderDetail == null)
            {
                return HttpNotFound();
            }
            return View(riderDetail);
        }


        // GET: Rider/RiderDetails/Details/5
        public ActionResult DeliveredCarts(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var cartDeliveries = store.RiderMgr.GetRiderDetails((int)id).CartDeliveries.ToList();

            if (cartDeliveries == null)
            {
                return HttpNotFound();
            }

            ViewBag.Rider = store.RiderMgr.GetRiderDetails((int)id);

            var deliveredList = new List<int>();
            cartDeliveries.ForEach(c=> {
                if (c.CartDetail.CartStatusId == 5)
                {
                    deliveredList.Add(c.Id);
                }
            });


            return View(cartDeliveries.Where(c=> deliveredList.Contains(c.Id)).ToList());
        }

        // GET: Rider/RiderDetails/Create
        public ActionResult Create()
        {
            ViewBag.MasterCityId = new SelectList(store.RefDbLayer.GetMasterAreas(), "Id", "Name");
            ViewBag.RiderStatusId = new SelectList(store.RefDbLayer.GetRiderStatus(), "Id", "Name");
            return View();
        }

        // POST: Rider/RiderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Name,Address,Mobile,Remarks,RiderStatusId,MasterCityId,Mobile2")] RiderDetail riderDetail)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            ViewBag.MasterCityId = new SelectList(store.RefDbLayer.GetMasterAreas(), "Id", "Name", riderDetail.MasterCityId);
            ViewBag.RiderStatusId = new SelectList(store.RefDbLayer.GetRiderStatus(), "Id", "Name", riderDetail.RiderStatusId);
            return View(riderDetail);
        }

        // GET: Rider/RiderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiderDetail riderDetail = store.RiderMgr.GetRiderDetails((int)id);
            if (riderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.MasterCityId = new SelectList(store.RefDbLayer.GetMasterAreas(), "Id", "Name", riderDetail.MasterCityId);
            ViewBag.RiderStatusId = new SelectList(store.RefDbLayer.GetRiderStatus(), "Id", "Name", riderDetail.RiderStatusId);
            return View(riderDetail);
        }

        // POST: Rider/RiderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Name,Address,Mobile,Remarks,RiderStatusId,MasterCityId,Mobile2")] RiderDetail riderDetail)
        {
            if (ModelState.IsValid)
            {
               if(store.RiderMgr.EditRiderDetails(riderDetail))
                    return RedirectToAction("Index");
            }
            ViewBag.MasterCityId = new SelectList(store.RefDbLayer.GetMasterAreas(), "Id", "Name", riderDetail.MasterCityId);
            ViewBag.RiderStatusId = new SelectList(store.RefDbLayer.GetRiderStatus(), "Id", "Name", riderDetail.RiderStatusId);
            return View(riderDetail);
        }

        // GET: Rider/RiderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RiderDetail riderDetail = store.RiderMgr.GetRiderDetails((int)id);
            if (riderDetail == null)
            {
                return HttpNotFound();
            }
            return View(riderDetail);
        }

        // POST: Rider/RiderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RiderDetail riderDetail = store.RiderMgr.GetRiderDetails((int)id);
            if (store.RiderMgr.RemoveRiderDetails(riderDetail))
                return RedirectToAction("Index");
            return View(riderDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                store.RiderMgr.DbDispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult DeliveryDetails(int id)
        {

            CartDelivery delivery = store.RiderMgr.GetCartDelivery(id);

            var paymentId = 0;
            if (store.RiderMgr.GetRiderCashDetailsByCartDetailId(delivery.CartDetailId) != null )
            {
                paymentId = store.RiderMgr.GetRiderCashDetailsByCartDetailId(delivery.CartDetailId).Id;
                ViewBag.RiderCashDetails = store.RiderMgr.GetRiderCashDetails(paymentId);
            }
            else
            {
                ViewBag.RiderCashDetails = null ;
            }

            ViewBag.DeliveryStatus = store.RiderMgr.getLastestActivity(delivery.CartDetailId);
            ViewBag.RiderCashParty = store.RefDbLayer.GetRiderCashParties();

            return View(delivery);
        }

        [HttpPost]
        public void AddPayment(int riderDetailId, int cartDetailId, int riderCashPartyId,  decimal amount)
        {
            var riderCashDetail = new RiderCashDetail
            {
                Amount = amount,
                CartDetailId = cartDetailId,
                DtCash = DateTime.Now,
                RiderCashPartyId = riderCashPartyId,
                RiderDetailId = riderDetailId
            };

            store.RiderMgr.AddCartPayment(riderCashDetail);

        }

        public void UpdateStatus(int id, int statusId)
        {
            store.RiderMgr.AddDeliveryActivity(id, statusId);

            //on item delivered
            if(statusId == 4)
            {
                var cartId = store.RiderMgr.GetCartDelivery(id).CartDetailId;
                store.RiderMgr.AddCartHistory(cartId, 5);
                store.RiderMgr.setCartStatusDelivered(cartId);
            }
        }

        public ActionResult NoUserRider()
        {
            return View();
        }
    }
}
