using System;
using System.Collections.Generic;

namespace INCServer
{
    public partial class FilmResources
    {
        public int? Filmid { get; set; }
        public string Src { get; set; }
        public short? Formatid { get; set; }

        public virtual Films Film { get; set; }
        public virtual VideoFormat Format { get; set; }
    }
}
