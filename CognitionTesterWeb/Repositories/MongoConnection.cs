using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace CognitionTesterWeb.Repositories
{
    public class MongoConnection
    {
        private static MongoClient client;
        private static IMongoDatabase database;

        static MongoConnection()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json"); 

            string mongodbConnStr = builder.Build().GetValue<string>(
                "MongoDb:ConnectionString");
            string databaseName = builder.Build().GetValue<string>(
                "MongoDb:Database");

            client = new MongoClient(mongodbConnStr);
            database = client.GetDatabase(databaseName);
        }

        internal IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return database.GetCollection<T>(collectionName);
        }
    }
}
