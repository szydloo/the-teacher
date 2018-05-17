using System;
using System.Threading.Tasks;
using TheTeacher.Core.Domain;

namespace TheTeacher.Infrastructure.Services
{
    public interface IPersonalDetailsService : IService
    {
        Task UpdateImageAsync(Guid userId, byte[] file);
        Task UpdatePersonalInfoAsync(Guid userId, Address address, DateTime dateOfBirth, string firstName, string lastName, string university, string fieldOfStudy, string title);
        Task<string> GetImageAsync(Guid userId);
         
    }
}