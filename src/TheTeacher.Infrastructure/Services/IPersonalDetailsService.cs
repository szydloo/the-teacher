using System;
using System.Threading.Tasks;
using TheTeacher.Core.Domain;

namespace TheTeacher.Infrastructure.Services
{
    public interface IPersonalDetailsService : IService
    {
        Task UpdateImageAsync(Guid userId, byte[] image);
        Task UpdatePersonalInfoAsync(Guid userId, Address address, DateTime dateOfBirth, string firstName, string lastName, string university, string fieldOfStudy, string title);
         
    }
}