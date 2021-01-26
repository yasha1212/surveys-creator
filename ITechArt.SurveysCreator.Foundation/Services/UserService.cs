using System;
using System.Collections.Generic;
using System.Linq;
using ITechArt.SurveysCreator.DAL;
using ITechArt.SurveysCreator.DAL.Models;

namespace ITechArt.SurveysCreator.Foundation.Services
{
    public class UserService : IUserService
    {
        private readonly SurveysCreatorDbContext _context;

        public UserService(SurveysCreatorDbContext context)
        {
            _context = context;
        }

        public void Add(User item)
        {
            if (!_context.Users.Any(u => u.Id == item.Id))
            {
                _context.Users.Add(item);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Invalid add operation, because current item exists!");
            }
        }

        public User Details(int id)
        {
            return _context.Users.Find(id);
        }

        public IEnumerable<User> Get()
        {
            return _context.Users.AsEnumerable();
        }

        public void Edit(int id, User item)
        {
            var user = _context.Users.Find(id);

            if (user != null)
            {
                user.Age = item.Age;
                user.Email = item.Email;
                user.Login = item.Login;
                user.FirstName = item.FirstName;
                user.SecondName = item.SecondName;
                
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Invalid edit operation, because current item does not exist!");
            }
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);

            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Invalid delete operation, because current item does not exist!");
            }
        }
    }
}
