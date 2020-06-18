using INCServer.Context;
using System;
using System.Security.Cryptography;
using System.Text;

namespace INCServer
{
    class Program
    {
        static void Main(string[] args)
        {
            string value = "12345678";
            /*using (incContext db = new incContext())
            {
                var users = db.Users.ToList();
                Console.WriteLine("List of users");
                foreach(User u in users)
                {
                    Console.WriteLine($"{u.Id}\t{u.Email}\t{u.Password}\t{u.Rightid}");
                }
            }*/
        }
    }
}
