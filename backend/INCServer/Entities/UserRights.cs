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

        public short Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
