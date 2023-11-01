using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MongoDataAcces.Models
{
    public class OrderModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string[] ShakesList { get; set; }
        public int Payment { get; set; }
        public string CustomerName { get; set; }
        public string OrderDate { get; set; }
        public string[] Sale { get; set; }
    }
}
