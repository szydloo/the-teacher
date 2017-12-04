using System.Collections.Generic;
using System.Threading.Tasks;
using TheTeacher.Infrastructure.DTO;

namespace TheTeacher.Infrastructure.Services
{
    public interface IUserService : IService
    {
        Task<UserDTO> GetAsync(string email);
        Task RegisterAsync(string email, string password, string username, string fullname, string role);        
        Task<IEnumerable<UserDTO>> BrowseAsync();        
    }
}