using System;
using System.Collections.Generic;

namespace INCServer
{
    public partial class LastWatch
    {
        public int? Userid { get; set; }
        public long? Watchedid { get; set; }

        public virtual User User { get; set; }
        public virtual Watched Watched { get; set; }
    }
}
