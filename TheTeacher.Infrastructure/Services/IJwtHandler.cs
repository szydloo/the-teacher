using TheTeacher.Infrastructure.DTO;

namespace TheTeacher.Infrastructure.Services
{
    public interface IJwtHandler : IService
    {
        JwtDTO CreateToken(string email, string role);

    }
}