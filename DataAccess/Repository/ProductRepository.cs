using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models;
using System.Linq;

namespace DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            var objFromDb = _db.Products.FirstOrDefault(s => s.Id == product.Id);
            if (objFromDb != null)
            {
                objFromDb.Price = product.Price;
                objFromDb.ProductDescription = product.ProductDescription;
                objFromDb.Active = product.Active;
                objFromDb.ProductGroupID = product.ProductGroupID;
            }
        }
    }
}
