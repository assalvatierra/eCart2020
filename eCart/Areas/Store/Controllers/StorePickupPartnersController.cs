using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eCartDbLayer;
using eCartModels;

namespace eCart.Areas.Store.Controllers
{
    public class StorePickupPartnersController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Store/StorePickupPartners/{storeId}
        public ActionResult Index(int id)
        {
            var partneredList = db.StorePickupPartners.Where(p => p.StoreDetailId == id).Select(s => s.StorePickupPointId).ToList();
            var pickupPointsList = db.StorePickupPoints.Include(s => s.StoreDetail).Where(s=>!partneredList.Contains(s.Id)).ToList().OrderBy(s => s.StoreDetailId);


            ViewBag.StoreId = id;
            ViewBag.PickupPoints = pickupPointsList;
            var storePickupPartners = db.StorePickupPartners.Where(s=>s.StoreDetailId==id).Include(s => s.StoreDetail).Include(s => s.StorePickupPoint);
            return View(storePickupPartners.ToList());
        }

        // GET: Store/StorePickupPartners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorePickupPartner storePickupPartner = db.StorePickupPartners.Find(id);
            if (storePickupPartner == null)
            {
                return HttpNotFound();
            }
            return View(storePickupPartner);
        }

        // GET: Store/StorePickupPartners/Create
        public ActionResult Create()
        {
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId");
            ViewBag.StorePickupPointId = new SelectList(db.StorePickupPoints, "Id", "Address");
            return View();
        }

        // POST: Store/StorePickupPartners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StorePickupPointId,StoreDetailId")] StorePickupPartner storePickupPartner)
        {
            if (ModelState.IsValid)
            {
                db.StorePickupPartners.Add(storePickupPartner);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = storePickupPartner.StoreDetailId });
            }

            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId", storePickupPartner.StoreDetailId);
            ViewBag.StorePickupPointId = new SelectList(db.StorePickupPoints, "Id", "Address", storePickupPartner.StorePickupPointId);
            return View(storePickupPartner);
        }

        // GET: Store/StorePickupPartners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorePickupPartner storePickupPartner = db.StorePickupPartners.Find(id);
            if (storePickupPartner == null)
            {
                return HttpNotFound();
            }
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId", storePickupPartner.StoreDetailId);
            ViewBag.StorePickupPointId = new SelectList(db.StorePickupPoints, "Id", "Address", storePickupPartner.StorePickupPointId);
            return View(storePickupPartner);
        }

        // POST: Store/StorePickupPartners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StorePickupPointId,StoreDetailId")] StorePickupPartner storePickupPartner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(storePickupPartner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = storePickupPartner.StoreDetailId });
            }
            ViewBag.StoreDetailId = new SelectList(db.StoreDetails, "Id", "LoginId", storePickupPartner.StoreDetailId);
            ViewBag.StorePickupPointId = new SelectList(db.StorePickupPoints, "Id", "Address", storePickupPartner.StorePickupPointId);
            return View(storePickupPartner);
        }

        // GET: Store/StorePickupPartners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorePickupPartner storePickupPartner = db.StorePickupPartners.Find(id);
            if (storePickupPartner == null)
            {
                return HttpNotFound();
            }
            return View(storePickupPartner);
        }

        // POST: Store/StorePickupPartners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StorePickupPartner storePickupPartner = db.StorePickupPartners.Find(id);
            var storeId = storePickupPartner.StoreDetailId;
            db.StorePickupPartners.Remove(storePickupPartner);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = storeId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public JsonResult AddPartner(int id, int storeId)
        {
            try
            {
                StorePickupPartner storePartner = new StorePickupPartner
                {
                    StoreDetailId = storeId,
                    StorePickupPointId = id
                };

                db.StorePickupPartners.Add(storePartner);
                //db.SaveChanges();

                var store = db.StoreItems.Find(storeId);


                //create obj for json
                var partnerStore = db.StorePickupPoints.Find(id);
                var data = new jsonStorePartner
                {
                    Id = id,
                    StoreName = partnerStore.StoreDetail.Name,
                    PickupAddress = partnerStore.Address
                };

                return Json(data, JsonRequestBehavior.AllowGet);
            } catch (Exception ex) {
                return Json(ex.Message.ToString(), JsonRequestBehavior.AllowGet); ;
            }
        }
    }
}


#region helperClass
public class jsonStorePartner{
        public int Id { get; set; }
        public string StoreName { get; set; }
        public string PickupAddress { get; set; }
    }

#endregion