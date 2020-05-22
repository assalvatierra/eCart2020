using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCart.Areas.Store.Models;
using eCartInterfaces;
using eCartModels;
using eCartDbLayer;

namespace eCartServices
{
    public class StoreFactory
    {
        iStoreMgr storemgr;
        iCartMgr cartmgr;
        iRiderMgr riderMgr;
        iUserMgr userMgr;

        public StoreFactory()
        {
            //initialize storemgr
            this.storemgr = new StoreMgr();
            this.storemgr.setDbLayer(new StoreDBLayer());

            this.cartmgr = new CartMgr();
            this.cartmgr.setDbLayer(new CartDbLayer());

            this.userMgr = new UserMgr();
            this.riderMgr = new RiderMgr();

         
        }

        public iStoreMgr StoreMgr
        {
            get { return this.storemgr; }

        }

        public iCartMgr CartMgr
        {
            get { return this.cartmgr; }
        }


        public iUserMgr UserMgr
        {
            get { return this.userMgr; }
        }

        public iRiderMgr RiderMgr
        {
            get { return this.riderMgr; }
        }

        
    }
}