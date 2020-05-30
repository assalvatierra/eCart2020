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
using eCartServices;

namespace eCart.Areas.Store.Controllers
{
    public class StorePickupPartnersController : Controller
    {
        private StoreFactory store = new StoreFactory();

        // GET: Store/StorePickupPartners/{storeId}
        public ActionResult Index(int id)
        {
            var partneredList = store.StoreMgr.GetStorePickupPartners(id).Select(s => s.StorePickupPointId).ToList();
            var StorePickupPoints = store.StoreMgr.GetStorePickupPoints(id);
            var pickupPointsList = store.StoreMgr.GetStorePickupPoints().Where(s=>s.StorePickupStatusId == 1).ToList();


            ViewBag.StoreId = id;
            ViewBag.PickupPoints = pickupPointsList.Except(StorePickupPoints).Where(s => !partneredList.Contains(s.Id)).OrderBy(s => s.StoreDetailId);

            var storePickupPartners = store.StoreMgr.GetStorePickupPartners(id);
            return View(storePickupPartners.ToList());
        }

        [HttpGet]
        public bool AddPartner(int id, int storeId)
        {
            try
            {
                StorePickupPartner storePartner = new StorePickupPartner
                {
                    StoreDetailId = storeId,
                    StorePickupPointId = id
                };

                return store.StoreMgr.AddStorePickupPartner(storePartner);

            } catch {
                return false;
            }
        }

        [HttpPost]
        public bool RemovePartner(int id, int storeId)
        {
            try
            {
                var storePartner = store.StoreMgr.GetStorePickupPartners(storeId).Find(s=>s.Id == id);

                return store.StoreMgr.RemoveStorePickupPartner(storePartner);
            }
            catch
            {
                return false;
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