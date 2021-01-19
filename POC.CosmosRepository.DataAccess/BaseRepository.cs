using Microsoft.Azure.Cosmos;
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
        public BaseRepository(Container container)
        {
            _container = container;
        }

        public async Task<T> AddAsync<T>(T entity, string paritionKey)
        {
            ItemResponse<T> entityResponse = await _container.CreateItemAsync<T>(entity, new PartitionKey(paritionKey));

            if (entityResponse.StatusCode != HttpStatusCode.Created)
            {
                throw new Exception($"AddAsync() returned ${ entityResponse.StatusCode }");
            }

            return entityResponse.Resource;

        }
        public async Task<T> UpdateAsync(T entity, Guid ID, string paritionKey)
        {
            ItemResponse<T> entityResponse = await _container.ReplaceItemAsync(entity, ID.ToString(), new PartitionKey(paritionKey));

            if (entityResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"UpdateAsync() returned ${ entityResponse.StatusCode }");
            }

            return entityResponse.Resource;
        }
        public async Task<T> GetByIDAsync(Guid ID, string paritionKey)
        {
            ItemResponse<T> entityResponse = await _container.ReadItemAsync<T>(ID.ToString(), new PartitionKey(paritionKey));

            if (entityResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"GetByIDAsync() returned ${ entityResponse.StatusCode }");
            }

            return entityResponse.Resource;

        }
        public async Task<(int total, IEnumerable<T> items)> GetAsync(int limit, int offset)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteAsync(Guid ID, string paritionKey)
        {
            ItemResponse<T> entityResponse = await _container.DeleteItemAsync<T>(ID.ToString(), new PartitionKey(paritionKey));
        }
    }
}