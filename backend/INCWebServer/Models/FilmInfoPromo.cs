using Newtonsoft.Json;

namespace INCWebServer.Models
{
    public class FilmInfoPromo
    {
        [JsonProperty("id")]
        public int Id { set; get; }

        [JsonProperty("name")]
        public string Name { set; get; }

        [JsonProperty("image")]
        public string Image { set; get; }
        public FilmInfoPromo() {
            Id = 0;
            Name = "";
            Image = "";
        }
        public FilmInfoPromo(int id, string name, string image)
        {
            Id = id;
            Name = name;
            Image = image;
        }
    }
}
