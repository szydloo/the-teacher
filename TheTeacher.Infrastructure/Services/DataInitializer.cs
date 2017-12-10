using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheTeacher.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService _userService;
        public DataInitializer(IUserService userService)
        {
            _userService = userService;
        }
        public async Task SeedAsync()
        {
            var Tasks = new List<Task>(); 
            for(int i = 1; i <= 10; i++)
            {
                Tasks.Add(_userService.RegisterAsync($"test{i}@email.com",$"secret{i}",$"username{i}",$"TestTest{i}","user"));
            }
            for(int i = 1; i <= 3; i++)
            {
                Tasks.Add(_userService.RegisterAsync($"testadmin{i}@email.com", $"secretAdmin", $"usernameAdmin{i}",$"AdminAdmin{i}", "admin"));
            }
            await Task.WhenAll(Tasks);            
        }

    }
}