using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace INCServer
{
    public partial class Countries
    {
        public Countries()
        {
            Films = new HashSet<Films>();
        }

        [JsonProperty("id")]
        public short Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        public virtual ICollection<Films> Films { get; set; }
    }
}
