using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using EcommerceService.Dbo;

namespace EcommerceService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProductOrderService" in both code and config file together.
    [ServiceContract]
    public interface IProductOrderService
    {
        [OperationContract]
        bool CreateOrder(string customerName, string customerAddressDesc, List<OrderItems> orderItems);

        [OperationContract]
        List<Product> GetProductList();

        [OperationContract]
        bool UpdateProduct(int ID, string name, string desc, decimal price);

        [OperationContract]
        bool CreateCustomer(string name, string address);
    }
}
