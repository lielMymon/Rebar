using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDataAccess.Models
{
    
    public class RebarDataModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string RebarBranchId { get; set; }
        public DateTime Date {  get; set; }
        public int NumOfOrders { get; set; }
        public int InCome { get; set;}
        public int Expenses { get; set; }

    }
}
