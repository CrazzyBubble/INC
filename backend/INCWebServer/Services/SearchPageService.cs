using INCServer.Context;
using INCWebServer.Sources;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INCWebServer.Services
{
    public class SearchPageService
    {
        incContext db;
        public SearchPageService(incContext db)
        {
            this.db = db;
        }
        public async Task<List<Genre_FilmsPromo>> GetGenres_Films(string name)
        {
            List<Genre_FilmsPromo> gfilms = new List<Genre_FilmsPromo>();
            foreach(var genre in db.Genres)
            {
                var films = (await GetFilmsByGenre(genre.Name)).ToList();
                if (films.Count > 0)
                {
                    var gf = new Genre_FilmsPromo();
                    gf.GenreName = genre.Name;
                    foreach (var fil in films)
                        if (!gf.addFilm(fil))
                            break;
                    gfilms.Add(new Genre_FilmsPromo());
                }
                
            }
            return gfilms;
        }
        public async Task<List<FilmInfoPromo>> GetFilmsByName(string name)
        {
            var films = from film in db.Films 
                        where film.Name.ToLower().Contains(name.ToLower().Trim())
                        join photo in db.Photos on film.Id equals photo.Filmid
                        where photo.Istitul == true
                        select new FilmInfoPromo(film.Id, film.Name, photo.Src);
            return await films.ToListAsync();
        }
        public async Task<List<FilmInfoPromo>> GetFilmsByGenre(string genre)
        {
            var films = from fg in db.FilmGenre
                        where fg.Genre.Name == genre
                        join photo in db.Photos on fg.Film.Id equals photo.Filmid
                        where photo.Istitul == true
                        select new FilmInfoPromo(fg.Film.Id, fg.Film.Name, photo.Src);
            return await films.ToListAsync();
        }
        public async Task<List<FilmInfoPromo>> GetSortedFilmsByPopularity(string name)
        {
            var films = from film in db.Films
                        where film.Name.ToLower().Contains(name.ToLower().Trim())
                        join photo in db.Photos on film.Id equals photo.Filmid
                        where photo.Istitul == true
                        select new FilmInfoPromo(film.Id, film.Name, photo.Src);
            return await films.ToListAsync();
        }
        public async Task<List<FilmInfoPromo>> GetSortedFilmsByRelease(string name)
        {
            var films = from film in db.Films
                        where film.Name.ToLower().Contains(name.ToLower().Trim())
                        join photo in db.Photos on film.Id equals photo.Filmid
                        where photo.Istitul == true
                        select new FilmInfoPromo(film.Id, film.Name, photo.Src);
            return await films.ToListAsync();
        }
        public async Task<List<FilmInfoPromo>> GetSortedFilmsByRating(string name)
        {
            var films = from film in db.Films
                        where film.Name.ToLower().Contains(name.ToLower().Trim())
                        join photo in db.Photos on film.Id equals photo.Filmid
                        where photo.Istitul == true
                        select new FilmInfoPromo(film.Id, film.Name, photo.Src);
            return await films.ToListAsync();
        }
    }
}
