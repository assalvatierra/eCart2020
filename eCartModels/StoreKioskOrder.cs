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
    
    public partial class StoreKioskOrder
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public System.DateTime DtOrder { get; set; }
        public int CartDetailId { get; set; }
        public int StoreKioskId { get; set; }
    
        public virtual CartDetail CartDetail { get; set; }
        public virtual StoreKiosk StoreKiosk { get; set; }
    }
}
