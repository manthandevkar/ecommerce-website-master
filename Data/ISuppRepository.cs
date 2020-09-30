using System.Threading.Tasks;
using CP.API.Model;

namespace CP.API.Data
{
    public interface ISuppRepository
    {
        Task<User> SupplierRegister(User supplier, string password);
        Task<User> SupplierLogin(string email, string password);
        Task<bool> SupplierExists(string email);

    }
}