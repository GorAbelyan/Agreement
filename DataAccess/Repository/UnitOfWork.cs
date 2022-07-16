using DataAccess.Data;
using DataAccess.Repository.IRepository;
using System;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Agreement = new AgreementRepository(_db);
            Product = new ProductRepository(_db);
            ProductGroup = new ProductGroupRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
        }

        public IAgreementRepository Agreement { get; private set; }
        public IProductRepository Product { get; private set; }
        public IProductGroupRepository ProductGroup { get; private set; }
        public IApplicationUserRepository ApplicationUser{ get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }

        public async Task SaveAsync()
        {
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {

            }
        }
    }
}
