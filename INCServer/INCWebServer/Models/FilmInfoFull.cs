using INCServer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace INCWebServer.Models
{
    public class FilmInfoFull
    {
        [JsonProperty("id")]
        public int Id { set; get; }
        [JsonProperty("name")]
        public string Name { set; get; }

        [JsonProperty("description")]
        public string Description { set; get; }

        [JsonProperty("logo")]
        public string ImageSrc { set; get; }

        [JsonProperty("videos")]
        public List<FilmResources> VideoSrc { set; get; }

        [JsonProperty("solo_properties")]
        public Dictionary<string, object> SoloOptions { set; get; }

        [JsonProperty("list_properties")]
        public Dictionary<string, object> ListOptions { set; get; }

        [JsonProperty("isliked")]
        public bool IsLiked { set; get; }

        [JsonProperty("rating")]
        public short? Rating { set; get; }
        public FilmInfoFull(Films film, List<FilmResources> src, bool isliked, short? rating,
             List<string> genres,List<string> studios)
        {
            Id = film.Id;
            Name = film.Name;
            Description = film.Description??"";
            ImageSrc = film.ImageSrc;
            VideoSrc = src;
            IsLiked = isliked;
            Rating = rating;
            SoloOptions = new Dictionary<string, object>();
            ListOptions = new Dictionary<string, object>();
            AddSoloOptions("Country", film.Country);
            AddSoloOptions("Release", film.Date);
            AddSoloOptions("Categories", film.Categories);
            AddListOptions("Genres", genres);
            AddListOptions("Directors", film.Directors);
            AddListOptions("Studios", studios);
        }
        public void AddSoloOptions<T>(string key, T value)
        {
            if (value != null)
                SoloOptions.Add(key, value);
        }

        public void AddListOptions<T>(string key, T value)
        {
            if (value != null)
                ListOptions.Add(key, value);
        }
    }
}
