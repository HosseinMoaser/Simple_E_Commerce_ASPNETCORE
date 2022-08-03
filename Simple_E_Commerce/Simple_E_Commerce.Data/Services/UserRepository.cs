using Simple_E_Commerce.Data.Context;
using Simple_E_Commerce.Data.Models;
using Simple_E_Commerce.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_E_Commerce.Data.Services
{
    public class UserRepository : IUserRepository
    {
        private SimpleEcommerceDbContext _simpleEcommerceDbContext;

        public UserRepository(SimpleEcommerceDbContext simpleEcommerceDbContext)
        {
            _simpleEcommerceDbContext = simpleEcommerceDbContext;
        }

        public void AddUser(User user)
        {
            _simpleEcommerceDbContext.Add(user);
            _simpleEcommerceDbContext.SaveChanges();
        }

        public User GetUserForLogin(string Email, string password)
        {
            return _simpleEcommerceDbContext.Users
                .SingleOrDefault(u => u.Email.ToLower() == Email && u.Password == password);
        }

        public bool IsExistUserByEmail(string Email)
        {
            return _simpleEcommerceDbContext.Users.Any(u => u.Email == Email);
        }

        public bool IsExistUserByUserName(string UserName)
        {
            return _simpleEcommerceDbContext.Users.Any(u => u.UserName == UserName);

        }
    }
}
