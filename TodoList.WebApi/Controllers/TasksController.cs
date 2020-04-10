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
    public class TasksController
    {
        private readonly IReaderService _readerService;
        private readonly IWriterService _writerService;

        public TasksController(IReaderService readerService, IWriterService writerService)
        {
            _readerService = readerService;
            _writerService = writerService;
        }

        [HttpGet("")]
        public async Task<IEnumerable<TaskDTO>> GetTasks(int objectiveId, int statusTypeKey)
        {
            StatusTypes? statusType = null;
            if (Enum.IsDefined(typeof(StatusTypes), statusTypeKey))
                statusType = (StatusTypes)statusTypeKey;

            return await _readerService.GetTasksByObjectiveId(objectiveId, statusType);
        }

        [HttpGet("id")]
        public async Task<TaskDTO> GetOneTask(int taskId)
        {
            return await _readerService.GetOneTask(taskId);
        }

        [HttpPost("")]
        public async Task<TaskDTO> CreateTask([FromBody]TaskDTO task)
        {
            return await _writerService.CreateTask(task);
        }

        [HttpPut("")]
        public async Task<TaskDTO> UpdateTask([FromBody]TaskDTO task)
        {
            return await _writerService.UpdateTask(task);
        }

        [HttpDelete("")]
        public async Task DeleteTask(int taskId)
        {
            await _writerService.DeleteTask(taskId);
        }
    }
}
