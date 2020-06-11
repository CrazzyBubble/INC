using INCServer;
using INCServer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public bool Enter(string email, string password)
        {
            var user = (from u in db.Users
                       where u.Email == email.Trim() && u.Password == password.GetHashCode().ToString()
                       select u).ToList();
            if (user.Count > 0)
                return true;
            return false;
            /*return Ok(JsonConvert.SerializeObject(parkingService.GetCapacity()));*/
        }

        public void PutNewUser(string email, string password, string firstname, string lastname, DateTime birthday)
        {
            User newuser = new User 
            { 
                Email = email, 
                Password = password.GetHashCode().ToString()
            };
            db.Users.Add(newuser);
            db.SaveChanges();
            UserInfo newuserinfo = new UserInfo
            {
                Firstname = firstname,
                Lastname = lastname,
                Birthday = birthday,
                Userid = newuser.Id
            };
            db.UserInfo.Add(newuserinfo);
            db.SaveChanges();
        }


        public void Dispose()
        {
            db.Dispose();
        }
    }
}
