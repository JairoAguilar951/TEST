using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Sample_Mflix.Models
{
    public class User
    {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("username")]
    public string username { get; set; }

    [BsonElement("password")]
    public string password { get; set; }

    [BsonElement("role")]
    public string role { get; set; }

    [BsonElement("status")]
    public bool status { get; set; }
    }
}