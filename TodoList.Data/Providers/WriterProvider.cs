using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TodoList.Data.Context;
using TodoList.Data.Entities;
using TodoList.Data.Mapping;
using TodoList.Models.Enums;
using TodoList.Models.Models;
using TodoList.Utils.Wrappers;

namespace TodoList.Data.Providers
{
    public class WriterProvider : IWriterProvider
    {
        private readonly TodoListContext _context;
        private readonly IDateTimeWrapper _dateTimeWrapper;

        public WriterProvider(TodoListContext context, IDateTimeWrapper dateTimeWrapper)
        {
            _context = context;
            _dateTimeWrapper = dateTimeWrapper;
        }

        public async Task<ObjectiveDTO> CreateObjective(ObjectiveDTO dto)
        {
            var mapper = EntityMapping.GetMapper();

            var objective = mapper.Map<ObjectiveDB>(dto);
            _context.Objectives.Add(objective);

            var history = new ObjectiveHistoryDB
            {
                CurrentStatusTypeKey = (int)StatusTypes.Todo,
                IsNew = true,
                ObjectiveId = objective.Id,
                PreviousStatusTypeKey = null,
                UpdateDate = _dateTimeWrapper.Now
            };
            _context.ObjectiveHistories.Add(history);

            await _context.SaveChangesAsync();

            return mapper.Map<ObjectiveDTO>(objective);
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
                    UpdateDate = _dateTimeWrapper.Now
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

            return EntityMapping.GetMapper().Map<ObjectiveDTO>(objective);
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
            var mapper = EntityMapping.GetMapper();
            var task = mapper.Map<TaskDB>(dto);
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return mapper.Map<TaskDTO>(task);
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

            return EntityMapping.GetMapper().Map<TaskDTO>(task);
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
