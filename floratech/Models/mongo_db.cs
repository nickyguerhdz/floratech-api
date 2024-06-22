using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Libmongocrypt;

namespace floratech.Models
{
    public class mongo_db
    {
        public mongo_db()
        {
            //No hace nada.jpg
            //Propósito: El que tú quieras
            var connectionString = ConfigurationManager.AppSettings["MongoDBConnectionString"];
            var databaseName = ConfigurationManager.AppSettings["MongoDBDatabaseName"];
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            MongoClient = new MongoClient(settings);

            mongoDatabase = ClientDB(MongoClient,  databaseName);

            //Mentira, puedes crear una instancia vacía para usar los métodos dinámicamente (o sea, tú puedas llenar los atributos o los métodos para hacer lo que quieras)
            //Caso contrario, usa el constructor inferior para crear la instancia con información predeterminada
        }
        public mongo_db(string username, string password, string database_Name, string cluster)
        {
            //Constructor para conexión predeterminada usando [usuario], [contrasena] y [nombre de base de datos]
            //Propósito: Constructor para conexión predeterminada
            MongoClient = ConnectionClient(username, password, cluster);
            mongoDatabase = ClientDB(MongoClient, database_Name);
        }
        public mongo_db(string username, string password, string database_Name, string cluster, bool ping)
        {
            //Constructor para hacer ping con nuestras credenciales o visualizarlas para comprobarlas (en caso de que las envíes desde otro medio que no sea escribirlas directamente en este código, como tu propio formulario en frontend)
            //Propósito: Validar conexión
            if (ping)
            {
                MongoClient = ConnectionClient(username, password, cluster);
                mongoDatabase = ClientDB(MongoClient, database_Name);
                ClientPing(MongoClient, database_Name);
            }
        }

        //Parámetros
        public MongoClient MongoClient { get; set; }
        public IMongoDatabase mongoDatabase { get; set; }

        //Métodos de conexión
        public MongoClient ConnectionClient(string username, string password, string cluster)
        {
            //Conexión con el cliente/servidor
            //Propósito: Llegar al servidor
            string connectionUri = $"mongodb+srv://{username}:{password}@{cluster}";
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            return new MongoClient(settings);
        }

        public IMongoDatabase ClientDB(MongoClient client, string database_Name)
        {
            //Conexión con la base de datos
            //Propósito: Habiendo llegado al servidor, conectar con nuestra base de datos
            return client.GetDatabase(database_Name);
        }

        // Método para confirmar la conexión con la base de datos haciendo ping
        public string ClientPing(MongoClient client, string databaseName)
        {
            string texto = "";
            //Propósito: Comprobar si nuestras credenciales o información está correcta y se puede hacer ping de conexión
            try
            {
                var database = client.GetDatabase(databaseName);
                var command = new BsonDocument("ping", 1);
                var result = database.RunCommand<BsonDocument>(command);
                texto = "El ping ha sido exitoso, estás conectado a MongoDB!";
            }
            catch (Exception ex)
            {
                texto = $"Ha ocurrido un error: {ex.Message}";
            }
            return texto;
        }
    }
}