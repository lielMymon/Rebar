using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Rebar_LielMymon1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDataAccess.Models
{
    public class OrdersHistory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }

        public int OrderingTimeSeconds { get; set; }
        public List<string> ShakesId { get; set; }=new List<string>();
        public int Paymet { get; set; }



        public OrdersHistory() { }
        public OrdersHistory(OrderModel order)
        {
            OrderId = order.Id;
            CustomerName = order.CustomerName;
            OrderDate = order.OrderDate;
            OrderingTimeSeconds = order.OrderingTimeSeconds;
            Paymet = order.Payment;
            order.ShakesList.ForEach(shake => ShakesId.Add(shake.Id));
          

        }

    }
}
