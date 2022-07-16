using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models;
using System.Linq;

namespace DataAccess.Repository
{
    public class AgreementRepository : Repository<Agreement>, IAgreementRepository
    {
        private readonly ApplicationDbContext _db;
        public AgreementRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Agreement agreement)
        {
            var objFromDb = _db.Agreements.FirstOrDefault(s => s.Id == agreement.Id);
            if (objFromDb != null)
            {
                objFromDb.EffectiveDate = agreement.EffectiveDate;
                objFromDb.ExpirationDate = agreement.ExpirationDate;
                objFromDb.NewPrice = agreement.NewPrice;
                objFromDb.ProductPrice = agreement.ProductPrice;
                objFromDb.Product = agreement.Product;
                objFromDb.ProductId = agreement.ProductId;
                objFromDb.ProductGroup = agreement.ProductGroup;
                objFromDb.ProductGroupId = agreement.ProductGroupId;
            }
        }
    }
}
