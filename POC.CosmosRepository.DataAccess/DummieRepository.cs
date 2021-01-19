using POC.CosmosRepository.DataAccess.DataEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace POC.CosmosRepository.DataAccess
{
    public class DummieRepository : BaseRepository<DummieDataEntity>, IDummieRepository
    {
        public DummieRepository(ICosmosClientFactory cosmosClientFactory) : base(cosmosClientFactory, "Dummies")
        {

        }
    }
}