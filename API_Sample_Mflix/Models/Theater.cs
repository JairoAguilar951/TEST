using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API_Sample_Mflix.Models
{
    public class Theater
    {
        public Theater()
        {
            //Se tomará cada atributo de la clase para rellenarlos con un valor por defecto y no se generen nulos
            //Propósito: Definir valores por defecto al crear instancia

            //##Nota, no definiremos las Ids primarias, esas se asignan solitas, si las definimos estropearemos el funcionamiento del programa y no hará nada
            TheaterId = 0;
            Location = new Location();
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("theaterId")]
        public int TheaterId { get; set; }

        [BsonElement("location")]
        public Location Location { get; set; }
    }

    public class Location
    {
        public Location()
        {
            Address = new Address();
            Geo = new Geo();
        }

        [BsonElement("address")]
        public Address Address { get; set; }

        [BsonElement("geo")]
        public Geo Geo { get; set; }
    }

    public class Address
    {
        public Address()
        {
            Street1 = "New street.";
            City = "New city";
            State = "New state";
            Zipcode = "New zipcode";
        }

        [BsonElement("street1")]
        public string Street1 { get; set; }

        [BsonElement("street2")]
        public string Street2 { get; set; }

        [BsonElement("city")]
        public string City { get; set; }

        [BsonElement("state")]
        public string State { get; set; }

        [BsonElement("zipcode")]
        public string Zipcode { get; set; }
    }

    public class Geo
    {
        public Geo()
        {
            Type = "";
            Coordinates = new double[] { 0.0, 0.0 };
        }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("coordinates")]
        public double[] Coordinates { get; set; }
    }
}