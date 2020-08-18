using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cognizant.ChallengesApi.Controllers.Challanges.jsons
{
    [JsonObject]
    public class ResponseModel
    {
        [JsonProperty]
        public string Result { get; set; }
    }
}
