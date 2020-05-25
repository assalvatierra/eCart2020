using eCartModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCartInterfaces
{
    public interface iDBRefDBLayer
    {
        IQueryable<MasterCity> GetMasterCities();
        IQueryable<MasterArea> GetMasterAreas();
        IQueryable<StoreStatus> GetStoreStatus();
        IQueryable<ItemMaster> GetItemMaster();
        IQueryable<StoreDetail> GetStoreDetails();
        IQueryable<ItemCategory> GetItemCategories();
        IQueryable<ItemMasterCategory> GetItemMasterCategories();

    }
}
