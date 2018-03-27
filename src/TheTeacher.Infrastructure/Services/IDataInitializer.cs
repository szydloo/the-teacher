using System.Threading.Tasks;

namespace TheTeacher.Infrastructure.Services
{
    public interface IDataInitializer : IService
    {
        Task SeedAsync();
    }
}