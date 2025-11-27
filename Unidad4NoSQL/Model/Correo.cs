using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Unidad4NoSQL.Model
{
    public class Correo
    {
        [BsonElement("tipo")] // Mapea a 'tipo' en Mongo
        public string Tipo { get; set; }

        [BsonElement("correo")] // Mapea a 'correo' en Mongo
        public string Correo_Electronico { get; set; }
    }
}
