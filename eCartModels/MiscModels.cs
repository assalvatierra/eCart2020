using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCartModels
{
    public class cCart : CartItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public int StoreId { get; set; }
        public string remarks1 { get; set; }
        public string remarks2 { get; set; }
        public string ItemImage { get; set; }
        public int CartItemStatusId { get; set; }
    }


    public class cCartDetails : CartDetail
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int PickupPointId { get; set; }
        public int CartStatus { get; set; }
        public DateTime DtPickup { get; set; }
        public string DeliveryType { get; set; }
        public string PaymentMode { get; set; }
        public List<cCart> cartItems { get; set; }
        public List<cCartPayment> cartPayments { get; set; }
        public string CheckedOut { get; set; }

    }

    public class cCartPayment : PaymentDetail
    {
        public int Id { get; set; }
        public int PaymentRecieverId { get; set; }
        public int PaymentPartyId { get; set; }
        public int PaymentStatusId { get; set; }
        public DateTime dtPayment { get; set; }
        public decimal Amount { get; set; }
        public string ReceiverInfo { get; set; }
        public string PartyInfo { get; set; }
    }

    public class cCartTransaction
    {
        public int Id { get; set; }
        public List<cCartDetails> cartDetails { get; set; }
    }

    public class CartClass
    {
      
    }

    public class jsonStoreItem
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public decimal UnitPrice { get; set; }
        public string ImageUrl { get; set; }
    }

}
