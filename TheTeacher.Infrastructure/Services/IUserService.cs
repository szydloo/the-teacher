using System;
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
        Task DeleteAsync(Guid userId);
        Task ChangeUsernameAsync(Guid userId, string newUsername);
        Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);


    }
}