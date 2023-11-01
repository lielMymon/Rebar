using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Rebar_LielMymon1;

namespace MongoDataAccess.Models
{
    public class OrderModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public List<ShakeModel> ShakesList { get; set; }=new List<ShakeModel>();
        public int Payment { get; set; } = 0;
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderingTimeSeconds { get; set; }
        public string[] Sale { get; set; }

        public OrderModel() { }
        //public OrderModel(ShakeModel[] ShakesInOrder)
        //{
        //    this.ShakesList = ShakesInOrder;
        //}

    }

    
}
