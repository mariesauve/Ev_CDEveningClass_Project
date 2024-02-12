using MerchProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchProjectDataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category Get(object value);
        void Update(Category category);
    }
}
