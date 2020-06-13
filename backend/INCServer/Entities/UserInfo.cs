using Newtonsoft.Json;
using System;

namespace INCServer
{
    public partial class UserInfo
    {
        [JsonProperty("userid")]
        public int Userid { get; set; }
        [JsonProperty("firstname")]
        public string Firstname { get; set; }
        [JsonProperty("lastname")]
        public string Lastname { get; set; }
        [JsonProperty("birthday")]
        public DateTime Birthday { get; set; }
        [JsonProperty("imgsrc")]
        public string Imgsrc { get; set; }
        [JsonProperty("money")]
        public decimal Money { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
