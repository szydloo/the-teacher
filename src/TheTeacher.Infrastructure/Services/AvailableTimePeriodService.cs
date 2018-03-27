using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Itenso.TimePeriod;
using TheTeacher.Infrastructure.DTO;
using TheTeacher.Infrastructure.Extensions;
using TheTeacher.Infrastructure.Repositories;

namespace TheTeacher.Infrastructure.Services
{
    public class AvailableTimePeriodService : IAvailableTimePeriodService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;

        public AvailableTimePeriodService(ITeacherRepository teacherRepository, IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TimeRangeDTO>> BrowseAsync(string name)
        {
            var teacher = await _teacherRepository.GetOrFailAsync(name);
            var timePeriodCol = teacher.GetTimePeriodCollection();
            var timeRangeList = new List<TimeRangeDTO>();
            foreach(var timePeriod in timePeriodCol)
            {
                timeRangeList.Add(_mapper.Map<TimeRangeDTO>((TimeRange)timePeriod));
            }
            
            return timeRangeList;
        }
        
        public async Task AddTimePeriodAsync(Guid userId, DateTime start, DateTime end)
        {
            var teacher = await _teacherRepository.GetOrFailAsync(userId);
            teacher.AddAvailableTimePeriod(start, end);
        }

        public async Task RemoveTimePeriodAsync(Guid userId, DateTime start)
        {
            var teacher = await _teacherRepository.GetOrFailAsync(userId);
            teacher.RemoveAvailableTimePeriod(start);
        }
    }
}