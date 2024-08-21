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

        [BsonElement("Width")]
        public float Width { get; set; }

        [BsonElement("Large")]
        public float Large { get; set; }

        [BsonElement("Depth")]
        public float Depth { get; set; }

        [BsonElement("Color")]
        public Color Color { get; set; }
    }


}
