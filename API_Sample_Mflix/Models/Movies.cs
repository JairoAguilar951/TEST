using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Sample_Mflix.Models
{
    public class Movies
    {
        public Movies()
        {
            awards = new Awards();
            imdb = new Imdb();
            tomatoes = new tomatoes();
            imdb.rating = 0.0;
            imdb.votes = 0;
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("plot")]
        public string plot { get; set; }

        [BsonElement("name")]
        public string name { get; set; }

        [BsonElement("genres")]
        public string [] genres { get; set; }

        [BsonElement("runtime")]
        public int runtime { get; set; }

        [BsonElement("cast")]
        public string[] cast { get; set; }

        [BsonElement("poster")]
        public string poster { get; set; }

        [BsonElement("title")]
        public string title { get; set; }

        [BsonElement("fullplot")]
        public string fullplot { get; set; }

        [BsonElement("languages")]
        public string[] languages { get; set; }

        [BsonElement("released")]
        public DateTime released { get; set; }


        [BsonElement("directors")]
        public string[] directors { get; set; }

        [BsonElement("rated")]
        public string rated { get; set; }

        [BsonElement("awards")]
        public Awards awards { get; set; }

        [BsonElement("lastupdated")]
        public string lastupdated { get; set; }

        [BsonElement("year")]
        public int year { get; set; }

        [BsonElement("metacritic")]
        public int metacritic { get; set; }

        [BsonElement("imdb")]
        public Imdb imdb { get; set; }

        [BsonElement("countries")]
        public string[] countries { get; set; }

        [BsonElement("type")]
        public string type { get; set; }

        [BsonElement("tomatoes")]
        public tomatoes tomatoes { get; set; }

        [BsonElement("num_mflix_comments")]
        public int num_mflix_comments { get; set; }

        [BsonElement("writers")]
        public string[] writers { get; set; }



    }

    public class Awards
    {
        [BsonElement("wins")]
        public int wins { get; set; }

        [BsonElement("nominations")]
        public int nominationsins { get; set; }

        [BsonElement("text")]
        public string text { get; set; }
    }

    public class Imdb
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("id")]
        public int id { get; set; }

        [BsonElement("rating")]
        public double? rating { get; set; }

        [BsonElement("votes")]
        public int? votes { get; set; }

    }

    public class tomatoes
    {
        public tomatoes()
        {
            viewer = new viewer();
            critic = new critic();
        }

        [BsonElement("boxOffice")]
        public string boxOffice { get; set; }

        [BsonElement("consensus")]
        public string consensus { get; set; }

        [BsonElement("production")]
        public string production { get; set; }

        [BsonElement("dvd")]
        public DateTime dvd { get; set; }

        [BsonElement("viewer")]
        public viewer viewer { get; set; }

        [BsonElement("fresh")]
        public int fresh { get; set; }

        [BsonElement("critic")]
        public critic critic { get; set; }

        [BsonElement("rotten")]
        public int rotten { get; set; }

        [BsonElement("lastUpdated")]
        public DateTime lastUpdated { get; set; }

        [BsonElement("website")]
        public string website { get; set; }
    }

    public class viewer
    {
        [BsonElement("rating")]
        public double rating { get; set; }

        [BsonElement("numReviews")]
        public int numReviews { get; set; }

        [BsonElement("meter")]
        public int meter { get; set; }
    }

    public class critic
    {
        [BsonElement("rating")]
        public double rating { get; set; }

        [BsonElement("numReviews")]
        public int numReviews { get; set; }

        [BsonElement("meter")]
        public int meter { get; set; }
    }
}