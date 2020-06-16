using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace INCServer
{
    public partial class User
    {
        public User()
        {
            Watched = new HashSet<Watched>();
        }

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonIgnore]
        public short Rightid { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        [JsonIgnore]
        public bool IsActive { get; set; }

        [JsonProperty("right")]
        public virtual UserRights Right { get; set; }
        [JsonIgnore]
        public virtual ICollection<Watched> Watched { get; set; }
    }
}
