using Models;

namespace DataAccess.Repository.IRepository
{
    public interface IAgreementRepository : IRepository<Agreement>
    {
        void Update(Agreement agreement);
    }
}
