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
        public Genre_FilmsPromo(string genre, List<FilmInfoPromo> films)
        {
            GenreName = genre;
            this.films = films;
        }
        public bool addFilm(FilmInfoPromo film)
        {
            if (films.Count < 5)
            {
                films.Add(film);
                return true;
            }
            return false;
        }
    }
}
