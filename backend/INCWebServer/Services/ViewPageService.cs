using INCServer;
using INCServer.Context;
using INCWebServer.Sources;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace INCWebServer.Services
{
    public class ViewPageService:IDisposable
    {
        incContext db;

        public ViewPageService(incContext db)
        {
            this.db = db;
        }

        public async Task<FilmInfoFull> GetFilmById(int id, int userID=0)
        {
            var film = from f in db.Films
                       where f.Id == id
                       select new FilmInfoFull(
                           f,
                           (from src in db.FilmResources
                            where src.Filmid == id
                            select src).ToList(),
                           (from genre in db.FilmGenre
                            where genre.Filmid == id
                            select genre.Genre.Name).ToList(),
                           (from studio in db.FilmStudio
                            where studio.Filmid == id
                            select studio.Studio.Name).ToList(),
                           (from w in f.Watched
                            where w.Userid == userID
                            select w.Islike).FirstOr(false),
                           (from w in f.Watched
                            where w.Userid == userID
                            select w.Rating).FirstOr(null)
                            );
            return await film.FirstOrDefaultAsync();
        }

        public async Task<FilmInfoFull> GetLastFilm(int userID)
        {
            var film = from lw in db.LastWatch
                       where lw.Userid == userID
                       select new FilmInfoFull(
                           lw.Watched.Film,
                           (from src in db.FilmResources
                            where src.Filmid == lw.Watched.Film.Id
                            select src).ToList(),
                           (from genre in db.FilmGenre
                            where genre.Filmid == lw.Watched.Film.Id
                            select genre.Genre.Name).ToList(),
                           (from studio in db.FilmStudio
                            where studio.Filmid == lw.Watched.Film.Id
                            select studio.Studio.Name).ToList(),
                           lw.Watched.Islike,
                           lw.Watched.Rating
                        );
            return await film.FirstOrDefaultAsync();
        }

        public bool SetWatched(int userid, int filmid)
        {
            var f = (from w1 in db.Watched
                     where w1.Filmid == filmid && w1.Userid == userid
                     select w1).Count();
            if (f > 0)
                return false;
            Watched w = new Watched { Userid = userid, Filmid = filmid };
            db.Watched.Add(w);
            db.SaveChanges();
            return true;
        }

        public bool SetLike(int userid, int filmid, bool islike)
        {
            var w = (from w1 in db.Watched
                     where w1.Filmid == filmid && w1.Userid == userid
                     select w1).FirstOr(null);
            if (w is null)
                return false;
            w.Islike = islike;
            db.SaveChanges();
            return true;
        }

        public bool SetRating(int userid, int filmid, short rating)
        {
            if (rating > 10 || rating < 1)
                return false;
            var w = (from w1 in db.Watched
                     where w1.Filmid == filmid && w1.Userid == userid
                     select w1).FirstOr(null);
            if (w is null)
                return false;
            w.Rating = rating;
            db.SaveChanges();
            return true;
        }

        public void Dispose()
        {
        }
    }
}
