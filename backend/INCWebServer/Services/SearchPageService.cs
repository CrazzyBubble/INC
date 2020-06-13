using INCServer.Context;
using INCWebServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INCWebServer.Services
{
    public class SearchPageService:IDisposable
    {
        incContext db;

        public SearchPageService(incContext db)
        {
            this.db = db;
        }

        public async Task<List<Genre_FilmsPromo>> GetGenres_Films()
        {
            var gfilms =    from genre in db.Genres
                            where 
                            (from fg in db.FilmGenre
                            where fg.Genreid == genre.Id
                            select fg).Count() > 0
                            orderby genre.Name
                            select new Genre_FilmsPromo(
                                genre.Name, 
                                (from fg in db.FilmGenre
                                 where fg.Genreid == genre.Id
                                 orderby fg.Film.Date descending
                                 select new FilmInfoPromo(fg.Film.Id, fg.Film.Name, fg.Film.ImageSrc)
                                ).ToList()
                            );
            return await gfilms.ToListAsync();
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
