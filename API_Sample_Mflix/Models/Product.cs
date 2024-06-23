using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Sample_Mflix.Models
{
    public class Product
    {
        public Product()
        {
            Color = new Color(); // Aquí se inicializa la propiedad Color correctamente
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Sensor1")]
        public float Sensor1 { get; set; }

        [BsonElement("Sensor2")]
        public float Sensor2 { get; set; }

        [BsonElement("Sensor3")]
        public float Sensor3 { get; set; }

        [BsonElement("Sensor4")]
        public float Sensor4 { get; set; }

        // Aquí se define la propiedad Color
        [BsonElement("Color")]
        public Color Color { get; set; }
    }

    public class Color
    {
        [BsonElement("Green")]
        public int Green { get; set; }

        [BsonElement("Red")]
        public int Red { get; set; }

        [BsonElement("Blue")]
        public int Blue { get; set; }
    }
}
