using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using eCartDbLayer;
using eCartModels;
using eCartServices;

namespace eCart.Areas.Store.Controllers
{
    public class CartDetailsController : Controller
    {
        private StoreContext db = new StoreContext();
        StoreFactory storeFactory = new StoreFactory();
       
        // GET: Store/CartDetails/{cartId}
        public ActionResult Index(int id)
        {
            var storeMgr = storeFactory.StoreMgr;
            var cartDetails = db.CartDetails.Include(c => c.CartStatu).Include(c => c.StoreDetail).Include(c => c.StorePickupPoint).Include(c => c.UserDetail).Where(c=>c.Id == id);
            ViewBag.StoreId = id;
            ViewBag.StoreCarts = storeMgr.getStoreActiveCarts(id);
            return View(cartDetails.ToList());
        }

        // GET: Store/CartDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CartDetail cartDetail = db.CartDetails.Find(id);
            ViewBag.StoreId = cartDetail.StoreDetailId;
            ViewBag.Store = cartDetail.StoreDetail.Name;
            ViewBag.PaymentReceiverList = db.PaymentReceivers.ToList();
            ViewBag.PaymentPartyList = db.PaymentParties.ToList();
            ViewBag.PaymentStatusList = db.PaymentStatus.ToList();
            ViewBag.PaymentDetails = db.PaymentDetails.Where(s => s.CartDetailId == id).ToList();
            ViewBag.CartDelivery = db.CartDeliveries.Where(s => s.CartDetailId == id).ToList();
            ViewBag.RiderList = db.RiderDetails.Where(r=>r.RiderStatusId == 1).ToList();
           

            if (cartDetail == null)
            {
                return HttpNotFound();
            }
            return View(cartDetail);
        }

        // GET: Store/CartDetails/Create
        public ActionResult Create()
        {
            ViewBag.CartStatusId = new SelectList(db.CartStatus, "Id", "Name");
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId");
            ViewBag.StorePickupPointId = new SelectList(db.StorePickupPoints, "Id", "Address");
            ViewBag.UserDetailId = new SelectList(db.UserDetails, "Id", "UserId");
            return View();
        }

        // POST: Store/CartDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserDetailId,StoreDetailId,CartStatusId,StorePickupPointId")] CartDetail cartDetail)
        {
            if (ModelState.IsValid)
            {
                db.CartDetails.Add(cartDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CartStatusId = new SelectList(db.CartStatus, "Id", "Name", cartDetail.CartStatusId);
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId", cartDetail.StoreDetailId);
            ViewBag.StorePickupPointId = new SelectList(db.StorePickupPoints, "Id", "Address", cartDetail.StorePickupPointId);
            ViewBag.UserDetailId = new SelectList(db.UserDetails, "Id", "UserId", cartDetail.UserDetailId);
            return View(cartDetail);
        }

        // GET: Store/CartDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartDetail cartDetail = db.CartDetails.Find(id);
            if (cartDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.CartStatusId = new SelectList(db.CartStatus, "Id", "Name", cartDetail.CartStatusId);
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId", cartDetail.StoreDetailId);
            ViewBag.StorePickupPointId = new SelectList(db.StorePickupPoints, "Id", "Address", cartDetail.StorePickupPointId);
            ViewBag.UserDetailId = new SelectList(db.UserDetails, "Id", "UserId", cartDetail.UserDetailId);
            return View(cartDetail);
        }

        // POST: Store/CartDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserDetailId,StoreDetailId,CartStatusId,StorePickupPointId")] CartDetail cartDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CartStatusId = new SelectList(db.CartStatus, "Id", "Name", cartDetail.CartStatusId);
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId", cartDetail.StoreDetailId);
            ViewBag.StorePickupPointId = new SelectList(db.StorePickupPoints, "Id", "Address", cartDetail.StorePickupPointId);
            ViewBag.UserDetailId = new SelectList(db.UserDetails, "Id", "UserId", cartDetail.UserDetailId);
            return View(cartDetail);
        }

        // GET: Store/CartDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartDetail cartDetail = db.CartDetails.Find(id);
            if (cartDetail == null)
            {
                return HttpNotFound();
            }
            return View(cartDetail);
        }

        // POST: Store/CartDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CartDetail cartDetail = db.CartDetails.Find(id);
            db.CartDetails.Remove(cartDetail);
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
        public string AddPayment(string date, int partyId, string partyInfo, int receiverId, string receiverInfo, int statusId, decimal amount, int cartDetailId)
        {
            try
            {
                var storeMgr = storeFactory.StoreMgr;
                storeMgr.addPaymentDetails(date, partyId, partyInfo, receiverId, receiverInfo, statusId, amount, cartDetailId);

                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        [HttpGet]
        public JsonResult SetCartStatus(int id, int statusId)
        {
            var cartMgr = storeFactory.CartMgr;
            var userid = cartMgr.getUserId();
            var response = cartMgr.setDBCartStatus(id,statusId, userid.ToString()); 

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void AddDeliveryRider(int cartDetailId, DateTime date, string address, int riderId, string remarks)
        {
            var cartMgr = storeFactory.CartMgr;
            cartMgr.addDeliveryDetails(cartDetailId, date, address, riderId, remarks);
        }

        [HttpPost]
        public void UpdateDeliveryRider(int id, int cartDetailId, DateTime date, string address, int riderId, string remarks)
        {
            var cartMgr = storeFactory.CartMgr;

            var cartDelivery = new CartDelivery
            {
                Id = id,
                Address = address,
                CartDetailId = cartDetailId,
                RiderDetailId = riderId,
                Remarks = remarks,
                dtDelivery = date
            };

            cartMgr.updateCartDelivery(cartDelivery);
        }

        public void DeleteCartDelivery(int id)
        {
            var cartMgr = storeFactory.CartMgr;
            cartMgr.removeCartdelivery(id);
        }

        #region CartHistory
        public ActionResult CartHistory(int? id)
        {
            if (id != null)
            {
                var cartMgr = storeFactory.CartMgr;
                var cartHistory = cartMgr.getCartHistory((int)id);
                ViewBag.StoreId = id;
                return View(cartHistory);
            }

            return RedirectToAction("Index", new { id = id });
        }

        [HttpGet]
        public JsonResult GetCartDeliveryActivities(int id)
        {
            var cartMgr = storeFactory.CartMgr;
            var cartDeliveryActivity = cartMgr.getCartDeliveryActivities(id).Select(c => new { Date = c.dtActivity.ToString(), Activity = c.CartActivityType.Name });

            return Json(cartDeliveryActivity, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
