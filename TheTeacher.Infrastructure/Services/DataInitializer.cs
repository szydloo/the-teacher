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
        private readonly ILessonService _lessonService;
        
        public DataInitializer(IUserService userService, ITeacherService teacherService, ILessonService lessonService)
        {
            _userService = userService;
            _teacherService = teacherService;
            _lessonService = lessonService;
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
                
                await _userService.RegisterAsync(userId,$"test{i}@email.com",$"secret{i}",$"username{i}", "user", $"TestTest{i}");
            }
            for(int i = 1; i <= 3; i++)
            {
                Guid userIdADmin = Guid.NewGuid();
                
                await _userService.RegisterAsync(userIdADmin, $"testadmin{i}@email.com", $"secretAdmin", $"usernameAdmin{i}", "admin", $"AdminAdmin{i}");
            }
            for(int i = 11; i <= 30; i++)
            {
                Guid userId = Guid.NewGuid();
                
                await _userService.RegisterAsync(userId, $"test{i}@email.com", $"secret{i}", $"username{i}", "user" );
            }

        TeachersIni:

            var teachers = await _teacherService.BrowseAsync();
            if(teachers.Any())
            {
                return;
            }
        

            for(int i = 1; i <= 10; i++)
            {
                var user = await _userService.GetAsync($"test{i}@email.com");
                await _teacherService.CreateAsync(user.Id, $"randomAdress{i}");
            }   
            for(int i = 1; i <= 3; i++) 
            {
                var u = await _userService.GetAsync($"test{i}@email.com");
                await _lessonService.AddAsync(u.Id, "Biology", "Science", "Elementary", 100M );
            }
                 
        }

    }
}