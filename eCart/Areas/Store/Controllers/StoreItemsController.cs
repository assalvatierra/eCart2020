using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using eCartModels;
using eCartServices;

namespace eCart.Areas.Store.Controllers
{
    public class StoreItemsController : Controller
    {
        private StoreFactory storeFactory = new StoreFactory();

        // GET: Store/StoreItems
        public ActionResult Index(int? id)
        {
            if (id != null)
            {
                var storeItems = storeFactory.StoreMgr.getStoreItems((int)id);
                ViewBag.StoreId = id;

                return View(storeItems.ToList());
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: Store/StoreItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreItem storeItem = storeFactory.StoreMgr.getStoreItem((int)id);
            if (storeItem == null)
            {
                return HttpNotFound();
            }
            return View(storeItem);
        }

        // GET: Store/StoreItems/Create
        public ActionResult Create(int id)
        {
            StoreItem storeItem = new StoreItem() {
                StoreDetailId = id,
                UnitPrice = 0
            };

            ViewBag.ItemMasterId = new SelectList(storeFactory.RefDbLayer.GetItemMaster().ToList(), "Id", "Name");
            ViewBag.StoreDetailId = new SelectList(new List<SelectListItem>
            { new SelectListItem{ Text = id.ToString(), Value = "test" } }, 
            "Id", "LoginId", id);

            return View(storeItem);
        }

        // POST: Store/StoreItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ItemMasterId,StoreDetailId,UnitPrice")] StoreItem storeItem)
        {
            if (ModelState.IsValid)
            {
                //db.StoreItems.Add(storeItem);
                //db.SaveChanges();

                if (storeFactory.StoreMgr.AddStoreItem(storeItem))
                {
                    return RedirectToAction("Index", new { id = storeItem.StoreDetailId });
                }

                ModelState.AddModelError("", "Unable to add new item.");
            }

            ViewBag.ItemMasterId = new SelectList(storeFactory.RefDbLayer.GetItemMaster().ToList(), "Id", "Name", storeItem.ItemMasterId);
            ViewBag.StoreDetailId = new SelectList(new List<SelectListItem>
            { new SelectListItem{ Text = "1", Value = "test" } },
             "Id", "LoginId");

            return View(storeItem);
        }


        
        // GET: Store/StoreItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreItem storeItem = storeFactory.StoreMgr.getStoreItem((int)id);
            if (storeItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemMasterId = new SelectList(storeFactory.RefDbLayer.GetItemMaster(), "Id", "Name", storeItem.ItemMasterId);
            ViewBag.StoreDetailId = new SelectList(new List<SelectListItem>
            { new SelectListItem{ Text = id.ToString(), Value = "test" } },
            "Id", "LoginId", id);

            return View(storeItem);
        }

        // POST: Store/StoreItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ItemMasterId,StoreDetailId,UnitPrice")] StoreItem storeItem)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(storeItem).State = System.Data.Entity.EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index", new { id = storeItem.StoreDetailId });
            }
            ViewBag.ItemMasterId = new SelectList(storeFactory.RefDbLayer.GetItemMaster(), "Id", "Name", storeItem.ItemMasterId);
            ViewBag.StoreDetailId = new SelectList(storeFactory.RefDbLayer.GetStoreDetails() , "Id", "LoginId", storeItem.StoreDetailId);
            return View(storeItem);
        }

        // GET: Store/StoreItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreItem storeItem = storeFactory.StoreMgr.getStoreItem((int)id);
            if (storeItem == null)
            {
                return HttpNotFound();
            }
            return View(storeItem);
        }

        // POST: Store/StoreItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StoreItem storeItem = storeFactory.StoreMgr.getStoreItem((int)id);
            //db.StoreItems.Remove(storeItem);
            //db.SaveChanges();
            return RedirectToAction("Index", new { id = storeItem.StoreDetailId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }


        public PartialViewResult _ModalCreateItem()
        {
            StoreItem item = new StoreItem();
            ViewBag.ItemMasterId = new SelectList(storeFactory.RefDbLayer.GetItemMaster(), "Id", "Name");
            ViewBag.StoreDetailId = new SelectList(storeFactory.RefDbLayer.GetStoreDetails(), "Id", "LoginId");

            return PartialView(item);
        }


        [HttpPost]
        public bool AddStoreItem(int storeId, string itemName, decimal price, string imgUrl)
        {
            try
            {
                var storeMgr = storeFactory.StoreMgr;
                storeMgr.AddNewStoreItem(storeId, itemName, price, imgUrl);
                return true;
            }
            catch
            {
                return false;
            }
        }


        [HttpPost]
        public bool AddItemToStore(int storeId, int itemMasterId, decimal price)
        {
            try
            {
                var storeMgr = storeFactory.StoreMgr;
                storeMgr.AddStoreItem(storeId, itemMasterId, price);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpGet]
        public JsonResult GetStoreItem(int id)
        {
            var storeMgr = storeFactory.StoreMgr;
            var item = storeMgr.getStoreItem(id);

            var jsonItem = new jsonStoreItem
            {
                Id = id,
                ItemName = item.ItemMaster.Name,
                UnitPrice = item.UnitPrice,
                ImageUrl = item.ItemMaster.ItemImages.FirstOrDefault().ImageUrl
            };

            return Json(jsonItem, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetItemMasters(int id)
        {
            var storeMgr = storeFactory.StoreMgr;
            var itemList = storeFactory.RefDbLayer.GetItemMasterCategories().Where(s=>s.ItemCategoryId == id).ToList().Select(s => new { s.ItemMasterId, s.ItemMaster.Name });

            return Json(itemList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetItemMasterDetails(int id)
        {
            var storeMgr = storeFactory.StoreMgr;
            var item = storeMgr.GetItemMaster(id);


            var jsonItem = new jsonStoreItem
            {
                Id = id,
                ItemName = item.Name,
                ImageUrl = item.ItemImages.FirstOrDefault().ImageUrl
            };

            return Json(item, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public bool EditStoreItem(int storeItemId, string itemName, decimal price, string imageUrl)
        {
            try
            {
                var storeMgr = storeFactory.StoreMgr;
                if(storeMgr.EditStoreItem(storeItemId, itemName, price))
                {
                    if (storeMgr.EditStoreItemImage(storeItemId, imageUrl))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch 
            {
                return false;
            }
        }

        [HttpGet]
        public JsonResult GetItemCategoryGroups()
        {
            var categoryGroups = storeFactory.StoreMgr.GetItemCatGroups().Select(c=> new { c.Id, c.Name });
            return Json(categoryGroups, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetItemCategoriesById(int id)
        {
            var categories = storeFactory.StoreMgr.GetItemCategories(id).Select(c => new { c.Id, c.Name });
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetItemCategories()
        {
            var categories = storeFactory.RefDbLayer.GetItemCategories().Select(c => new { c.Id, c.Name }).ToList();
            return Json(categories, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public bool RemoveStoreItem(int Id)
        {
            try
            {
                var storeMgr = storeFactory.StoreMgr;
                if (storeMgr.RemoveStoreItem(Id))
                {
                    return true;
                }
                return false;
            }
            catch 
            {
                return false;
            }

        }

    }
}
