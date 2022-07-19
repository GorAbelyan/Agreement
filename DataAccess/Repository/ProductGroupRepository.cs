using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models;
using System.Linq;

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
            var objFromDb = _db.ProductGroups.FirstOrDefault(s => s.Id == productGroup.Id);
            if (objFromDb != null)
            {
                objFromDb.Active = productGroup.Active;
                objFromDb.GroupCode= productGroup.GroupCode;
                objFromDb.GroupDescription= productGroup.GroupDescription;
            }
        }
    }
}
