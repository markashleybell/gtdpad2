using System.Threading.Tasks;
using gtdpad.Domain;

namespace gtdpad.Services
{
    public interface IRepository
    {
        Task<User> FindUserByEmail(string email);
    }
}
