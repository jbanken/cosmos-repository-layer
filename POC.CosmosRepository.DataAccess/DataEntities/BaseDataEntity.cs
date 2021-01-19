using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace POC.CosmosRepository.DataAccess.DataEntities
{
    public class BaseDataEntity
    {

        public string ID { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
    }
}
