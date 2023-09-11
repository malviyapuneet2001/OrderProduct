using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using entity.Repository;
using Npgsql.EntityFrameworkCore.PostgreSQL; 
using Microsoft.EntityFrameworkCore;

namespace dal.Repository
{
    public class UserDataDal
    {
        public UserDataDal()
        {
            
        }
        //ChangesAgain
        public void AddDATA(UserData userData){
            MyProjDb obj1 = new MyProjDb();
             var newOrder = new Order
            {
                OrderDate = DateTime.SpecifyKind( DateTime.Now, DateTimeKind.Utc),
                CustomerName = userData.CustomerName,
                Address = userData.Address,
                City = userData.City,
                State = userData.State,
                MobileNo = userData.MobileNo,
                OrderList = new List<OrderItem>(),
            };
            decimal totAmount = 0;

                foreach(var userProd in userData.Products)
                {
                    var productToAdd = obj1.Products.FirstOrDefault(prod => prod.Id == userProd.ProductId);
                    if (productToAdd != null)
                    {
                        var OrderItem = new OrderItem
                        {
                            ProductId = userProd.ProductId,
                            Price = productToAdd.Price,
                            Quantity = userProd.Quantity,
                            Amount = productToAdd.Price * userProd.Quantity,
                        };
                        newOrder.OrderList.Add(OrderItem);
                        totAmount += OrderItem.Amount;
                    }
                }
                newOrder.OrderAmount = totAmount;
                obj1.Orders.Add(newOrder);
                obj1.SaveChanges();
            
                // Console.WriteLine("NAME" + ":" + userData.CustomerName);
                // Console.WriteLine("Address" + ":" + userData.Address);
                // Console.WriteLine("City" + ":" + userData.City);
                // Console.WriteLine("State" + ":" + userData.State);
                // Console.WriteLine("MobileNo" + ":" + userData.MobileNo);
                // foreach(UserProd i in userData.Products){
                //     Console.WriteLine(i.ProductId);
                //     Console.WriteLine(i.Quantity);
                // }

        }

        public OrderWithProductNames GetOrderData(int id){
            MyProjDb obj1 = new MyProjDb();
            var Orderlist = obj1.Orders
            .Include(p => p.OrderList)
            // .Include("OrderItem")
            .Include("OrderList.Product")
            .FirstOrDefault(o => o.Id == id);
        
            if (Orderlist == null){
                return null;
            }

            var ProductNames = Orderlist.OrderList.Select(orderItem => (orderItem.Product.ProdName, orderItem.Product.Price, orderItem.Product.Id, orderItem.Quantity, orderItem.Amount)).ToList();
            var response = new OrderWithProductNames
            {
                Id = Orderlist.Id,
                CustomerName = Orderlist.CustomerName,
                OrderAmount = Orderlist.OrderAmount,
                MobileNo = Orderlist.MobileNo,

            };
                foreach (var item in ProductNames)
                {
                    
                    var ProductItem = new ProductItem{
                        Id = item.Id,
                        ProdName = item.ProdName,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        Amount = item.Amount,
    
                        
                    };
                        response.ProdItems.Add(ProductItem);
                    
                }
            return response;
            // foreach (var orderd in Orderlist.OrderList){
            //     Console.WriteLine(orderd.Product.Id);
            //     Console.WriteLine(orderd.Product.ProdName);
            // }
         
            // // var result = new specificdata
            // // {
            // //     Id = Orderlist.Id,
            // //     SpecificData = specificData
            // // };
            
            // //  Orderlist.OrderList.Select(orderItem => (orderItem.Product.ProductId, orderItem.Product.ProdName));
            // return Orderlist;  
        }

        public void deleteOrderProduct(int id1, int id2){
            MyProjDb obj1 = new MyProjDb();
            var RemoveList = obj1.Items.FirstOrDefault(o => o.OrderId == id1 && o.ProductId == id2);
               if (RemoveList != null)
                {
                    var ProductPrice = RemoveList.Amount;
                    var order = obj1.Orders.FirstOrDefault(o => o.Id == id1);
                    if (order != null)
                    {
                        order.OrderAmount -= ProductPrice;
                        obj1.Items.Remove(RemoveList);
                        obj1.SaveChanges();
                    }
                }
        }

        
    }
}
            // if (Orderlist.OrderList != null){

                //CHECK ORDELIST WORKING or noT
            //     foreach(var o in Orderlist.OrderList){
            //         Console.WriteLine(o.Id);
            //     }
            // }
            // else{
            //     Console.WriteLine("Not Found");
            // }
