using INCServer.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INCWebServer.Services
{
    public class SubsidiaryService
    {
        incContext db;
        public SubsidiaryService(incContext db)
        {
            this.db = db;
        }
        public async Task<List<string>> GetUsefullGenres()
        {
            return await (from g in db.Genres
                   where (from gs in db.FilmGenre
                          where gs.Genreid == g.Id
                          select gs).Count() > 0
                   select g.Name).ToListAsync();

        }
        public async Task<List<string>> GetAllGenres()
        {
            return await (from g in db.Genres
                          select g.Name).ToListAsync();

        }
        public string GetPersonRight(int idRight)
        {
            return db.UserRights.FirstOrDefault(r => r.Id == idRight)?.Name;
        }
    }
}
