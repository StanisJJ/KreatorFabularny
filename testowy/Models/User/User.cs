using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace testowy.Models.User
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public User(UserRequest userRequest)
        {
            Email = userRequest.Email;
            Login = userRequest.Login;
            Password = BCrypt.Net.BCrypt.HashPassword(userRequest.Password);
            Role = "User";
        }
        public User()
        {

        }
        
    }
}
