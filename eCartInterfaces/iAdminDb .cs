using eCartModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCartInterfaces
{
    public interface iAdminDb
    {
        bool DbDispose();

        IQueryable<StoreDetail> GetStoreDetails();
        bool AddStoreDetails(StoreDetail storeDetail);
        bool EditStoreDetails(StoreDetail storeDetail);
        bool RemoveStoreDetails(StoreDetail storeDetail);

        IQueryable<UserDetail> GetUserDetails();
        bool AddUserDetails(UserDetail userDetail);
        bool EditUserDetails(UserDetail userDetail);
        bool RemoveUserDetails(UserDetail userDetail);

        IQueryable<MasterCity> GetMasterCities();
        bool AddMasterCity(MasterCity masterCity);
        bool EditMasterCity(MasterCity masterCity);
        bool RemoveMasterCity(MasterCity masterCity);

        IQueryable<MasterArea> GetMasterAreas();
        bool AddMasterArea(MasterArea masterArea);
        bool EditMasterArea(MasterArea masterArea);
        bool RemoveMasterArea(MasterArea masterArea);

    }
}
