using EcommerceAPI.Models;
using EcommerceAPI.ProductOrderService;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace EcommerceAPI.Controllers
{
    //[Authorize]
    public class OrderController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetProducts()
        {
            ProductOrderServiceClient client = new ProductOrderServiceClient();
            var result = client.GetProductList();
            client.Close();
            //return Json(result);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult OrderCreate(CreateOrderModel com)
        {
            try
            {
                ProductOrderServiceClient client = new ProductOrderServiceClient();
                List<OrderItems> orderItems = DiscountCalculation(com.ProductList);
                client.CreateOrder(com.CustomerName, com.AddressDetail, orderItems.ToArray());
                client.Close();
                return Ok();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public void ProductUpdate(UpdateProduct up)
        {
            //int ID, string name, string desc, decimal price
            ProductOrderServiceClient client = new ProductOrderServiceClient();
            client.UpdateProduct(up.ID, up.Name, up.Desc, up.Price);
            //client.UpdateProduct(ID, name, desc, price);
            client.Close();
        }

        private List<OrderItems> DiscountCalculation(List<ProductDetails> productDetails)
        {
            List<OrderItems> result = new List<OrderItems>();
            //      Redis
            ProductOrderServiceClient client = new ProductOrderServiceClient();
            Product[] products = client.GetProductList();
            client.Close();
            //      RedisEnd
            List<Product> orderProducts = products.Where(p => productDetails.Any(pd => pd.ID == p.ID)).ToList();
            decimal totalPriceWithoutDrinks = orderProducts.Where(op => op.ProductType.Name != "Drink").Sum(x => x.Price);
            decimal totalprice = orderProducts.Sum(x => x.Price);
            if (totalprice >= 100)
            {
                decimal discountTotalPrice = orderProducts.Where(op => op.ProductType.Name != "Drink").Sum(x => x.DiscountPrice);
                decimal diffPrice = totalPriceWithoutDrinks - discountTotalPrice;
                foreach (Product product in orderProducts)
                {
                    int quantity = productDetails.FirstOrDefault(pd => pd.ID == product.ID).Quantity;
                    decimal orderItemDiscountPrice = diffPrice * ((quantity * product.Price) / totalprice);
                    for (int i = 0; i < quantity; i++)
                    {
                        result.Add(new OrderItems
                        {
                            ProductID = product.ID,
                            //Product = product,
                            DiscountValue = orderItemDiscountPrice,
                            ProductDiscountPrice = product.DiscountPrice,
                            ProductPrice = product.Price,
                        });
                    }
                }

            }
            else
            {
                foreach (Product product in orderProducts)
                {
                    int quantity = productDetails.FirstOrDefault(pd => pd.ID == product.ID).Quantity;
                    for (int i = 0; i < quantity; i++)
                    {
                        result.Add(new OrderItems
                        {
                            ProductID = product.ID,
                            //Product = product,
                            DiscountValue = 0,
                            ProductDiscountPrice = product.DiscountPrice,
                            ProductPrice = product.Price,
                        });
                    }
                }
            }
            return result;
        }

    }
}