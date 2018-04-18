using AutoMapper;
using Itenso.TimePeriod;
using TheTeacher.Core.Domain;
using TheTeacher.Infrastructure.Dto;

namespace TheTeacher.Infrastructure.Mapper
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration( cfg =>
            {
                cfg.CreateMap<UserDto,User>();                
                cfg.CreateMap<TeacherDto,Teacher>();
                cfg.CreateMap<SubjectDto,Subject>();
                cfg.CreateMap<TimeRangeDto,TimeRange>();
            })
            .CreateMapper();
    }
}