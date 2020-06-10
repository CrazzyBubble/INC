using System;
using System.Collections.Generic;

namespace INCServer
{
    public partial class FilmStudio
    {
        public int Filmid { get; set; }
        public short Studioid { get; set; }

        public virtual Films Film { get; set; }
        public virtual Studios Studio { get; set; }
    }
}
