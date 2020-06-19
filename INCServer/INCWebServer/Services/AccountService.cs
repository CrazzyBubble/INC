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
    public class AccountService:IDisposable
    {
        incContext db;

        public AccountService(incContext db)
        {
            this.db = db;
        }

        public async Task<User> SignIn(LoginModel model)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user != null && AuthOptions.VerifyHashedPassword(user.Password, model.Password))
                return user;
            return null;
        }

        public bool Registration(RegistrationModel model)
        {
            User user = db.Users.FirstOrDefault(u => u.Email == model.Email);
            if(user == null)
            {
                user = new User { 
                    Email = model.Email, 
                    Password = AuthOptions.GetHashPassword(model.Password),
                    Info = new UserInfo
                    {
                        Firstname = model.Firstname,
                        Lastname = model.Lastname,
                        Birthday = model.Birthday
                    }
                };
                db.Add(user);
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
