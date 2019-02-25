using EcommerceService.Dbo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProductOrderService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ProductOrderService.svc or ProductOrderService.svc.cs at the Solution Explorer and start debugging.
    public class ProductOrderService : IProductOrderService
    {

        public bool CreateCustomer(string name, string address)
        {
            try
            {
                using (var context = new OrderContext())
                {
                    Customer customer = new Customer
                    {
                        Name = name,
                        Address = new CustomerAddress { Desc = address }
                    };
                    context.Customer.Add(customer);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CreateOrder(string customerName, string customerAddressDesc, List<OrderItems> orderItems)
        {
            try
            {
                using (var context = new OrderContext())
                {
                    var discountPrice = orderItems.Sum(oi => oi.DiscountValue);
                    var totalPrice = orderItems.Sum(oi => oi.ProductPrice);
                    Order order = new Order
                    {
                        Customer = new Customer { Address = new CustomerAddress { Desc = customerAddressDesc }, Name = customerName },
                        CustomerAddressDesc = customerAddressDesc,
                        DiscountPrice = discountPrice,
                        TotalPrice = totalPrice,
                        OrderItems = orderItems
                    };
                    context.Order.Add(order);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Product> GetProductList()
        {
            try
            {
                using (var context = new OrderContext())
                {
                    List<Product> products = context.Product.Include("ProductType").ToList();
                    return products;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool UpdateProduct(int ID, string name, string desc, decimal price)
        {
            try
            {
                using (OrderContext co = new OrderContext())
                {
                    Product product = co.Product.Find(ID);
                    product.Name = name;
                    product.Desc = desc;
                    product.Price = price;
                    co.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
