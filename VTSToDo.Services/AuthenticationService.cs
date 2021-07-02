using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VTSToDo.DAL;
using VTSToDo.Shared.Extensions;

namespace VTSToDo.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private ToDoContext _context;

        public AuthenticationService(ToDoContext context)
        {
            _context = context;
        }

        public async Task<UserModel> LoginUser(string userName, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user != null)
            {
                var hashString = CryptographyExtensions.ComputeHash(password);

                if (user.Password == hashString)
                {
                    return new UserModel { 
                        Id = user.Id,
                        UserName = user.UserName
                    };
                }
            }

            return null;
        }
    }
}
