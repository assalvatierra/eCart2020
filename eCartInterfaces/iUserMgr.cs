using eCartModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCartInterfaces
{
    public interface iUserMgr
    {
        string GetUserId(string email);
        UserDetail GetUserDetails(int id);
        UserDetail GetUserDetailsbyUserId(string userId);
        bool CheckUserDetailsExist(string userId);

        UserApplication GetUserApplication(int id);
        List<UserApplication> GetUserApplicationsList(int userDetailId);
        bool AddUserApplication(UserApplication userApplication);
        bool EditUserApplication(UserApplication userApplication);
        bool HasStoreApplication(int userDetailId);
        bool HasRiderApplication(int userDetailId);
    }
}
