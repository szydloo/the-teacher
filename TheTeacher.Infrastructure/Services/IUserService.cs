using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheTeacher.Infrastructure.DTO;

namespace TheTeacher.Infrastructure.Services
{
    public interface IUserService : IService
    {
        Task<UserDTO> GetAsync(string email);
        Task RegisterAsync(Guid id, string email, string password, string username, string role);        
        Task LoginAsync(string email, string password);
        Task<IEnumerable<UserDTO>> BrowseAsync();        
        Task DeleteAsync(Guid userId);
        Task ChangeUsernameAsync(Guid userId, string newUsername);
        Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
    }
}