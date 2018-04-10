using System;
using TheTeacher.Infrastructure.DTO;

namespace TheTeacher.Infrastructure.Services
{
    public interface IJwtHandler : IService
    {
        JwtDto CreateToken(Guid userId,string username, string role);

    }
}