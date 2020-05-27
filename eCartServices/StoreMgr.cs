using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using eCartInterfaces;
using eCartModels;
using eCartDbLayer;

namespace eCartServices
{
    public class StoreMgr : iStoreMgr
    {

        private ecartdbContainer db = new ecartdbContainer();
        private iStoreDb storeDb = new StoreDBLayer(); 

        public void setDbLayer(iStoreDb storedblayer)
        {
            this.storeDb = storedblayer;
        }

        #region For Revision

        public List<StoreDetail> getFeaturedStores()
        {
            try
            {
                //initial: take latest added stores
                var stores = db.StoreDetails.OrderByDescending(s => s.Id).Take(10);
                
                return stores.ToList();
            }
            catch (Exception ex)
            { 
                throw ex;
            }
        }

        public List<StoreItem> getFeaturedItems()
        {
            try
            {
                //take latest added items
                var items = db.StoreItems.OrderByDescending(s => s.Id).Take(12);

                return items.ToList();
            }
            catch
            {
                throw new NotImplementedException();
            }

        }

        public List<StoreItem> getStoreItems(int id)
        {
            try
            {
                //take latest all store items
                var items = db.StoreItems.Where(i => i.StoreDetailId == id);
//                .Include(s => s.ItemMaster).Include(s => s.StoreDetail).OrderByDescending(s => s.Id);
  
                items = items.OrderByDescending(s => s.Id).Take(18);

                return items.ToList();
            }
            catch
            {
                throw new NotImplementedException();
            }

        }

