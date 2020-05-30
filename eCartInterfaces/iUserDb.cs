using eCartModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCartInterfaces
{
    public interface iUserDb
    {
        string GetUserId(string email);
        IQueryable<UserDetail> GetUserDetails();
    }
}
