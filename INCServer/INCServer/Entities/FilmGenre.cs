using System;
using System.Collections.Generic;

namespace INCServer
{
    public partial class FilmGenre
    {
        public int Filmid { get; set; }
        public short Genreid { get; set; }

        public virtual Films Film { get; set; }
        public virtual Genres Genre { get; set; }
    }
}
