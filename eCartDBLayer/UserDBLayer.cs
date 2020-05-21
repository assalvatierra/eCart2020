using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCartInterfaces;
using eCartModels;

namespace eCartDBLayer
{
    class UserDBLayer : iUserDb
    {
        ecartdbContainer db = new ecartdbContainer();

        public string GetUserId(string email)
        {
            try
            {
               
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
