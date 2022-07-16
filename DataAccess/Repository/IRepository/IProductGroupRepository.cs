using Models;

namespace DataAccess.Repository.IRepository
{
    public interface IProductGroupRepository:IRepository<ProductGroup>
    {
        void Update(ProductGroup productGroup);
    }
}
