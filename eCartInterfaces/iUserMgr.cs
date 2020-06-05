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
        bool CheckUserDetailsExist(string userId);
    }
}
