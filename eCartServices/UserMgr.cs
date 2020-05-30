using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCartInterfaces;
using eCartDBLayer;
using eCartModels;

namespace eCartServices
{
    class UserMgr : iUserMgr
    {
        private iUserDb userDb = new UserDBLayer();

        public UserDetail GetUserDetails(int id)
        {
            try
            {
                return userDb.GetUserDetails().Where(u => u.Id == id).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public string GetUserId(string email)
        {
            try
            {
                var user = userDb.GetUserId(email);
                return user;
            }
            catch
            {
                return null;
            }
        }


    }
}
