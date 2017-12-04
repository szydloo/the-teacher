using AutoMapper;
using TheTeacher.Core.Domain;
using TheTeacher.Infrastructure.DTO;

namespace TheTeacher.Infrastructure.Mapper
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration( cfg =>
            {
                cfg.CreateMap<UserDTO,User>();                
                cfg.CreateMap<TeacherDTO,User>();
            })
            .CreateMapper();
    }
}