using System;
using System.Collections.Generic;

namespace INCServer
{
    public partial class Watched
    {
        public long Id { get; set; }
        public int Userid { get; set; }
        public int Filmid { get; set; }
        public TimeSpan Timestop { get; set; }
        public short? Rating { get; set; }
        public bool Islike { get; set; }
        public bool Iswatched { get; set; }

        public virtual Films Film { get; set; }
        public virtual User User { get; set; }
    }
}
