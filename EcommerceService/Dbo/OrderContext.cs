namespace EcommerceService.Dbo
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;

    public partial class OrderContext : DbContext
    {
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<CustomerAddress> CustomerAddress { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }


        public OrderContext()
            : base("name=OrderContextDefault")
        {
            Configuration.ProxyCreationEnabled = false;
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }

    public class Customer
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        [ForeignKey("Address")]
        public int AddressID { get; set; }
        [Required]
        public virtual CustomerAddress Address { get; set; }
    }
    public class Product
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }

        [ForeignKey("ProductType")]
        public int ProductTypeID { get; set; }
        [Required]
        public virtual ProductType ProductType { get; set; }
    }
    public class ProductType
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class CustomerAddress
    {
        [Key]
        public int ID { get; set; }
        public string Desc { get; set; }
    }
    public class Order
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
        [Required]
        public Customer Customer { get; set; }

        public decimal TotalPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public string CustomerAddressDesc { get; set; }

        public virtual List<OrderItems> OrderItems { get; set; }
    }
    public class OrderItems
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [ForeignKey("Order")]
        public int OrderID { get; set; }

        public Order Order { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductID { get; set; }

        public Product Product { get; set; }

        public decimal ProductPrice { get; set; }
        public decimal ProductDiscountPrice { get; set; }
        public decimal DiscountValue { get; set; }
    }
}
