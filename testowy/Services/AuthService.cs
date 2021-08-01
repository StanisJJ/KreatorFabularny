using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using testowy.DAL;
using testowy.Entities;
using testowy.Helpers;
using testowy.Models;
using testowy.Models.User;

namespace testowy.Services
{
    public interface IAuthService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        bool Register(UserRequest userRequest);
        User GetById(int id);
    }

    public class AuthService : IAuthService
    {

        private readonly AppSettings _appSettings;
        private readonly MyDbContext _context;

        public AuthService(IOptions<AppSettings> appSettings, MyDbContext myDbContext)
        {
            _appSettings = appSettings.Value;
            _context = myDbContext;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            //BCrypt.Net.BCrypt.Verify(model.Password, x.Password)
            var user = _context.Users.SingleOrDefault(x => x.Login == model.Login);
            
            // return null if user not found
            if (user == null) return null;

            if (!BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.UserId == id);
        }

        // helper methods

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public bool Register(UserRequest userRequest)
        {
            if (_context.Users.Any(x => x.Email.Equals(userRequest.Email) || x.Login.Equals(userRequest.Login))) return false;
            if (!userRequest.Password.Equals(userRequest.ConfirmPassword)) return false;
            _context.Users.Add(new User(userRequest));

            return _context.SaveChanges() > 0;
        }

    }
}
