using Newtonsoft.Json;
using Npgsql.TypeHandlers.NumericHandlers;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;

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
        public virtual User User { get; set; }
    }
}
