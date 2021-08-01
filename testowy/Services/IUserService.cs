using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testowy.DAL;
using testowy.Models;
using testowy.Models.User;

namespace testowy.Repository
{
    public interface IUserService : CrudService<User>
    {
        UserResponse GetByLogin(string login); 
    }

    public class UserManager : IUserService
    {
        readonly MyDbContext _myDbContext;

        public UserManager(MyDbContext context)
        {
            _myDbContext = context;
        }

        public void Add(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public User Get(long id)
        {
            return _myDbContext.Users
              .FirstOrDefault(e => e.UserId == id);
        }


        public IEnumerable<User> GetAll()
        {
            return _myDbContext.Users.ToList();
        }

        public UserResponse GetByLogin(string login)
        {
            var userResponse = _myDbContext.Users.FirstOrDefault(e => e.Login == login);
            if (userResponse == null)
                return null;
            return new UserResponse(userResponse);  
        }

        public void Update(User dbEntity, User entity)
        {
            throw new NotImplementedException();
        }
    }
}
