using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace POC.CosmosRepository.DataAccess
{
    public interface IRepository<T>
    {
        Task<T> AddAsync<T>(T entity, string paritionKey) where T : DataEntities.BaseDataEntity;
        Task<T> GetByIdAsync<T>(string id, string partitionKey) where T : DataEntities.BaseDataEntity;
        Task<IList<T>> GetAllAsync<T>(string partitionKey) where T : DataEntities.BaseDataEntity;
        Task<T> UpdateAsync<T>(T entity, string partitionKey) where T : DataEntities.BaseDataEntity;
        Task DeleteAsync<T>(string id, string partitionKey) where T : DataEntities.BaseDataEntity;

    }
}