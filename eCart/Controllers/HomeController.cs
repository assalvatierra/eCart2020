using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCartServices;
using eCartModels;
using Microsoft.AspNet.Identity;

namespace eCart.Controllers
{
    public class HomeController : Controller
    {
        StoreFactory store = new StoreFactory();

        public ActionResult Index()
        {
            var userId = HttpContext.User.Identity.GetUserId();

            if (userId != null)
            {
                //get ready to pickup carts
                var userDetailId = store.CartMgr.GetUserDetails(userId).Id;
                var readyCarts = store.CartMgr.GetReadyShopperCarts(userDetailId);
                ViewBag.ReadyCarts = readyCarts;
            }
            else
            {
                ViewBag.ReadyCarts = null;
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AboutHow()
        {
            ViewBag.Message = "How It Works Page.";

            return View();
        }
        // GET: Store
        public ActionResult FeaturedStores()
        {
            var storeList = store.StoreMgr.getFeaturedStores();
            return View(storeList);
        }

        // PartialView for Store List View
        public PartialViewResult _StoreList()
        {
            // var featuredStores = storeMgr.getFeaturedStores().Take(3);
             var featuredStores = store.StoreMgr.getFeaturedStores().Take(3); //using the factory
            return PartialView(featuredStores);
        }

        // PartialView for Store List View
        public PartialViewResult _ProductList()
        {
            //var featuredItems = storeMgr.getFeaturedItems().Take(12);
            var featuredItems = store.StoreMgr.getFeaturedItems().Take(12); //using the factory
            return PartialView(featuredItems);
        }

        public ViewResult Store(int? id)
        {
            if (id != null)
            {
                var storeMgr = store.StoreMgr;

                int storeId = (int)id;
                var storeItems = storeMgr.getStoreItems(storeId);
                var storeDetails = storeMgr.getStoreDetails(storeId);
                var defaultImg = "/img/placeholders/placeholder-product.png";

                ViewBag.StoreName = storeDetails.Name;
                ViewBag.StoreAddress = storeDetails.Address;
                ViewBag.StoreImg = storeDetails.StoreImages.FirstOrDefault() != null ? storeDetails.StoreImages.FirstOrDefault().ImageUrl : defaultImg;

                //Get next suggested Store
                StoreDetail storeDetail = new StoreDetail();
                do
                {
                    storeDetail = storeMgr.getRandomStore();
                } while (storeDetail.Id == id);

                ViewBag.nextStoreId = storeDetail.Id;
                ViewBag.nextStore = storeDetail.Name;
                ViewBag.nextStoreImg = storeDetail.StoreImages.FirstOrDefault().ImageUrl;
                return View(storeItems);
            }
            else
            {
                return View("Index", "Error");
            }
        }
    }
}