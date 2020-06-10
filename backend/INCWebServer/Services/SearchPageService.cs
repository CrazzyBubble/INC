using INCServer;
using INCServer.Context;
using INCWebServer.Sources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace INCWebServer.Services
{
    public class SearchPageService:IDisposable
    {
        incContext db;
        public SearchPageService(incContext db)
        {
            //db = new incContext();
            this.db = db;
        }
        public async Task<List<Genre_FilmsPromo>> GetGenres_Films()
        {
            var gfilms =    from genre in db.Genres
                            where 
                            (from fg in db.FilmGenre
                            where fg.Genreid == genre.Id
                            select fg).Count() > 0
                            select new Genre_FilmsPromo(genre.Name, (
                            from fg in db.FilmGenre
                            where fg.Genreid == genre.Id
                            join film in db.Films on fg.Filmid equals film.Id
                            select new FilmInfoPromo(film.Id, film.Name, film.ImageSrc)
                            ).ToList());
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
            var films = /*from fg in db.FilmGenre
                        join f in db.Films on fg.Filmid equals f.Id
                        join g in db.Genres on fg.Genreid equals g.Id
                        where g.Name.Equals(genre)
                        select new FilmInfoPromo(f.Id, f.Name, f.ImageSrc);*/
                        from fg in db.FilmGenre
                         where fg.Genre.Name.Equals(genre)
                         select new FilmInfoPromo(fg.Film.Id, fg.Film.Name, fg.Film.ImageSrc);
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
