//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace E_Commerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class ProductType
    {
        public ProductType()
        {
            this.Products = new HashSet<Product>();
        }
    
        public int producttypeId { get; set; }

        [Required(ErrorMessage="Please input the product Type Name!")]    
        public string product_type_name { get; set; }
    
        public virtual ICollection<Product> Products { get; set; }
    }
}
