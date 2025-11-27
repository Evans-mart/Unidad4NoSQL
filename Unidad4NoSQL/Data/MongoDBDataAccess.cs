// Data/MongoDBDataAccess.cs (CORREGIDA y AMPLIADA)
using System;
using System.Collections.Generic; // Para List<KeyValuePair>
using System.Threading.Tasks;
using Unidad4NoSQL.Model;
using MongoDB.Driver;
using NLog;

namespace Unidad4NoSQL.Data
{
    public class MongoDBDataAccess
    {
        private readonly MongoDbContext _context;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public MongoDBDataAccess()
        {
            _context = new MongoDbContext();
        }

        public IMongoCollection<Empleado> Usuarios => _context.Usuarios; // Propiedad de acceso directo para PersonasDataAccess


        public async Task<string> InsertarUsuarioAsync(Empleado nuevoEmpleado)
        {
            try
            {
                // 1. Ya no se calcula el ID. Simplemente insertamos el objeto.
                //    MongoDB generará el _id automáticamente en este paso.
                await _context.Usuarios.InsertOneAsync(nuevoEmpleado);

                // 2. Después de la inserción, el driver de C# actualiza automáticamente
                //    el objeto 'nuevoEmpleado' con el ID que se acaba de generar.
                return nuevoEmpleado.Id;
            }
            catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
            {
                // Error específico si se viola un índice único (CURP, RFC, etc.)
                _logger.Error(ex, "Error de clave duplicada al insertar usuario.");
                return null; // O un string vacío, para indicar el fallo
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error general al insertar usuario en MongoDB");
                return null; // O un string vacío
            }
        }            
    }
}