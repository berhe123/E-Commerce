
namespace E_Commerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class Product
    {
        public Product()
        {
            this.Color_Options = new HashSet<Color_Options>();
        }
             [Key]
        public int productId { get; set; }

        [Required(ErrorMessage="please enter  Model!!!")]
        public string model { get; set; }

        [Required(ErrorMessage = "please enter  supply Name!!!")]
        public Nullable<int> suppyId { get; set; }

        [Required(ErrorMessage = "please enter  price!!!")]
        [DataType(DataType.Currency)]
        
       
        public Nullable<double> price { get; set; }

        [Required(ErrorMessage = "please enter  product Type Name!!!")]
        public Nullable<int> producttypeId { get; set; }

        [Required(ErrorMessage = "please enter  salemodel!!!")]      
        public string salemodel { get; set; }

        [Required(ErrorMessage = "please enter  Photo!!!")]
       
        public string photo { get; set; }
    
        public virtual ICollection<Color_Options> Color_Options { get; set; }
        public virtual ProductType ProductType { get; set; }
        public virtual Supply Supply { get; set; }
    }
}
