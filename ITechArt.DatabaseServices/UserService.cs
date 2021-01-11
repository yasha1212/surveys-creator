using ITechArt.DatabaseModels;
using ITechArt.DatabaseModels.Models;
using System;

namespace ITechArt.DatabaseServices
{
    public class UserService : IDatabaseEntityService
    {
        private readonly DatabaseContext _context;

        public UserService(DatabaseContext context)
        {
            _context = context;
        }

        public void Add(object item)
        {
            if (item is User user)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
        }

        public object Details(int id)
        {
            return _context.Users.Find(id);
        }

        public void Edit(object item)
        {
            if (item is User user)
            {
                _context.Users.Update(user);
                _context.SaveChanges();
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
        }
    }
}
