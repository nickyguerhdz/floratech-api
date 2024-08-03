using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using MongoDB.Bson;
using MongoDB.Driver;

namespace floratech.Connection
{
    public class mongo_db
    {
        public mongo_db() {
            var connectionString = ConfigurationManager.AppSettings["MongoDBConnectionString"];
            var databaseName = ConfigurationManager.AppSettings["MongoDBDatabaseName"];
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            MongoClient = new MongoClient(settings);

            mongoDatabase = ClientDB(MongoClient, databaseName);
        }
        public mongo_db(string username, string password, string database_Name, string cluster)
        {
            MongoClient = ConnectionClient(username, password, cluster);
            mongoDatabase = ClientDB(MongoClient, database_Name);
        }

        public MongoClient MongoClient { get; set; }

        public IMongoDatabase mongoDatabase { get; set; }

        public MongoClient ConnectionClient(string username, string password, string cluster)
        {
            string connectionUri = $"mongodb+srv://{username}:{password}@{cluster}";
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            return new MongoClient(settings);
        }

        public IMongoDatabase ClientDB(MongoClient client, string database_Name)
        {
            return client.GetDatabase(database_Name);
        }

        public string ClientPing(MongoClient client, string databaseName)
        {
            string texto = " ";
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

