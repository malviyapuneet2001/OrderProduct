using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using entity.Repository;
using dal.Repository;

namespace MyWebApi.WebApi.Controllers
{
[ApiController]
[Route("[controller]")]
    public class TestUserData : ControllerBase
    {
        public TestUserData()
        {

        }

        [HttpPost(Name = "AddData")]
        public void AddData([FromBody] UserData data){
            UserDataDal obj1 = new UserDataDal();
            obj1.AddDATA(data);
        }

        [HttpGet("{id}", Name = "GetOrderData")]
        public OrderWithProductNames GetData(int id){
            UserDataDal obj1 = new UserDataDal();
            return obj1.GetOrderData(id);
        }

        // [HttpGet("{id}", Name = "GetOrderItemData")]
        // public OrderItem GetItemData(int id){
        //     UserDataDal obj1 = new UserDataDal();
        //     return obj1.GetOrderItemData(id);
        // }

        [HttpDelete("{id1}/{id2}", Name = "DeleteUserProduct")]
        public void DeleteData(int id1, int id2){
            UserDataDal obj1 = new UserDataDal();
            obj1.deleteOrderProduct(id1,id2);
        }
    }
}