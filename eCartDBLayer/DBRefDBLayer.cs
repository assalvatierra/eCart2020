﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using eCartModels;
using eCartInterfaces;


namespace eCartDbLayer
{
    public class DBRefDBLayer: iDBRefDBLayer
    {
        DBRefContext db = new DBRefContext();

        public IQueryable<MasterCity> GetMasterCities()
        {
            return db.MasterCities;
        }
        public IQueryable<MasterArea> GetMasterAreas()
        {
            return db.MasterAreas;
        }

        public IQueryable<StoreStatus> GetStoreStatus()
        {
            return db.StoreStatuses;
        }

        public IQueryable<ItemMaster> GetItemMaster()
        {
            return db.ItemMasters;
        }

        public IQueryable<StoreDetail> GetStoreDetails()
        {
            return db.StoreDetails;
        }

        public IQueryable<ItemCategory> GetItemCategories()
        {
            return db.ItemCategories;
        }

        public IQueryable<ItemMasterCategory> GetItemMasterCategories()
        {
            try
            {
                return db.ItemMasterCategories;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
