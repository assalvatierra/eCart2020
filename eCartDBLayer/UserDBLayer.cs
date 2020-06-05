using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        private ecartdbContainer db = new ecartdbContainer();

        public bool AddUserApplication(UserApplication userApplication)
        {
            try
            {

                db.UserApplications.Add(userApplication);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditUserApplication(UserApplication userApplication)
        {
            try
            {
                db.Entry(userApplication).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<UserApplication> GetUserApplications()
        {
            return db.UserApplications;
        }

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
