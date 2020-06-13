using INCServer;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace INCWebServer.Models
{
    public class FilmInfoFull
    {
        [JsonProperty("film")]
        public Films Film { set; get; }

        [JsonProperty("videos")]
        public List<FilmResources> VideoSrc { set; get; }

        [JsonProperty("genres")]
        public List<string> Genres { set; get; }

        [JsonProperty("studios")]
        public List<string> Studios { set; get; }

        [JsonProperty("isliked")]
        public bool IsLiked { set; get; }

        [JsonProperty("rating")]
        public short? Rating { set; get; }
        public FilmInfoFull(Films film, List<FilmResources> src, List<string> genres, 
            List<string> studios, bool isliked, short? rating)
        {
            this.Film = film;
            this.VideoSrc = src;
            this.Genres = genres;
            this.Studios = studios;
            this.IsLiked = isliked;
            this.Rating = rating;
        }
    }
}
