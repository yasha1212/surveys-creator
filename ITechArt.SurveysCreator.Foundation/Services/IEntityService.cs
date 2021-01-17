using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ITechArt.SurveysCreator.DAL.Models;

namespace ITechArt.SurveysCreator.Foundation.Services
{
    public interface IEntityService
    {
        User Details(int id);

        IEnumerable<User> Get();

        void Add(User item);

        void Edit(int id, User item);
        
        void Delete(int id);
    }
}
