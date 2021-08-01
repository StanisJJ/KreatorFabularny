using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testowy.Models.User
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }

        public UserResponse(User user)
        {
            UserId = user.UserId;
            Email = user.Email;
            Login = user.Login;
            Role = user.Role;
        }

    }
}
