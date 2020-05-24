using eCartInterfaces;
using eCartDbLayer;

namespace eCartServices
{
    public class StoreFactory
    {
        iStoreMgr storemgr;
        iCartMgr cartmgr;
        iRiderMgr riderMgr;
        iUserMgr userMgr;
        iDBRefDBLayer DBRef;
        public DBRefContext dbRef = new DBRefContext();

        public StoreFactory()
        {
            //initialize storemgr
            this.storemgr = new StoreMgr();
            this.storemgr.setDbLayer(new StoreDBLayer());

            this.cartmgr = new CartMgr();
            this.cartmgr.setDbLayer(new CartDbLayer());

            this.userMgr = new UserMgr();
            this.riderMgr = new RiderMgr();

            this.DBRef = new DBRefDBLayer();
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

        public iDBRefDBLayer RefDbLayer
        { get { return this.DBRef; } }

        
    }
}