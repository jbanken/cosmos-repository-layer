using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace POC.CosmosRepository.DataAccess
{
    public interface IRepository<T>
    {
        Task<T> AddAsync<T>(T entity, string paritionKeyValue) where T : DataEntities.BaseDataEntity;
        Task<T> GetByIDAsync<T>(string id, string partitionKeyValue) where T : DataEntities.BaseDataEntity;
        Task<IList<T>> GetAllAsync<T>() where T : DataEntities.BaseDataEntity;
        Task<T> UpdateAsync<T>(T entity, string id, string paritionKeyValue) where T : DataEntities.BaseDataEntity;
        Task DeleteAsync<T>(string id, string partitionKeyValue) where T : DataEntities.BaseDataEntity;

    }
}