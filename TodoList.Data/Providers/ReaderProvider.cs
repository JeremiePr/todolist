﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Data.Context;
using TodoList.Data.Mapping;
using TodoList.Models.Enums;
using TodoList.Models.Models;

namespace TodoList.Data.Providers
{
    public class ReaderProvider : IReaderProvider
    {
        private readonly TodoListContext _context;
        private readonly MapperConfiguration _mapperConfig;

        public ReaderProvider(TodoListContext context)
        {
            _context = context;
            _mapperConfig = EntityMapping.GetMapperConfig();
        }

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
                .ProjectTo<TaskDTO>(_mapperConfig)
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
                .ProjectTo<ObjectiveHistoryDTO>(_mapperConfig)
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
                .ProjectTo<TaskDTO>(_mapperConfig)
                .SingleOrDefaultAsync();

            return task;
        }
    }
}
