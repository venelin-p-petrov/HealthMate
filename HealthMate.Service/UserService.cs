using HealthMate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMate.Service
{
    public class UserService
    {
        private HealthMateContext db;

        public UserService()
        {
            db = new HealthMateContext();
        }

        public User GetUserByUsername(string username, string password)
        {
            var user = db.Users.Where(u => u.Username == username && u.Password == password).FirstOrDefault();
            return user;
        }

        public User GetUserByUsername(string username)
        {
            var user = db.Users.Where(u => u.Username == username).FirstOrDefault();
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public int RegisterUser(User user)
        {
            User newUser = new User()
            {
                Username = user.Username,
                Password = user.Password,
                Email = user.Email
            };

            db.Users.Add(newUser);
            db.SaveChanges();
            return newUser.ID;
        }
    }
}
