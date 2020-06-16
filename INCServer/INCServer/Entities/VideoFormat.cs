using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace INCServer
{
    public partial class VideoFormat
    {
        [JsonProperty("id")]
        public short Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
