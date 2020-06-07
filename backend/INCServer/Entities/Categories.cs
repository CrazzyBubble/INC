using System;
using System.Collections.Generic;

namespace INCServer
{
    public partial class Categories
    {
        public Categories()
        {
            Films = new HashSet<Films>();
        }

        public short Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Films> Films { get; set; }
    }
}
