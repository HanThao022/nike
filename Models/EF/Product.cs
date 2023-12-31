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
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }
    
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Describe { get; set; }
        public string Image { get; set; }
        public Nullable<int> Warranty { get; set; }
        public Nullable<double> Size { get; set; }
        public string Color { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<int> ProductTypeId { get; set; }
        public Nullable<int> BrandId { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDay { get; set; }
        public Nullable<bool> Active { get; set; }
    
        public virtual Brand Brand { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ProductType ProductType { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
