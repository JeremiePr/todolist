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
    public class ObjectivesController
    {
        private readonly IReaderService _readerService;
        private readonly IWriterService _writerService;

        public ObjectivesController(IReaderService readerService, IWriterService writerService)
        {
            _readerService = readerService;
            _writerService = writerService;
        }

        [HttpGet("")]
        public async Task<IEnumerable<ObjectiveDTO>> GetObjectives(int statusTypeKey)
        {
            StatusTypes? statusType = null;
            if (Enum.IsDefined(typeof(StatusTypes), statusTypeKey))
                statusType = (StatusTypes)statusTypeKey;

            return await _readerService.GetObjectives(statusType);
        }

        [HttpGet("id")]
        public async Task<ObjectiveDTO> GetOneObjective(int objectiveId)
        {
            return await _readerService.GetOneObjective(objectiveId);
        }

        [HttpPost("")]
        public async Task<ObjectiveDTO> CreateObjective([FromBody]ObjectiveDTO objective)
        {
            return await _writerService.CreateObjective(objective);
        }

        [HttpPut("")]
        public async Task<ObjectiveDTO> UpdateObjective([FromBody]ObjectiveDTO objective)
        {
            return await _writerService.UpdateObjective(objective);
        }
    }
}
