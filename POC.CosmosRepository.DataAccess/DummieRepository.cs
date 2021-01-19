using POC.CosmosRepository.DataAccess.DataEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace POC.CosmosRepository.DataAccess
{
    public class DummieRepository : BaseRepository<DummieDataEntity>
    {
        public DummieRepository(ICosmosClientFactory cosmosClientFactory)
        {
            var container = cosmosClientFactory.GetContainer("Dummies");
            base(container);
        }

    }
}