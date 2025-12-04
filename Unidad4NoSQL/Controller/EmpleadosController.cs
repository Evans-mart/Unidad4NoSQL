using NLog;
using Unidad4NoSQL.Data;
using Unidad4NoSQL.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Unidad4NoSQL.Controller
{
    public class EmpleadosController
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger(); 
        private readonly EmpleadosDataAccess _empleadosData;

        public EmpleadosController()
        {
            _empleadosData = new EmpleadosDataAccess();
        }

        public async Task<(int id, string mensaje)> RegistrarNuevoEmpleado(
            Empleado empleado)
        {
            try
            {
                _logger.Info($"Iniciando registro en Mongo...");
                // idGenerado puede ser: ID, null, "DUPLICADO_PRINCIPAL" o "DUPLICADO_SECUNDARIO"
                String resultado = await _empleadosData.InsertarEmpleadoAsync(empleado);

                //Verificación de CÓDIGO DE DUPLICIDAD
                if (resultado == "DUPLICADO_PRINCIPAL" || resultado == "DUPLICADO_SECUNDARIO")
                {
                    // Devolvemos -1 y el código de error
                    return (-1, resultado);
                }

                //Verificación de ÉXITO (ID de DON Mongo)
                if (!string.IsNullOrWhiteSpace(resultado) && resultado.Length >= 6)
                {
                    // Devolvemos 0 (Éxito)
                    return (0, $"Empleado registrado exitosamente con ID: {resultado}");
                }

                //Caso de error general
                return (-2, "Error desconocido al intentar insertar el documento en MongoDB.");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error inesperado en el Controller al registrar empleado.");
                return (-4, $"Error inesperado: {ex.Message}");
            }
        }
    }
}