using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using eCartDbLayer;
using eCartModels;
using eCartServices;
using Microsoft.AspNet.Identity;

namespace eCart.Areas.Store.Controllers
{
    public class CartDetailsController : Controller
    {
        StoreFactory store = new StoreFactory();
       
        // GET: Store/CartDetails/{cartId}
        //public ActionResult Index(int id)
        //{
        //    return View();
        //}

        // GET: Store/CartDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userId = HttpContext.User.Identity.GetUserId();
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CartDetail cartDetail = store.CartMgr.GetCartDetail((int)id);

            ViewBag.StoreId = cartDetail.StoreDetailId;
            ViewBag.Store = cartDetail.StoreDetail.Name;
            ViewBag.PaymentReceiverList = store.RefDbLayer.GetPaymentReceivers().ToList();
            ViewBag.PaymentStatusList = store.RefDbLayer.GetPaymentStatus().ToList();
            ViewBag.PaymentPartyList = store.RefDbLayer.GetPaymentParties().ToList();
            ViewBag.PaymentDetails = store.CartMgr.GetCartPaymentDetails((int)id);
            ViewBag.CartDelivery = store.CartMgr.GetCartDeliveries((int)id);
            ViewBag.RiderList = store.RiderMgr.GetActiveRiders();

            //cart Realease
            ViewBag.StorePickupPoints = new SelectList(store.CartMgr.GetStorePickupPoints(cartDetail.StoreDetailId), "Id", "Address", cartDetail.StorePickupPointId);
            ViewBag.UserDetails = new SelectList(store.RefDbLayer.GetUserDetails().Where(u => u.UserStatusId == 1), "Id", "Name", cartDetail.UserDetailId);
            ViewBag.UserId = userId;
            ViewBag.UserEmail = HttpContext.User.Identity.GetUserName();

            if (cartDetail == null)
            {
                return HttpNotFound();
            }
            return View(cartDetail);
        }


        // GET: Store/CartDetails/Create
        public ActionResult Create()
        {
            //ViewBag.CartStatusId = new SelectList(db.CartStatus, "Id", "Name");
            //ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId");
            //ViewBag.StorePickupPointId = new SelectList(db.StorePickupPoints, "Id", "Address");
            //ViewBag.UserDetailId = new SelectList(db.UserDetails, "Id", "UserId");
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
                //db.CartDetails.Add(cartDetail);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.CartStatusId = new SelectList(db.CartStatus, "Id", "Name", cartDetail.CartStatusId);
            //ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId", cartDetail.StoreDetailId);
            //ViewBag.StorePickupPointId = new SelectList(db.StorePickupPoints, "Id", "Address", cartDetail.StorePickupPointId);
            //ViewBag.UserDetailId = new SelectList(db.UserDetails, "Id", "UserId", cartDetail.UserDetailId);
            return View(cartDetail);
        }

        // GET: Store/CartDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //CartDetail cartDetail = db.CartDetails.Find(id);
            //if (cartDetail == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.CartStatusId = new SelectList(db.CartStatus, "Id", "Name", cartDetail.CartStatusId);
            //ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId", cartDetail.StoreDetailId);
            //ViewBag.StorePickupPointId = new SelectList(db.StorePickupPoints, "Id", "Address", cartDetail.StorePickupPointId);
            //ViewBag.UserDetailId = new SelectList(db.UserDetails, "Id", "UserId", cartDetail.UserDetailId);
            return View();
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
                //db.Entry(cartDetail).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.CartStatusId = new SelectList(db.CartStatus, "Id", "Name", cartDetail.CartStatusId);
            //ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId", cartDetail.StoreDetailId);
            //ViewBag.StorePickupPointId = new SelectList(db.StorePickupPoints, "Id", "Address", cartDetail.StorePickupPointId);
            //ViewBag.UserDetailId = new SelectList(db.UserDetails, "Id", "UserId", cartDetail.UserDetailId);
            return View();
        }

        // GET: Store/CartDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //CartDetail cartDetail = db.CartDetails.Find(id);
            //if (cartDetail == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(cartDetail);

            return View();
        }

        // POST: Store/CartDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //CartDetail cartDetail = db.CartDetails.Find(id);
            //db.CartDetails.Remove(cartDetail);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public bool AddPayment(string date, int partyId, string partyInfo, int receiverId, string receiverInfo, int statusId, decimal amount, int cartDetailId)
        {
            try
            {
                var storeMgr = store.StoreMgr;
                storeMgr.addPaymentDetails(date, partyId, partyInfo, receiverId, receiverInfo, statusId, amount, cartDetailId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpGet]
        public JsonResult SetCartStatus(int id, int statusId)
        {
            var cartMgr = store.CartMgr;
            var userid =  HttpContext.User.Identity.GetUserId();
            var response = cartMgr.SetDBCartStatus(id,statusId, userid.ToString()); 

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public bool AddDeliveryRider(int cartDetailId, DateTime date, string address, int riderId, string remarks)
        {
            try
            {
                var cartMgr = store.CartMgr;
                cartMgr.AddDeliveryDetails(cartDetailId, date, address, riderId, remarks);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost]
        public void UpdateDeliveryRider(int id, int cartDetailId, DateTime date, string address, int riderId, string remarks)
        {
            var cartMgr = store.CartMgr;

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
            var cartMgr = store.CartMgr;
            cartMgr.removeCartdelivery(id);
        }

        #region CartHistory
        public ActionResult CartHistory(int? id)
        {
            if (id != null)
            {
                var cartMgr = store.CartMgr;
                var cartHistory = cartMgr.getCartHistory((int)id);
                ViewBag.StoreId = id;
                return View(cartHistory);
            }

            return RedirectToAction("Index", new { id = id });
        }

        [HttpGet]
        public JsonResult GetCartDeliveryActivities(int id)
        {
            var cartMgr = store.CartMgr;
            var cartDeliveryActivity = cartMgr.getCartDeliveryActivities(id).Select(c => new { Date = c.dtActivity.ToString(), Activity = c.CartActivityType.Name });

            return Json(cartDeliveryActivity, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region cart release
        [HttpPost]
        public int AddCartRelease(string date, string releaseBy, string userId, int cartDetailId, int userDetailId, int storePickupPointId)
        {
            try
            {
                var cartRelease = new CartRelease()
                {
                    DtRelease = DateTime.Parse(date),
                    ReleaseBy = releaseBy,
                    LoginId = userId,
                    CartDetailId = cartDetailId,
                    UserDetailId = userDetailId,
                    StorePickupPointId = storePickupPointId
                };

                if (store.CartMgr.AddCartRelease(cartRelease))
                {
                    return cartRelease.Id;
                }
                else
                {
                    //error
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        public ActionResult CartRelease(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cartRelease = store.CartMgr.GetCartRelease((int)id);

            return View(cartRelease);
        }
        #endregion

    }

}
