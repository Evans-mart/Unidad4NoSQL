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

        /// <summary>
        /// Verifica si un correo electrónico específico ya existe en el array 'correos.correo' de cualquier documento.
        /// </summary>
        /// <param name="correo">El correo a buscar.</param>
        /// <param name="excluirId">ID del documento a excluir de la búsqueda (útil en updates).</param>
        /// <returns>True si el correo ya existe en la base de datos.</returns>
        public async Task<bool> ExisteCorreoEnDBAsync(string correo, string excluirId = null)
        {
            if (string.IsNullOrWhiteSpace(correo)) return false;

            try
            {
                // El filtro utiliza el operador $elemMatch implícito para buscar dentro del array.
                // Busca cualquier documento donde el array 'correos' contenga un subdocumento
                // cuya propiedad 'correo' coincida con el valor proporcionado.
                var filter = Builders<Empleado>.Filter.Eq("correos.correo", correo);

                // aquí esto es por si damos un ID para excluir (por ejemplo, al actualizar), se añade una condición.
                if (!string.IsNullOrWhiteSpace(excluirId))
                {
                    var notIdFilter = Builders<Empleado>.Filter.Ne(e => e.Id, excluirId);
                    filter = Builders<Empleado>.Filter.And(filter, notIdFilter);
                }

                var count = await _context.Usuarios.CountDocumentsAsync(filter);
                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al verificar la unicidad del correo: {correo}");
                // En caso de error de conexión o similar, lanzamos la excepción para ser cachada.
                throw;
            }
        }


        public async Task<string> InsertarUsuarioAsync(Empleado nuevoEmpleado)
        {
            //EXTRAER y NORMALIZAR Correos a validar
            string correoPrincipal = nuevoEmpleado.Correos.Find(c => c.Tipo.Equals("PRINCIPAL", StringComparison.OrdinalIgnoreCase))?.Correo_Electronico.Trim().ToLowerInvariant();
            string correoSecundario = nuevoEmpleado.Correos.Find(c => c.Tipo.Equals("SECUNDARIO", StringComparison.OrdinalIgnoreCase))?.Correo_Electronico.Trim().ToLowerInvariant();

            //Aqui ya validamos contra la bd de Don MONGO

            //Validar Correo Principal
            if (!string.IsNullOrWhiteSpace(correoPrincipal) && await ExisteCorreoEnDBAsync(correoPrincipal))
            {
                _logger.Warn($"Correo Principal duplicado detectado: {correoPrincipal}");
                return "DUPLICADO_PRINCIPAL";
            }

            //Validar Correo Secundario (ojito!! que solo si existe y es diferente al principal ya entra aquí)
            if (!string.IsNullOrWhiteSpace(correoSecundario) && await ExisteCorreoEnDBAsync(correoSecundario))
            {
                _logger.Warn($"Correo Secundario duplicado detectado: {correoSecundario}");
                return "DUPLICADO_SECUNDARIO";
            }

            //La insertamos si todo OK
            try
            {
                await _context.Usuarios.InsertOneAsync(nuevoEmpleado);
                return nuevoEmpleado.Id;
            }
            catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
            {
                _logger.Error(ex, "Error de clave duplicada al insertar usuario (puede ser por otro índice).");
                return null;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error general al insertar usuario en MongoDB");
                return null;
            }
        }
    }
}