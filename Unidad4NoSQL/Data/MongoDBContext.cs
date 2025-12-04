// Data/MongoDbContext.cs (CORREGIDA)
using System;
using System.Configuration;
using MongoDB.Driver;
using Unidad4NoSQL.Model;
using NLog;

namespace Unidad4NoSQL.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
       private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public MongoDbContext()
        {
            try
            {
                // Leer la configuración del app.config
                string connectionString = ConfigurationManager.AppSettings["MongoDbConnectionString"];
                string databaseName = ConfigurationManager.AppSettings["MongoDbDatabaseName"];

                if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(databaseName))
                {
                    throw new InvalidOperationException("La configuración de MongoDB no se encontró en app.config.");
                }

                var client = new MongoClient(connectionString);
                _database = client.GetDatabase(databaseName);
                _logger.Info($"Conexión exitosa a la base de datos Mongo: {databaseName}");
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Error CRÍTICO al inicializar el contexto de MongoDB.");
                throw;
            }
        }

        // Colección principal
        public IMongoCollection<Empleado> Usuarios
        {
            get { return _database.GetCollection<Empleado>("usuarios"); }
        }
    }
}