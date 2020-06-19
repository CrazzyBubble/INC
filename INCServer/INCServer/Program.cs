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
            string value = "admin2password";
            Console.WriteLine(GetHashPassword(value));
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
        public static string GetHashPassword(string pass)
        {
            byte[] salt;
            byte[] buffer2;
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(pass, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }
    }
}
