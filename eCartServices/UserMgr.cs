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

        public bool CheckUserDetailsExist(string userId)
        {
            try
            {
                var userDetails = userDb.GetUserDetails().Where(u => u.UserId == userId).FirstOrDefault();
                if (userDetails != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }catch
            {
                return false;
            }
        }

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

        public UserDetail GetUserDetailsbyUserId(string userId)
        {
            try
            {
                return userDb.GetUserDetails().Where(u => u.UserId == userId).FirstOrDefault();
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
