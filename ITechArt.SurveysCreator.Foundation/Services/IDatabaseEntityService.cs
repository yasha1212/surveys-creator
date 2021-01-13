using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ITechArt.SurveysCreator.Foundation.Services
{
    interface IDatabaseEntityService
    {
        void Add(object item);
        object Details(int id);
        void Edit(object item);
        void Delete(int id);
    }
}
