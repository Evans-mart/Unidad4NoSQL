// Data/EmpleadosDataAccess.cs
using Unidad4NoSQL.Model;
using NLog;
using System.Threading.Tasks;

namespace Unidad4NoSQL.Data
{
    public class EmpleadosDataAccess
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly MongoDBDataAccess _mongoData;

        public EmpleadosDataAccess()
        {
            _mongoData = new MongoDBDataAccess();
        }
        // Retorna el ID (string), o el código de error ("DUPLICADO"), o null.
        public async Task<string> InsertarEmpleadoAsync(Empleado empleado)
        {
            _logger.Info($"Iniciando inserción de Empleado en MongoDB a través de DAL...");

            // Llamamos al método real de inserción en MongoDBDataAccess
            string resultado = await _mongoData.InsertarUsuarioAsync(empleado);

            // Se registra el resultado
            if (resultado != null && resultado.Length > 0 && !resultado.StartsWith("DUPLICADO_"))
            {
                _logger.Info($"Inserción completada. ID generado: {resultado}");
            }
            else
            {
                _logger.Error($"Inserción fallida o con duplicidad. Código/Mensaje: {resultado}");
            }

            // Simplemente retransmite el resultado
            return resultado;
        }

    }
}