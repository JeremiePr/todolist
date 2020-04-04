using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TodoList.Data.Context;
using TodoList.Data.Entities;
using TodoList.Models.Enums;
using TodoList.Models.Models;

namespace TodoList.Data.Providers
{
    public class WriterProvider : IWriterProvider
    {
        private readonly TodoListContext _context;
        private readonly MapperConfiguration _mapperConfig;
        private readonly IMapper _mapper;

        public WriterProvider(TodoListContext context)
        {
            _context = context;

            _mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ObjectiveDB, ObjectiveDTO>()
                    .ForMember(dto => dto.StatusType, opt => opt.MapFrom(dbo => (StatusTypes)dbo.StatusTypeKey));

                cfg.CreateMap<TaskDB, TaskDTO>()
                    .ForMember(dto => dto.StatusType, opt => opt.MapFrom(dbo => (StatusTypes)dbo.StatusTypeKey));

                cfg.CreateMap<ObjectiveHistoryDB, ObjectiveHistoryDTO>()
                    .ForMember(dto => dto.PreviousStatusType, opt => opt.MapFrom(dbo => (StatusTypes?)dbo.PreviousStatusTypeKey))
                    .ForMember(dto => dto.CurrentStatusType, opt => opt.MapFrom(dbo => (StatusTypes)dbo.PreviousStatusTypeKey));

                cfg.CreateMap<ObjectiveDTO, ObjectiveDB>()
                    .ForMember(dbo => dbo.StatusTypeKey, opt => opt.MapFrom(dbo => (int)dbo.StatusType));

                cfg.CreateMap<TaskDTO, TaskDB>()
                    .ForMember(dbo => dbo.StatusTypeKey, opt => opt.MapFrom(dbo => (int)dbo.StatusType));

                cfg.CreateMap<ObjectiveHistoryDTO, ObjectiveHistoryDB>()
                    .ForMember(dto => dto.PreviousStatusTypeKey, opt => opt.MapFrom(dbo => (int)dbo.PreviousStatusType))
                    .ForMember(dto => dto.CurrentStatusTypeKey, opt => opt.MapFrom(dbo => (int)dbo.CurrentStatusType));
            });

            _mapper = new Mapper(_mapperConfig);
        }

        public async Task<ObjectiveDTO> CreateObjective(ObjectiveDTO dto)
        {
            var objective = _mapper.Map<ObjectiveDB>(dto);
            _context.Objectives.Add(objective);

            var history = new ObjectiveHistoryDB
            {
                CurrentStatusTypeKey = (int)StatusTypes.Todo,
                IsNew = true,
                ObjectiveId = objective.Id,
                PreviousStatusTypeKey = null,
                UpdateDate = DateTime.Now
            };
            _context.ObjectiveHistories.Add(history);

            await _context.SaveChangesAsync();

            return _mapper.Map<ObjectiveDTO>(objective);
        }

        public async Task<ObjectiveDTO> UpdateObjective(ObjectiveDTO dto)
        {
            var objective = await _context.Objectives.SingleOrDefaultAsync(x => x.Id == dto.Id);
            if (objective == null)
                throw new Exception($"There is no objective with id '{dto.Id}'");

            if (objective.StatusTypeKey != (int)dto.StatusType)
            {
                var history = new ObjectiveHistoryDB
                {
                    CurrentStatusTypeKey = (int)dto.StatusType,
                    IsNew = false,
                    ObjectiveId = objective.Id,
                    PreviousStatusTypeKey = objective.StatusTypeKey,
                    UpdateDate = DateTime.Now
                };
                _context.ObjectiveHistories.Add(history);
            }

            objective.Details = dto.Details;
            objective.Priority = dto.Priority;
            objective.StatusDate = dto.StatusDate;
            objective.StatusDetails = dto.StatusDetails;
            objective.StatusTypeKey = (int)dto.StatusType;
            objective.Title = dto.Title;
            _context.Objectives.Update(objective);

            await _context.SaveChangesAsync();

            return _mapper.Map<ObjectiveDTO>(objective);
        }

        public async Task DeleteObjective(int objectiveId)
        {
            var objective = await _context.Objectives.SingleOrDefaultAsync(x => x.Id == objectiveId);
            if (objective == null)
                throw new Exception($"There is no objective with id '{objectiveId}'");

            _context.Objectives.Remove(objective);
            await _context.SaveChangesAsync();
        }

        public async Task<TaskDTO> CreateTask(TaskDTO dto)
        {
            var task = _mapper.Map<TaskDB>(dto);
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return _mapper.Map<TaskDTO>(task);
        }

        public async Task<TaskDTO> UpdateTask(TaskDTO dto)
        {
            var task = await _context.Tasks.SingleOrDefaultAsync(x => x.Id == dto.Id);
            if (task == null)
                throw new Exception($"There is no task with id '{dto.Id}'");

            task.Details = dto.Details;
            task.Priority = dto.Priority;
            task.StatusTypeKey = (int)dto.StatusType;
            task.Title = dto.Title;
            _context.Tasks.Update(task);

            await _context.SaveChangesAsync();

            return _mapper.Map<TaskDTO>(task);
        }

        public async Task DeleteTask(int taskId)
        {
            var task = await _context.Tasks.SingleOrDefaultAsync(x => x.Id == taskId);
            if (task == null)
                throw new Exception($"There is no task with id '{taskId}'");

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
