using System;
using TheTeacher.Infrastructure.DTO;

namespace TheTeacher.Infrastructure.Services
{
    public interface IJwtHandler : IService
    {
        JwtDTO CreateToken(Guid userId, string role);

    }
}