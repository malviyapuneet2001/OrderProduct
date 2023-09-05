using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 
using System.Text.Json;
using System.Text.Json.Serialization;

namespace entity.Repository
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int MobileNo { get; set; }
        public decimal OrderAmount { get; set; }
        public List<OrderItem> OrderList { get; set;}
    }

    public class OrderItem
    {
        [Key]
        public int Id {get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }

    }

    public class Product
    {
        [Key]
        public int Id {get; set; }
        public string ProdName { get; set; }
        public decimal Price {get; set; }
    }

    public class UserData
    {
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int MobileNo { get; set; }
        public List<UserProd> Products { get; set;}
    }

    public class UserProd
    {

        public int ProductId { get; set; }

        public int Quantity { get; set; }
           
    }
    public class OrderWithProductNames
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public decimal OrderAmount { get; set; }
        public int MobileNo { get; set; }
        public List<ProductItem> ProdItems { get; set; } = new List<ProductItem>();
    }

    public class ProductItem
    {

        [Key]
        public int Id {get; set; }
        public string ProdName { get; set; }
        public decimal Price {get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }


    }   
}
