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
using Microsoft.Ajax.Utilities;

namespace eCart.Areas.Admin.Controllers
{
    public class ItemMastersController : Controller
    {
        private StoreFactory store = new StoreFactory();

        // GET: Admin/ItemMasters
        public ActionResult Index()
        {
            return View(store.AdminMgr.GetItemMasterList()) ;
        }

        // GET: Admin/ItemMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemMaster itemMaster = store.AdminMgr.GetItemMaster((int)id);
            if (itemMaster == null)
            {
                return HttpNotFound();
            }

            return View(itemMaster);
        }

        // GET: Admin/ItemMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ItemMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] ItemMaster itemMaster, string ItemImageUrl)
        {
            if (ModelState.IsValid)
            {
               

                if (store.AdminMgr.AddItemMaster(itemMaster))
                {
                    if (!ItemImageUrl.IsNullOrWhiteSpace())
                    {
                        var newItemImage = new ItemImage()
                        {
                            ItemMasterId = itemMaster.Id,
                            ImageUrl = ItemImageUrl
                        };

                        store.AdminMgr.AddItemImage(newItemImage);
                    }
                    return RedirectToAction("Index");
                }
                   
            }

            return View(itemMaster);
        }

        // GET: Admin/ItemMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemMaster itemMaster = store.AdminMgr.GetItemMaster((int)id);
            if (itemMaster == null)
            {
                return HttpNotFound();
            }
            return View(itemMaster);
        }

        // POST: Admin/ItemMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] ItemMaster itemMaster, string ItemImageUrl)
        {
            if (ModelState.IsValid)
            {
                var itemImage = store.AdminMgr.GetItemImageByItemId(itemMaster.Id);
                if (itemImage == null)
                {
                    var itemImgNotNull = !ItemImageUrl.IsNullOrWhiteSpace();
                    if (itemImgNotNull)
                    {
                        var newItemImage = new ItemImage()
                        {
                            ItemMasterId = itemMaster.Id,
                            ImageUrl = ItemImageUrl
                        };

                        store.AdminMgr.AddItemImage(newItemImage);
                        itemImage = store.AdminMgr.GetItemImageByItemId(itemMaster.Id);
                    }
                }

                if (store.AdminMgr.EditItemImage(itemImage))
                    if (store.AdminMgr.EditItemMaster(itemMaster))
                        return RedirectToAction("Index");
            }
            return View(itemMaster);
        }

        // GET: Admin/ItemMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemMaster itemMaster = store.AdminMgr.GetItemMaster((int)id);
            if (itemMaster == null)
            {
                return HttpNotFound();
            }
            return View(itemMaster);
        }

        // POST: Admin/ItemMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemMaster itemMaster = store.AdminMgr.GetItemMaster((int)id);

            //check if item has img
            var itemImg = store.AdminMgr.GetItemImageByItemId(itemMaster.Id);
            if (itemImg != null)
                store.AdminMgr.RemoveItemImage(itemImg);

            //check if item categories
            var itemCat = store.AdminMgr.GetItemMasterCategoryList().Where(c=>c.ItemMasterId == id);
            foreach (var cat in itemCat)
            {
                RemoveItemCategory(cat.Id);
            }

            if (store.AdminMgr.RemoveItemMaster(itemMaster))
                return RedirectToAction("Index");
            return View(itemMaster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                store.AdminMgr.DbDispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public JsonResult GetItemCategoryGroups()
        {
            var ItemCatGroup = store.AdminMgr.GetItemCatGroupList().Select(c => new { c.Id , c.Name });

            return Json(ItemCatGroup, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetItemCategoriesById(int id)
        {
            var ItemCatGroup = store.AdminMgr.GetItemCategoriesList().Where(c=>c.ItemCatGroupId == id).Select(c => new { c.Id, c.Name });

            return Json(ItemCatGroup, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public bool AddItemCategory(int id, int categoryId)
        {
            var newItemCategory = new ItemMasterCategory(){
                ItemCategoryId = categoryId,
                ItemMasterId = id,
            };

            return store.AdminMgr.AddItemMasterCategory(newItemCategory);

        }


        [HttpPost]
        public bool RemoveItemCategory(int id)
        {
            var itemCategory = store.AdminMgr.GetItemMasterCategory(id);

            return store.AdminMgr.RemoveItemMasterCategory(itemCategory);

        }
    }
}
