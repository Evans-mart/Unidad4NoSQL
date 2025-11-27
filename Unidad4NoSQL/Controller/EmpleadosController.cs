using NLog;
using Unidad4NoSQL.Data;
using Unidad4NoSQL.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver; // Necesario para algunas excepciones de Mongo

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
            Empleado empleado)     // Corregido: La vista DEBE pasar los IDs seleccionados
        {
            try
            {                
                //(Inserción Atómica en Mongo)
                _logger.Info($"Iniciando registro en Mongo...");
                String idGenerado = await _empleadosData.InsertarEmpleadoAsync(empleado);

                if (idGenerado.Length > 0)
                {
                    return (0, "Empleado registrado exitosamente.");
                }
                else if (idGenerado.Length == -1)
                {
                    return (-1, "Fallo en el registro: Dato duplicado (Correo).");
                }
                else // idGenerado == -2 (Error general en DAL)
                {
                    return (-2, "Error desconocido al intentar insertar el documento en MongoDB.");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error inesperado en el Controller al registrar empleado.");
                return (-4, $"Error inesperado: {ex.Message}");
            }
        }
    }
}