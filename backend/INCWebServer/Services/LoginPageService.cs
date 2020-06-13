using INCServer;
using INCServer.Context;
using INCWebServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INCWebServer.Services
{
    public class LoginPageService:IDisposable
    {
        incContext db;

        public LoginPageService(incContext db)
        {
            this.db = db;
        }

        public async Task<UserFullInfo> SignIn(string email, string password)
        {
            var user = (from u in db.Users
                       where u.Email == email.Trim() && u.Password == password.GetHashCode().ToString()
                       join ui in db.UserInfo on u.Id equals ui.Userid
                       select new UserFullInfo(u, ui));
            return await user.FirstOrDefaultAsync();
        }

        public bool Registration(string email, string password, string firstname, string lastname, DateTime birthday)
        {
            User newuser = new User 
            { 
                Email = email, 
                Password = password.GetHashCode().ToString()
            };
            try
            {
                db.Users.Add(newuser);
                db.SaveChanges();
            }
            catch(Exception)
            {
                return false;
            }
            UserInfo newuserinfo = new UserInfo
            {
                Firstname = firstname,
                Lastname = lastname,
                Birthday = birthday,
                Userid = newuser.Id
            };
            try
            {
                db.UserInfo.Add(newuserinfo);
                db.SaveChanges();
            }
            catch (Exception)
            {
            }
            return true;
        }


        public void Dispose()
        {
            db.Dispose();
        }
    }
}
