//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nike.Models.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class DetailBill
    {
        public int Id { get; set; }
        public Nullable<int> BillId { get; set; }
        public string Name { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<double> Price { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDay { get; set; }
        public Nullable<bool> Active { get; set; }
    
        public virtual Bill Bill { get; set; }
    }
}
