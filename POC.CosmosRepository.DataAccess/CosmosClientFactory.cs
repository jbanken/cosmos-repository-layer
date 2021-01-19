using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace POC.CosmosRepository.DataAccess
{
    public class CosmosClientFactory: ICosmosClientFactory
    {
        private readonly IConfiguration _configuration;
        private readonly CosmosClient _client;
        private readonly string _serviceEndpointUrl;
        private readonly string _databaseName;
        private readonly string _authKey;
        private readonly Database _database;

        public CosmosClientFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            _serviceEndpointUrl = _configuration["cosmos:db:connection.serviceendpoint"];
            _databaseName = configuration["cosmos.db.name"];
            _authKey = configuration["cosmos.db.connection.authkey"];

            //client
            _client = new CosmosClient(_serviceEndpointUrl, _authKey);

            //database
            _database = _client.CreateDatabaseIfNotExistsAsync(_databaseName).Result;

            //containers
            _database.CreateContainerIfNotExistsAsync("Dummies", "/ID");
        }

        public Database GetDatabase()
        {
            return _database;

        }

        public CosmosClient GetClient()
        {
            return _client;
        }

        public Container GetContainer(string containerName)
        {
            return _database.GetContainer(containerName);
        }

    }
}