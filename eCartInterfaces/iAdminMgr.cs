using eCartModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCartInterfaces
{
    public interface iAdminMgr
    {
        bool DbDispose();

        bool AddStoreDetails(StoreDetail storeDetail);
        bool EditStoreDetails(StoreDetail storeDetail);
        bool RemovestoreDetails(StoreDetail storeDetail);
        StoreDetail GetStoreDetail(int id);

        bool AddUserDetails(UserDetail userDetail);
        bool EditUserDetails(UserDetail userDetail);
        bool RemoveUserDetails(UserDetail userDetail);
        UserDetail GetUserDetail(int id);
        List<UserDetail> GetUserDetailList();

        bool AddMasterCity(MasterCity masterCity);
        bool EditMasterCity(MasterCity masterCity);
        bool RemoveMasterCity(MasterCity masterCity);
        MasterCity GetMasterCity(int id);
        List<MasterCity> GetMasterCitiesList();


        bool AddMasterArea(MasterArea masterArea);
        bool EditMasterArea(MasterArea masterArea);
        bool RemoveMasterArea(MasterArea masterArea);
        MasterArea GetMasterArea(int id);
        List<MasterArea> GetMasterAreaList();
    }
}
