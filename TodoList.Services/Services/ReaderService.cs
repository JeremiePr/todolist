using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Data.Providers;
using TodoList.Models.Enums;
using TodoList.Models.Models;

namespace TodoList.Services.Services
{
    public class ReaderService : IReaderService
    {
        private readonly IReaderProvider _readerProvider;

        public ReaderService(IReaderProvider readerProvider)
        {
            _readerProvider = readerProvider;
        }

        public async Task<IEnumerable<ObjectiveDTO>> GetObjectives(StatusTypes statusType)
        {
            return await _readerProvider.GetObjectives(statusType);
        }

        public async Task<IEnumerable<TaskDTO>> GetTasksByObjectiveId(int objectiveId, StatusTypes statusType)
        {
            return await _readerProvider.GetTasksByObjectiveId(objectiveId, statusType);
        }

        public async Task<IEnumerable<ObjectiveHistoryDTO>> GetObjectiveHistoriesByObjectiveId(int objectiveId)
        {
            return await _readerProvider.GetObjectiveHistoriesByObjectiveId(objectiveId);
        }

        public async Task<ObjectiveDTO> GetOneObjective(int objectiveId)
        {
            return await _readerProvider.GetOneObjective(objectiveId);
        }

        public async Task<TaskDTO> GetOneTask(int taskId)
        {
            return await _readerProvider.GetOneTask(taskId);
        }
    }
}
