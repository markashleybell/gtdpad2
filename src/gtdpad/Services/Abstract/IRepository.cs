using System;
using System.Threading.Tasks;
using gtdpad.Domain;

namespace gtdpad.Services
{
    public interface IRepository
    {
        Task<Page> GetPage(Guid id);

        Task PersistPage(Page page);

        Task DeletePage(Guid id);

        Task<User> FindUserByEmail(string email);
    }
}
