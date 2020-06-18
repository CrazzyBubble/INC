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

        [JsonProperty("logo")]
        public string ImageSrc { set; get; }

        [JsonProperty("videos")]
        public List<FilmResources> VideoSrc { set; get; }

        [JsonProperty("solo_properties")]
        public Dictionary<string, string> SoloOptions { set; get; }

        [JsonProperty("list_properties")]
        public Dictionary<string, List<string>> ListOptions { set; get; }

        [JsonProperty("isliked")]
        public bool IsLiked { set; get; }

        [JsonProperty("rating")]
        public short? Rating { set; get; }
        public FilmInfoFull(Films film, List<FilmResources> src, bool isliked, short? rating,
             List<string> genres,List<string> studios)
        {
            Id = film.Id;
            Name = film.Name;
            ImageSrc = film.ImageSrc;
            VideoSrc = src;
            IsLiked = isliked;
            Rating = rating;
            SoloOptions = new Dictionary<string, string>();
            ListOptions = new Dictionary<string, List<string>>();
            AddSoloOptions("Country", film.Country);
            AddSoloOptions("Release", film.Date.Value.ToString("d"));
            AddSoloOptions("Categories", film.Categories);
            AddSoloOptions("Description", film.Description);
            AddListOptions("Genres", genres);
            AddListOptions("Directors", film.Directors.ToList());
            AddListOptions("Studios", studios);
        }
        public void AddSoloOptions<T>(string key, T value)
        {
            if (value != null)
                SoloOptions.Add(key, value.ToString());
        }

        public void AddListOptions<T>(string key, List<T> value)
        {
            if (value != null && value.Count>0)
            {
                List<string> newv = new List<string>();
                for (int i = 0; i < value.Count; ++i)
                    newv.Add(value[i].ToString());
                ListOptions.Add(key, newv);
            }
        }
    }
}
