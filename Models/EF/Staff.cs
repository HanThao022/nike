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
    
    public partial class Staff
    {
        public int StaffId { get; set; }
        public string FullName { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> CreateDay { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<int> AccountId { get; set; }
    
        public virtual Account Account { get; set; }
    }
}
