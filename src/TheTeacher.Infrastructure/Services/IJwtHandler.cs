using System;
using TheTeacher.Infrastructure.Dto;

namespace TheTeacher.Infrastructure.Services
{
    public interface IJwtHandler : IService
    {
        JwtDto CreateToken(Guid userId,string username, string role);

    }
}