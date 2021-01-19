using POC.CosmosRepository.DataAccess.DataEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace POC.CosmosRepository.DataAccess
{
    public interface IDummieRepository : IRepository<DummieDataEntity>
    {
    }
}
