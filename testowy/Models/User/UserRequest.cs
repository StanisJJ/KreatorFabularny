using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testowy.Models.User
{
    public class UserRequest
    {
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
