using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Data.Context;
using TodoList.Data.Entities;
using TodoList.Data.Mapping;
using TodoList.Models.Enums;
using TodoList.Models.Models;

namespace TodoList.Data.Providers
{
    public class ReaderProvider : IReaderProvider
    {
        private readonly TodoListContext _context;

        public ReaderProvider(TodoListContext context)
        {
            _context = context;
        }

        private Action<IMapperConfigurationExpression> GetMapperConfigurator(bool excludeCollections) => new Action<IMapperConfigurationExpression>(cfg =>
        {
            if (excludeCollections)
            {
                cfg.CreateMap<ObjectiveDB, ObjectiveDTO>()
                .ForMember(dto => dto.StatusType, opt => opt.MapFrom(dbo => (StatusTypes)dbo.StatusTypeKey))
                .ForMember(dto => dto.Tasks, opt => opt.MapFrom(dbo => (IList<TaskDTO>)null));
            }
            else
            {
                cfg.CreateMap<ObjectiveDB, ObjectiveDTO>()
                .ForMember(dto => dto.StatusType, opt => opt.MapFrom(dbo => (StatusTypes)dbo.StatusTypeKey));
            }

            cfg.CreateMap<TaskDB, TaskDTO>()
                .ForMember(dto => dto.StatusType, opt => opt.MapFrom(dbo => (StatusTypes)dbo.StatusTypeKey));

            cfg.CreateMap<ObjectiveHistoryDB, ObjectiveHistoryDTO>()
                .ForMember(dto => dto.PreviousStatusType, opt => opt.MapFrom(dbo => (StatusTypes?)dbo.PreviousStatusTypeKey))
                .ForMember(dto => dto.CurrentStatusType, opt => opt.MapFrom(dbo => (StatusTypes)dbo.CurrentStatusTypeKey));
        });

        public async Task<IEnumerable<ObjectiveDTO>> GetObjectives(StatusTypes? statusType)
        {
            var query =
                from 
                    x in _context.Objectives
                where 
                    !statusType.HasValue || x.StatusTypeKey == (int)statusType.Value
                orderby 
                    x.Priority descending, 
                    x.LastUpdateDate ascending
                select 
                    x;

            var objectives = await query
                .ProjectTo<ObjectiveDTO>(EntityMapping.GetMapperConfig())
                .ToListAsync();

            foreach (var objective in objectives)
            {
                objective.Tasks = objective.Tasks
                    .OrderByDescending(x => x.Priority);
            }

            return objectives;
        }

        public async Task<IEnumerable<TaskDTO>> GetTasksByObjectiveId(int objectiveId, StatusTypes? statusType)
        {
            var query =
                from 
                    x in _context.Tasks
                where 
                    x.ObjectiveId == objectiveId && 
                    (!statusType.HasValue || x.StatusTypeKey == (int)statusType.Value)
                orderby 
                    x.Priority descending,
                    x.LastUpdateDate ascending
                select 
                    x;

            var tasks = await query
                .ProjectTo<TaskDTO>(EntityMapping.GetMapperConfig(false))
                .ToListAsync();

            return tasks;
        }

        public async Task<IEnumerable<ObjectiveHistoryDTO>> GetObjectiveHistoriesByObjectiveId(int objectiveId)
        {
            var query =
                from 
                    x in _context.ObjectiveHistories
                where 
                    x.ObjectiveId == objectiveId
                orderby 
                    x.UpdateDate descending
                select 
                    x;

            var histories = await query
                .ProjectTo<ObjectiveHistoryDTO>(EntityMapping.GetMapperConfig(false))
                .ToListAsync();

            return histories;
        }

        public async Task<ObjectiveDTO> GetOneObjective(int objectiveId)
        {
            var query =
                from 
                    x in _context.Objectives
                where 
                    x.Id == objectiveId
                select 
                    x;

            var objective = await query
                .ProjectTo<ObjectiveDTO>(EntityMapping.GetMapperConfig())
                .SingleOrDefaultAsync();

            objective.Tasks = objective.Tasks
                    .OrderByDescending(x => x.Priority);

            return objective;
        }

        public async Task<TaskDTO> GetOneTask(int taskId)
        {
            var query =
                from 
                    x in _context.Tasks
                where 
                    x.Id == taskId
                select 
                    x;

            var task = await query
                .ProjectTo<TaskDTO>(EntityMapping.GetMapperConfig(false))
                .SingleOrDefaultAsync();

            return task;
        }
    }
}
