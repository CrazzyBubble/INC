using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace INCWebServer
{
    public class AuthOptions
    {
        public const string ISSUER = "https://localhost:5001/"; // издатель токена
        public const string AUDIENCE = "file:///C:/1.6_semester/Technology_of_program-2/INC/backend/INCWebServer/wwwroot/"; // потребитель токена
        const string KEY = "secretkey_INCCorporation!";   // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
