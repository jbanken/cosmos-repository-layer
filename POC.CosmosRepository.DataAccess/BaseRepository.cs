using Microsoft.Azure.Cosmos;
using POC.CosmosRepository.DataAccess.DataEntities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace POC.CosmosRepository.DataAccess
{
    public class BaseRepository<T> : IRepository<T>
    {
        private readonly Container _container;
        public BaseRepository(ICosmosClientFactory cosmosClientFactory, string containerName)
        {
            _container = cosmosClientFactory.GetContainer(containerName);
        }
       
        public async Task<T> AddAsync<T>(T entity, string paritionKeyValue) where T : BaseDataEntity
        {
            ItemResponse<T> entityResponse = await _container.CreateItemAsync<T>(entity, new PartitionKey(paritionKeyValue));

            if (entityResponse.StatusCode != HttpStatusCode.Created)
            {
                throw new Exception($"AddAsync() returned ${ entityResponse.StatusCode }");
            }

            return entityResponse.Resource;

        }
        public async Task<T> UpdateAsync<T>(T entity, string id, string paritionKeyValue) where T : BaseDataEntity
        {
            ItemResponse<T> entityResponse = await _container.ReplaceItemAsync(entity, id, new PartitionKey(paritionKeyValue));

            if (entityResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"UpdateAsync() returned ${ entityResponse.StatusCode }");
            }

            return entityResponse.Resource;
        }
        public async Task<T> GetByIDAsync<T>(string id, string paritionKeyValue) where T : BaseDataEntity
        {
            ItemResponse<T> entityResponse = await _container.ReadItemAsync<T>(paritionKeyValue, new PartitionKey(paritionKeyValue));

            if (entityResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"GetByIDAsync() returned ${ entityResponse.StatusCode }");
            }

            return entityResponse.Resource;

        }
        public async Task<IList<T>> GetAllAsync<T>() where T : BaseDataEntity
        {

            return new List<T>();
        }
        public async Task DeleteAsync<T>(string id, string paritionKeyValue) where T : BaseDataEntity
        {
            ItemResponse<T> entityResponse = await _container.DeleteItemAsync<T>(id, new PartitionKey(paritionKeyValue));
            if (entityResponse.StatusCode != HttpStatusCode.NoContent)
            {
                throw new Exception($"DeleteAsync() returned ${ entityResponse.StatusCode }");
            }
        }

      
    }
}