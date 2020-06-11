using Newtonsoft.Json;

namespace INCServer
{
    public partial class Studios
    {
        [JsonProperty("id")]
        public short Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