        public StoreDetail getStoreDetails(int id)
        {
            try
            {
                //take latest all store items
                var storeDetail = db.StoreDetails.Find(id);

                return storeDetail;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        public bool AddNewStoreItem(int storeId, string itemName, decimal price, string imgUrl)
        {
            try
            {
                //add item to item master
                ItemMaster item = new ItemMaster()
                {
                    Name = itemName,
                };


                //add item image
                ItemImage itemImage = new ItemImage() {
                    ItemMaster = item,
                    ImageUrl = imgUrl
                };

                StoreItem storeItem = new StoreItem()
                {
                    ItemMaster = item,
                    StoreDetailId = storeId,
                    UnitPrice = price
                };


                if (storeDb.AddItemMaster(item))
                {
                    if (storeDb.AddItemImage(itemImage))
                    {
                        if (storeDb.AddStoreItem(storeItem))
                        {
                            if (storeDb.SaveChanges())
                            {
                                return true;
                            }
                        }
                    }
                }

                return false;
            }
            catch 
            { 
                return false; 
            }
            
        }

        public StoreItem getStoreItem(int id)
        {
            try
            {
                return db.StoreItems.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public StorePickupPoint getDefaultPickupPoint(int StoreId)
        {
            try
            {
                return db.StoreDetails.Find(StoreId).StorePickupPoints.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string addPaymentDetails(string date, int partyId, string partyInfo, int receiverId, string receiverInfo, int statusId, decimal amount, int cartDetailId)
        {
            try
            {

                PaymentDetail payment = new PaymentDetail()
                {
                    Amount = amount,
                    CartDetailId = cartDetailId,
                    dtPayment = DateTime.Now,
                    PaymentPartyId = partyId,
                    PartyInfo = partyInfo,
                    PaymentReceiverId = receiverId,
                    ReceiverInfo = receiverInfo,
                    PaymentStatusId = statusId
                };

                db.PaymentDetails.Add(payment);
                db.SaveChanges();

                return "Payment Added";
            }
            catch (Exception ex)
            {
                return ex.Message.ToLower();
            }   
        }

        public StoreDetail getRandomStore()
        {
            try
            {
                var random = new Random();
                var storeListCount = db.StoreDetails.ToList().Count();

                var selectedID = random.Next(1, storeListCount);
                var store = db.StoreDetails.Find(selectedID);

                return store;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void addCartDeliveryActivity(int cartId, int statusId)
        {
            
        }

        public List<CartDetail> getStoreActiveCarts(int id)
        {
            var storeCarts = db.CartDetails.Where(s => s.StoreDetailId == id && s.CartStatusId <= 5); //active

            return storeCarts.ToList();
        }

        #endregion

        #region Store Registration

        public bool CreateStore(StoreDetail storeDetail)
        {
            return storeDb.CreateStoreDetail(storeDetail);
        }

        public bool RegisterStore(StoreRegistration newStore)
        {
            try
            {
                StoreDetail store = new StoreDetail();
                store.Name = newStore.Name;
                store.Address = newStore.Address;
                store.LoginId = newStore.LoginId;
                store.MasterAreaId = newStore.MasterAreaId;
                store.MasterCityId = newStore.MasterCityId;
                store.StoreCategoryId = newStore.StoreCategoryId;
                store.Remarks = newStore.Remarks ?? " ";
                store.StoreStatusId = newStore.StoreStatusId;

                return storeDb.CreateStoreDetail(store);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EditStore(StoreDetail storeDetail)
        {
            return storeDb.EditStoreDetail(storeDetail);
        }

        public bool EditStoreImg(int storeId, string imgUrl, int imgTypeId)
        {
            try
            {
                //get store img ref
                var storeImg = storeDb.GetStoreImages().Where(s => s.StoreDetailId == storeId && s.StoreImgTypeId == imgTypeId).FirstOrDefault();

                storeImg.ImageUrl = imgUrl;

                storeDb.EditStoreImage(storeImg);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public StoreDetail GetStoreDetailByLoginId(string loginId)
        {
            if (!string.IsNullOrWhiteSpace(loginId))
            {
                return storeDb.GetStoreByUserId(loginId);
            }
            return null;
        }

        public StoreImage GetStoreImg(int storeId)
        {
            int IMAGEFRONT = 1;

            if (storeDb.GetStoreImages().Any(s => s.StoreDetailId == storeId))
            {
                return storeDb.GetStoreImages().Where(s => s.StoreDetailId == storeId && s.StoreImgTypeId == IMAGEFRONT).FirstOrDefault();
            }

            //if no images found, return placeholder
            return new StoreImage() { 
                Id = 0,
                StoreDetailId = storeId,
                StoreImgTypeId = 1,
                ImageUrl = "/img/placeholders/placeholder-product.png"
            };
            
        }

        public bool IsStoreImgExist(int storeId)
        {
            return storeDb.GetStoreImages().Any(s => s.StoreDetailId == storeId);
        }

        public bool CreatStoreImg(int storeId, string imgUrl, int ImgTypeId)
        {
            StoreImage storeImage = new StoreImage()
            {
                ImageUrl = imgUrl,
                StoreImgTypeId = ImgTypeId,
                StoreDetailId = storeId,
            };

            storeDb.AddStoreImage(storeImage);

            return true;
        }

        public bool ValidateStoreImg(int storeId, string ImgUrl)
        {

            if (!string.IsNullOrWhiteSpace(ImgUrl))
            {
                //check if there is existing storeImg
                if (IsStoreImgExist(storeId))
                {
                    //update store Img
                    EditStoreImg(storeId, ImgUrl, 1);
                }
                else
                {
                    //create storeImg
                    CreatStoreImg(storeId, ImgUrl, 1);
                }
            }
            else
            {
                if (IsStoreImgExist(storeId))
                {
                    //when imgeUrl is empty and Previous img exists throw error
                    return false;
                }

            }

            return true;
        }



        #endregion

        #region Store Items
        public bool AddStoreItem(StoreItem storeItem)
        {
            try
            {
                if (storeItem != null)
                {
                    if (storeDb.AddStoreItem(storeItem))
                    {
                        return storeDb.SaveChanges();
                    }
                    return false;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool AddStoreItem(int storeID, int itemId, decimal price)
        {

            try
            {
                var storeItem = new StoreItem
                {
                    ItemMasterId = itemId,
                    StoreDetailId = storeID,
                    UnitPrice = price
                };

                if (storeDb.AddStoreItem(storeItem))
                {
                    return storeDb.SaveChanges();
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public ItemMaster GetItemMaster(int id)
        {
            try
            {
                return storeDb.GetItemMaster(id);
            }
            catch
            {
                return null;
            }
        }

        public List<ItemCatGroup> GetItemCatGroups()
        {
            try
            {
                var itemCatGroup = storeDb.GetItemCatGroups().ToList();

                return itemCatGroup;
            }
            catch
            {
                return null;
            }
        }

        public List<ItemCategory> GetItemCategories(int itemCatGroupId)
        {
            try
            {
                var itemCategories = storeDb.GetItemCategories().Where(c=>c.Id == itemCatGroupId).ToList();
               
                return itemCategories;
            }
            catch
            {
                return null;
            }
        }

        public bool EditStoreItem(int storeItemId, string itemName, decimal price)
        {

            try
            {
                var storeItem = storeDb.GetStoreItem(storeItemId);
                storeItem.UnitPrice = price;
                storeItem.ItemMaster.Name = itemName;

                return storeDb.EditStoreItem(storeItem);
            }
            catch 
            {
                return false;
            }
        }

        public bool EditStoreItemImage(int storeItemId, string imageUrl)
        {
            try
            {
                var storeItem = storeDb.GetStoreItem(storeItemId);
                var item = storeDb.GetItemMaster(storeItem.ItemMasterId);
                var itemImg = storeDb.GetItemImage(item.Id);
                itemImg.ImageUrl = imageUrl;

                return storeDb.EditStoreItemImg(itemImg);
            }
            catch 
            {
                return false;
            }
        }

        public bool RemoveStoreItem(int id)
        {
            try
            {
                var storeItem = storeDb.GetStoreItem(id);
                if(storeItem != null)
                {
                    return storeDb.RemoveStoreItem(storeItem);
                }
                return false;
            }
            catch
            {
                return false;
            }
        }


        #endregion

        #region Store Kiosk
        public List<StoreKiosk> GetStoreKiosks(int storeId)
        {
            var kiosks = db.StoreKiosks.Where(s => s.StoreDetailId == storeId);

            return kiosks.ToList();
        }


        #endregion
    }
}