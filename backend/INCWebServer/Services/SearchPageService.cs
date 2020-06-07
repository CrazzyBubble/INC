using INCServer.Context;
using INCWebServer.Sources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INCWebServer.Services
{
    public class SearchPageService: IDisposable
    {
        incContext db;
        public SearchPageService()
        {
            db = new incContext();
        }
        public async Task<List<FilmInfoPromo>> GetFilms(string name)
        {
            var films = from film in db.Films 
                        where film.Name.ToLower().Contains(name.ToLower().Trim())
                        join photo in db.Photos on film.Id equals photo.Filmid
                        where photo.Istitul == true
                        select new FilmInfoPromo(film.Id, film.Name, photo.Src);
            return await films.ToListAsync();
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
