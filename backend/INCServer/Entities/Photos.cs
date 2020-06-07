using System;
using System.Collections.Generic;

namespace INCServer
{
    public partial class Photos
    {
        public int? Filmid { get; set; }
        public string Src { get; set; }
        public bool? Istitul { get; set; }

        public virtual Films Film { get; set; }
    }
}
