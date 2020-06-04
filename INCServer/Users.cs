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

        public int Id { get; set; }
        public string Email { get; set; }
        public short Rightid { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public virtual UserRights Right { get; set; }
        public virtual ICollection<Watched> Watched { get; set; }
    }
}
