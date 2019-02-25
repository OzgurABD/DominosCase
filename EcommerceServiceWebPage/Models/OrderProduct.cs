using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceServiceWebPage.Models
{
    public class OrderProduct
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public int ProductTypeID { get; set; }
        public Dictionary<string, string> ProductType { get; set; }
    }
    //public class ProductType
    //{
    //    public int ID { get; set; }
    //    public string Name { get; set; }
    //}
}
