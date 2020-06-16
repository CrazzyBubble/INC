using INCServer;
using INCServer.Context;
using INCWebServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace INCWebServer.Services
{
    public class LoginService:IDisposable
    {
        incContext db;

        public LoginService(incContext db)
        {
            this.db = db;
        }

        public async Task<User> SignIn(LoginModel model)
        {
            /*var user = (from u in db.Users
                       where u.Email == model.Email && u.Password == model.Password.GetHashCode().ToString()
                       join ui in db.UserInfo on u.Id equals ui.Userid
                       select new UserFullInfo(u, ui));
            return await user.FirstOrDefaultAsync();*/
            return await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email && 
            u.Password == model.Password.GetHashCode().ToString());
        }

        public bool Registration(RegistrationModel model)
        {
            User user = db.Users.FirstOrDefault(u => u.Email == model.Email);
            if(user == null)
            {
                user = new User { Email = model.Email, Password = model.GetHashCode().ToString() };
                db.Users.Add(user);
                db.SaveChanges();
                db.UserInfo.Add(new UserInfo { 
                    Userid = user.Id,
                    Firstname = model.Firstname, 
                    Lastname = model.Lastname, 
                    Birthday = model.Birthday});
                db.SaveChanges();
                return true;
            }
            return false;
        }


        public void Dispose()
        {
            db.Dispose();
        }
    }
}
