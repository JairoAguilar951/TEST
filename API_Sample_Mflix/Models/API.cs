using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace API_Sample_Mflix.Models
{
    public class API
    {
        // Constructor para inicializar valores por defecto, si es necesario
        public API()
        {
            Time = DateTime.UtcNow;
            Value = 10; // Inicializa el valor con un valor por defecto si es necesario
        }

        // El campo de tiempo que Grafana usará para series temporales
        [BsonElement("timeField")]
        public DateTime Time { get; set; }

        // Identificador del documento
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        // Datos de sensores
        [BsonElement("Sensor1")]
        public double Sensor1 { get; set; }

        [BsonElement("Sensor2")]
        public double Sensor2 { get; set; }

        [BsonElement("Sensor3")]
        public double Sensor3 { get; set; }

        [BsonElement("Sensor4")]
        public double Sensor4 { get; set; }

        // Estado activo o inactivo
        [BsonElement("Active")]
        public bool Active { get; set; }

        // Color, como un objeto anidado
        [BsonElement("Color")]
        public Color Color { get; set; }

        // Campo de valor para las alertas o gráficos en Grafana
        [BsonElement("value")]
        public double Value { get; set; }

        // Método para obtener la hora actual en la zona horaria de México, si es necesario
        private DateTime GetCurrentTimeInMexico()
        {
            // Configurar la zona horaria de México
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");

            // Obtener la hora actual en la zona horaria especificada
            return TimeZoneInfo.ConvertTime(DateTime.UtcNow, timeZoneInfo);
        }
    }

    // Suponiendo que Color es una clase definida en tu modelo
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
