using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCartInterfaces;
using eCartDBLayer;

namespace eCartServices
{
    class UserMgr : iUserMgr
    {
        private iUserDb userDb = new UserDBLayer();

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
