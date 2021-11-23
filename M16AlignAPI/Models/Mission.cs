using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;
using System.Drawing;


namespace M16AlignAPI.Models
{
    [BsonIgnoreExtraElements]
    public class Mission
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("agent")]
        public string Agent { get; set; }
        [BsonElement("country")]
        public string Country { get; set; }
        [BsonElement("address")]
        public string Address { get; set; }
        [BsonElement("date")]
        public string Date { get; set; }
        [BsonElement("latitude")]
        public double Latitude { get; set; }
        [BsonElement("longitude")]
        public double Longitude { get; set; }     

    }
}
