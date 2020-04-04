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
    public class ObjectiveHistoriesController
    {
        private readonly IReaderService _readerService;

        public ObjectiveHistoriesController(IReaderService readerService, IWriterService writerService)
        {
            _readerService = readerService;
        }

        [HttpGet("")]
        public async Task<IEnumerable<ObjectiveHistoryDTO>> GetObjectives(int objectiveId)
        {
            return await _readerService.GetObjectiveHistoriesByObjectiveId(objectiveId);
        }
    }
}
