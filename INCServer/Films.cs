using System;
using System.Collections.Generic;

namespace INCServer
{
    public partial class Films
    {
        public Films()
        {
            Watched = new HashSet<Watched>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public short? Categoriesid { get; set; }
        public string[] Directors { get; set; }
        public short? Countryid { get; set; }
        public string[] Tags { get; set; }
        public string Description { get; set; }

        public virtual Categories Categories { get; set; }
        public virtual Countries Country { get; set; }
        public virtual ICollection<Watched> Watched { get; set; }
    }
}
