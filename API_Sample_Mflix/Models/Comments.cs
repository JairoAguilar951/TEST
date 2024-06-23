using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Sample_Mflix.Models
{
    public class Comments
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string name { get; set; }

        [BsonElement("email")]
        public string email { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("movie_id")]
        public string movie_id { get; set; }

        [BsonElement("text")]
        public string text { get; set; }

        [BsonElement("date")]
        public DateTime date { get; set; }
    }
}