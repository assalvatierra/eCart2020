using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCartInterfaces;
using eCartModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace eCartDBLayer
{
    public class UserDBLayer : iUserDb
    {
        ecartdbContainer db = new ecartdbContainer();

        public IQueryable<UserDetail> GetUserDetails()
        {
            return db.UserDetails;
        }

        public string GetUserId(string email)
        {
            try
            {
                var context = new IdentityDbContext();
                var users = context.Users.Where(u=>u.Email.ToLower() == email.ToLower()).FirstOrDefault().Id;
                return users;
            }
            catch
            {
                return null;
            }
        }

    }
}
