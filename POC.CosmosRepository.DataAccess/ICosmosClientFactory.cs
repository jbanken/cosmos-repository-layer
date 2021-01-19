using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;

namespace POC.CosmosRepository.DataAccess
{
    public interface ICosmosClientFactory
    {
        CosmosClient GetClient();
        Database GetDatabase();
        Container GetContainer(string containerName);
    }
}

