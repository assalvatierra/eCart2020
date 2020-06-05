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

        public bool AddUserApplication(UserApplication userApplication)
        {
            try
            {
                return userDb.AddUserApplication(userApplication);
            }
            catch
            {
                return false;
            }
        }

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

        public bool EditUserApplication(UserApplication userApplication)
        {
            try
            {
                return userDb.EditUserApplication(userApplication);
            }
            catch
            {
                return false;
            }
        }

        public UserApplication GetUserApplication(int id)
        {
            try
            {
                return userDb.GetUserApplications().Where(a => a.Id == id).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public List<UserApplication> GetUserApplicationsList(int userDetailId)
        {
            try
            {
                return userDb.GetUserApplications().Where(a => a.UserDetailId == userDetailId).ToList();
            }
            catch
            {
                return null;
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

        public bool HasStoreApplication(int userDetailId)
        {
            try
            {
                var applicationsList = GetUserApplicationsList(userDetailId);
                
                if (applicationsList.Where(a=>a.UserApplicationTypeId == 1).Count() > 0)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool HasRiderApplication(int userDetailId)
        {
            try
            {
                var applicationsList = GetUserApplicationsList(userDetailId);

                if (applicationsList.Where(a => a.UserApplicationTypeId == 2).Count() > 0)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
