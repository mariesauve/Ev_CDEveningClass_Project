using MerchProjectDataAccess.Data;
using MerchProjectDataAccess.Repository.IRepository;
using MerchProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchProjectDataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category category)
        {
            var objFromDb = _db.Categories.Find(category.Id);

            if (objFromDb == null)
            {
                throw new ArgumentException("Category not found", nameof(category));
            }

            _db.Entry(objFromDb).CurrentValues.SetValues(category);

            _db.SaveChanges();
        }

        public Category Get(object id)
        {
            return _db.Categories.Find(id);
        }
    }
}
