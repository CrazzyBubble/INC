using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace INCServer
{
    public partial class UserRights
    {
        public UserRights()
        {
            Users = new HashSet<User>();
        }

        [JsonProperty("id")]
        public short Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
