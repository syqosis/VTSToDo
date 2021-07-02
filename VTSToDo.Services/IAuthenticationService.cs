using System.Threading.Tasks;
using VTSToDo.DAL;

namespace VTSToDo.Services
{
    public interface IAuthenticationService
    {
        public Task<UserModel> LoginUser(string userName, string password);
    }
}
