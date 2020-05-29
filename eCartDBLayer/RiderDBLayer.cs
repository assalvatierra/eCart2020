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
    public class RiderDBLayer : iRiderDb
    {
        ecartdbContainer db = new ecartdbContainer();

        public IQueryable<RiderDetail> GetRiderDetails()
        {
            return db.RiderDetails;
        }
    }
}
