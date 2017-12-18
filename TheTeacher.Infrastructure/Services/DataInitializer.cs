using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheTeacher.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService _userService;
        private readonly ITeacherService _teacherService;
        
        public DataInitializer(IUserService userService, ITeacherService teacherService)
        {
            _userService = userService;
            _teacherService = teacherService;
        }
        public async Task SeedAsync()
        {
            var users = await _userService.BrowseAsync();
            if(users.Any())
            {
                goto TeachersIni;
            }


            for(int i = 1; i <= 10; i++)
            {
                Guid userId = Guid.NewGuid();
                
                await _userService.RegisterAsync(userId,$"test{i}@email.com",$"secret{i}",$"username{i}",$"TestTest{i}","user");
            }
            for(int i = 1; i <= 3; i++)
            {
                Guid userIdADmin = Guid.NewGuid();
                
                await _userService.RegisterAsync(userIdADmin, $"testadmin{i}@email.com", $"secretAdmin", $"usernameAdmin{i}",$"AdminAdmin{i}", "admin");
            }

        TeachersIni:

            var teachers = await _teacherService.BrowseAsync();
            if(teachers.Any())
            {
                return;
            }
        

            for(int i = 1; i <= 5; i++)
            {
                var user = await _userService.GetAsync($"test{i}@email.com");
                await _teacherService.CreateAsync(user.Id, $"randomAdress{i}");
            }        
        }

    }
}