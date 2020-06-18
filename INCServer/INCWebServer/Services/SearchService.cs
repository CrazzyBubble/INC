using INCServer.Context;
using INCWebServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INCWebServer.Services
{
    public class SearchService: IDisposable
    {
        incContext db;
        public int FilmsCapacity { set; get; } = 5;

        public SearchService(incContext db)
        {
            this.db = db;
        }

        public async Task<Dictionary<string, List<FilmInfoPromo>>> GetGenres_Films()
        {
            Dictionary<string, List<FilmInfoPromo>> res = new Dictionary<string, List<FilmInfoPromo>>();
            foreach(var genre in db.Genres.OrderBy(g => g.Name).ToList())
            {
                var films = await (from fg in db.FilmGenre
                            where fg.Genreid == genre.Id
                            orderby fg.Film.Date descending
                            select new FilmInfoPromo(fg.Film.Id, fg.Film.Name, fg.Film.ImageSrc)).ToListAsync();
                if (films.Count() > 0)
                {
                    if (films.Count > FilmsCapacity)
                        res.Add(genre.Name, films.GetRange(0, FilmsCapacity));
                    else
                        res.Add(genre.Name, films);
                }
            }
            return res;
        }

        public async Task<List<FilmInfoPromo>> GetFilmsByName(string name)
        {
            var films = from film in db.Films 
                        where film.Name.ToLower().Contains(name.ToLower().Trim())
                        select new FilmInfoPromo(film.Id, film.Name, film.ImageSrc);
            return await films.ToListAsync();
        }

        public async Task<List<FilmInfoPromo>> GetFilmsByGenre(string genre)
        {
            var films =  from fg in db.FilmGenre
                         where fg.Genre.Name.Equals(genre)
                         orderby fg.Film.Date descending
                         select new FilmInfoPromo(fg.Film.Id, fg.Film.Name, fg.Film.ImageSrc);
            return await films.ToListAsync();
        }

        public async Task<List<FilmInfoPromo>> GetFilmsByGenreId(int id)
        {
            var films = from fg in db.FilmGenre
                        where fg.Genreid == id
                        orderby fg.Film.Date descending
                        select new FilmInfoPromo(fg.Film.Id, fg.Film.Name, fg.Film.ImageSrc);
            return await films.ToListAsync();
        }

        public async Task<List<FilmInfoPromo>> GetFilmsByStudio(string studio)
        {
            var films = from fs in db.FilmStudio
                        where fs.Studio.Name.Equals(studio)
                        orderby fs.Film.Date descending
                        select new FilmInfoPromo(fs.Film.Id, fs.Film.Name, fs.Film.ImageSrc);
            return await films.ToListAsync();
        }

        public async Task<List<FilmInfoPromo>> GetFilmsByStudioId(int id)
        {
            var films = from fs in db.FilmStudio
                        where fs.Studioid == id
                        orderby fs.Film.Date descending
                        select new FilmInfoPromo(fs.Film.Id, fs.Film.Name, fs.Film.ImageSrc);
            return await films.ToListAsync();
        }

        public async Task<List<FilmInfoPromo>> GetSortedFilmsByPopularity(bool isAscending = true)
        {
            var films = from film in db.Films
                        orderby (
                        from watched in db.Watched
                        where watched.Filmid == film.Id
                        select watched
                        ).Count()
                        select new FilmInfoPromo(film.Id, film.Name, film.ImageSrc);
            if (!isAscending)
                films.Reverse();
            return await films.ToListAsync();
        }

        public async Task<List<FilmInfoPromo>> GetSortedFilmsByRelease(bool isAscending = true)
        {
            var films = from film in db.Films
                        orderby film.Date
                        select new FilmInfoPromo(film.Id, film.Name, film.ImageSrc);
            if (!isAscending)
                films.Reverse();
            return await films.ToListAsync();
        }

        public async Task<List<FilmInfoPromo>> GetSortedFilmsByRating(bool isAscending=true)
        {
            var films = from film in db.Films
                        orderby (
                        from watched in db.Watched
                        where watched.Filmid == film.Id && watched.Rating != null
                        select watched
                        ).Average(n => n.Rating)
                        select new FilmInfoPromo(film.Id, film.Name, film.ImageSrc);
            if (!isAscending)
                films.Reverse();
            return await films.ToListAsync();
        }

        public void Dispose()
        {
        }
    }
}
