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
    
    public partial class CartItem
    {
        public int Id { get; set; }
        public int CartDetailId { get; set; }
        public int StoreItemId { get; set; }
        public decimal ItemQty { get; set; }
        public string ItemOrder { get; set; }
        public int CartItemStatusId { get; set; }
        public string Remarks1 { get; set; }
        public string Remarks2 { get; set; }
    
        public virtual CartDetail CartDetail { get; set; }
        public virtual StoreItem StoreItem { get; set; }
        public virtual CartItemStatus CartItemStatu { get; set; }
    }
}
