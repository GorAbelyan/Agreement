using System;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        Task SaveAsync();
        public IAgreementRepository Agreement { get; }
        public IProductRepository Product { get; }
        public IProductGroupRepository ProductGroup { get; }
        public IApplicationUserRepository ApplicationUser { get; }
    }
}
