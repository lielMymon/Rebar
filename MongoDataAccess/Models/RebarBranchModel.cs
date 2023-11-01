using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDataAccess.Models
{
    public class RebarBranchModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ManagerName { get; set; }
        public string ManagerPassword { get; set; }
        public string Address { get; set; }
    }
}
