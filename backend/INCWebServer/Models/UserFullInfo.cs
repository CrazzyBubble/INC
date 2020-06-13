using INCServer;
using Newtonsoft.Json;

namespace INCWebServer.Models
{
    public class UserFullInfo
    {
        [JsonProperty("user_login")]
        public User UserLoginInfo { set; get; }
        [JsonProperty("user_info")]
        public UserInfo UserMainInfo { set; get; }

        public UserFullInfo(User u, UserInfo ui)
        {
            UserLoginInfo = u;
            UserMainInfo = ui;
        }
    }
}
