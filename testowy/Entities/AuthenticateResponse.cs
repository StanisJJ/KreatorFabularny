using testowy.Entities;
using testowy.Models;
using testowy.Models.User;

namespace testowy.Entities
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }


        public AuthenticateResponse(User user, string token)
        {
            Id = user.UserId;
            Login = user.Login;
            Token = token;
            Role = user.Role;
        }
    }
}