using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace INCServer
{
    public partial class Films
    {
        public Films()
        {
            Watched = new HashSet<Watched>();
        }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("release")]
        public DateTime? Date { get; set; }
        public short? Categoriesid { get; set; }
        [JsonProperty("directors")]
        public string[] Directors { get; set; }
        public short? Countryid { get; set; }
        [JsonProperty("tags")]
        public string[] Tags { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("imagesrc")]
        public string ImageSrc { get; set; }

        [JsonProperty("categories")]
        public virtual Categories Categories { get; set; }
        [JsonProperty("country")]
        public virtual Countries Country { get; set; }
        public virtual ICollection<Watched> Watched { get; set; }
    }
}
