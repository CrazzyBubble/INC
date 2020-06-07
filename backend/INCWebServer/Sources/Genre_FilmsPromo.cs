using Newtonsoft.Json;
using System.Collections.Generic;

namespace INCWebServer.Sources
{
    public class Genre_FilmsPromo
    {
        [JsonProperty("genre")]
        public string GenreName { set; get; }
        [JsonIgnore]
        private List<FilmInfoPromo> films;
        [JsonProperty("films")]
        public FilmInfoPromo[] Films { get { return films.ToArray(); } }
        public Genre_FilmsPromo()
        {
            films = new List<FilmInfoPromo>();
        }
        public void addFilm(FilmInfoPromo film)
        {
            if (films.Count < 5)
                films.Add(film);
        }
    }
}
