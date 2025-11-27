using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Unidad4NoSQL.Model
{
    public class Empleado
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("correos")]
        public List<Correo> Correos { get; set; } = new List<Correo>();

        // Constructor por defecto
        public Empleado()
        {
        }
    }

}
