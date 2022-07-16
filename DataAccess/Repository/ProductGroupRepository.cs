using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models;

namespace DataAccess.Repository
{
    public class ProductGroupRepository : Repository<ProductGroup>, IProductGroupRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductGroupRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ProductGroup productGroup)
        {

        }
    }
}
