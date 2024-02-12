using MerchProjectDataAccess.Data;
using MerchProjectDataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchProjectDataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            SP_Call = new SP_Call(_db);
        }

        public ICategoryRepository Category { get; private set; }
        public ISP_Call SP_Call { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
