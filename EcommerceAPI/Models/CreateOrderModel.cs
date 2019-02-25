using System.Collections.Generic;

namespace EcommerceAPI.Models
{
    public class CreateOrderModel
    {
        public List<ProductDetails> ProductList { get; set; }
        public string CustomerName { get; set; }
        public string AddressDetail { get; set; }
    }
    public class ProductDetails
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
    }
}