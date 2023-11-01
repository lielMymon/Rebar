using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rebar_LielMymon1
{
    public class ShakeModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Dis{ get; set; }
        public int Price_S { get; set; }
        public int Price_M { get; set; }
        public int Price_L { get; set; }
       
    }
}
