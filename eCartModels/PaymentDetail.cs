//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eCartModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class PaymentDetail
    {
        public int Id { get; set; }
        public int CartDetailId { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime dtPayment { get; set; }
        public int PaymentReceiverId { get; set; }
        public string ReceiverInfo { get; set; }
        public int PaymentPartyId { get; set; }
        public string PartyInfo { get; set; }
        public int PaymentStatusId { get; set; }
    
        public virtual CartDetail CartDetail { get; set; }
        public virtual PaymentReceiver PaymentReceiver { get; set; }
        public virtual PaymentStatus PaymentStatu { get; set; }
        public virtual PaymentParty PaymentParty { get; set; }
    }
}