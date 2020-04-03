using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Models.Enums;
using TodoList.Models.Models;
using TodoList.Services.Services;

namespace TodoList.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoListController
    {
        private readonly IReaderService _readerService;

        public TodoListController(IReaderService readerService)
        {
            _readerService = readerService;
        }

        [HttpGet("Objectives")]
        public async Task<IEnumerable<ObjectiveDTO>> GetObjectives(int statusTypeKey)
        {
            if (!Enum.IsDefined(typeof(StatusTypes), statusTypeKey))
                throw new InvalidCastException($"Integer type '{statusTypeKey}' cannot be converted into type ${nameof(StatusTypes)}");

            return await _readerService.GetObjectives((StatusTypes)statusTypeKey);
        }

        [HttpGet("Tasks")]
        public async Task<IEnumerable<TaskDTO>> GetTasks(int objectiveId, int statusTypeKey)
        {
            if (!Enum.IsDefined(typeof(StatusTypes), statusTypeKey))
                throw new InvalidCastException($"Integer type '{statusTypeKey}' cannot be converted into type ${nameof(StatusTypes)}");

            return await _readerService.GetTasksByObjectiveId(objectiveId, (StatusTypes)statusTypeKey);
        }

        [HttpGet("ObjectiveHistories")]
        public async Task<IEnumerable<ObjectiveHistoryDTO>> GetObjectiveHistories(int objectiveId)
        {
            return await _readerService.GetObjectiveHistoriesByObjectiveId(objectiveId);
        }

        [HttpGet("Objective")]
        public async Task<ObjectiveDTO> GetOneObjective(int objectiveId)
        {
            return await _readerService.GetOneObjective(objectiveId);
        }

        [HttpGet("Task")]
        public async Task<TaskDTO> GetOneTask(int taskId)
        {
            return await _readerService.GetOneTask(taskId);
        }
    }
}
