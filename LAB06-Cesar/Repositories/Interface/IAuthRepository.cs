using LAB06_Cesar.Models;
using System.Threading.Tasks;

namespace LAB06_Cesar.Repositories.Interface
{
    public interface IAuthRepository
    {
        Task<Usuario> Register(Usuario user, string password);
        Task<Usuario> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}
