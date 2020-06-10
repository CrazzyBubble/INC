using INCServer.Context;
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
        public LoginPageService()
        {
            db = new incContext();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
