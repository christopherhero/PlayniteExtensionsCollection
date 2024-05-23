﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNDBFuze.VndbDomain.Aggregates.ProducerAggregate;

namespace VNDBFuze.VndbDomain.Aggregates.ReleaseAggregate
{
    public class ReleaseProducer : Producer
    {
        [JsonProperty("developer")]
        public bool Developer { get; set; }

        [JsonProperty("publisher")]
        public bool Publisher { get; set; }
    }
}