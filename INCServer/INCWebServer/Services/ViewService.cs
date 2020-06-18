using INCServer;
using INCServer.Context;
using INCWebServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace INCWebServer.Services
{
    public class ViewService :IDisposable
    {
        incContext db;

        public ViewService(incContext db)
        {
            this.db = db;
        }

        public async Task<FilmInfoFull> GetFilmById(int id, int userID=1)
        {
            if (userID == 0)
                return null;
            var film = from f in db.Films
                       where f.Id == id
                       select new FilmInfoFull(
                           f,
                           (from src in db.FilmResources
                            where src.Filmid == id
                            select src).ToList(),
                           (from w in f.Watched
                            where w.Userid == userID
                            select w.Islike).FirstOrDefault(),
                           (from w in f.Watched
                            where w.Userid == userID
                            select w.Rating).FirstOrDefault(),
                           (from genre in db.FilmGenre
                            where genre.Filmid == id
                            select genre.Genre.Name).ToList(),
                           (from studio in db.FilmStudio
                            where studio.Filmid == id
                            select studio.Studio.Name).ToList()
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
                           lw.Watched.Islike,
                           lw.Watched.Rating,
                           (from genre in db.FilmGenre
                            where genre.Filmid == lw.Watched.Film.Id
                            select genre.Genre.Name).ToList(),
                           (from studio in db.FilmStudio
                            where studio.Filmid == lw.Watched.Film.Id
                            select studio.Studio.Name).ToList()
                        );
            return await film.FirstOrDefaultAsync();
        }

        public bool SetWatched(int userid, int filmid)
        {
            /*var cw = (from w1 in db.Watched
                     where w1.Filmid == filmid && w1.Userid == userid
                     select w1).Count();
            if (cw > 0)
                return false;*/
            Watched w = new Watched { Userid = userid, Filmid = filmid };
            try
            {
                db.Watched.Add(w);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool SetWatchTrue(int userid, int filmid)
        {
            var w = (from w1 in db.Watched
                     where w1.Filmid == filmid && w1.Userid == userid
                     select w1).FirstOrDefault(null);
            if (w is null)
                return false;
            if (!w.Iswatched)
            {
                w.Iswatched = true;
                db.SaveChanges();
            }
            return true;
        }
        public bool SetTimeStop(int userid, int filmid, TimeSpan timestop)
        {
            var w = (from w1 in db.Watched
                     where w1.Filmid == filmid && w1.Userid == userid
                     select w1).FirstOrDefault(null);
            if (w is null)
                return false;
            w.Timestop = timestop;
            db.SaveChanges();
            return true;
        }

        public bool SetLike(int userid, int filmid, bool islike)
        {
            var w = (from w1 in db.Watched
                     where w1.Filmid == filmid && w1.Userid == userid
                     select w1).FirstOrDefault(null);
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
                     select w1).FirstOrDefault(null);
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
