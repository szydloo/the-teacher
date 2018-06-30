using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheTeacher.Infrastructure.Dto;

namespace TheTeacher.Infrastructure.Services
{
    public interface IUserService : IService
    {
        Task<UserDto> GetAsync(string email);
        Task<UserDto> GetAsync(Guid userId);

        Task<IEnumerable<UserDto>> GetUsersForIdsList(IEnumerable<Guid> userIds);
        Task RegisterAsync(Guid id, string email, string password, string username, string role);        
        Task LoginAsync(string email, string password);
        Task<IEnumerable<UserDto>> BrowseAsync();        
        Task DeleteAsync(Guid userId);
        Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
    }
}