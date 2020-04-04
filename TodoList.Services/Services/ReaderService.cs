using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Data.Providers;
using TodoList.Models.Enums;
using TodoList.Models.Models;

namespace TodoList.Services.Services
{
    public class ReaderService : IReaderService
    {
        private readonly IReaderProvider _provider;

        public ReaderService(IReaderProvider readerProvider)
        {
            _provider = readerProvider;
        }

        public async Task<IEnumerable<ObjectiveDTO>> GetObjectives(StatusTypes statusType)
        {
            try
            {
                return await _provider.GetObjectives(statusType);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error on getting objectives: {ex.Message}");
            }
        }

        public async Task<IEnumerable<TaskDTO>> GetTasksByObjectiveId(int objectiveId, StatusTypes statusType)
        {
            try
            {
                return await _provider.GetTasksByObjectiveId(objectiveId, statusType);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error on getting tasks by objective id: {ex.Message}");
            }
        }

        public async Task<IEnumerable<ObjectiveHistoryDTO>> GetObjectiveHistoriesByObjectiveId(int objectiveId)
        {
            try
            {
                return await _provider.GetObjectiveHistoriesByObjectiveId(objectiveId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error on getting objective histories by objective id: {ex.Message}");
            }
        }

        public async Task<ObjectiveDTO> GetOneObjective(int objectiveId)
        {
            try
            {
                return await _provider.GetOneObjective(objectiveId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error on getting objective with id '{objectiveId}': {ex.Message}");
            }
        }

        public async Task<TaskDTO> GetOneTask(int taskId)
        {
            try
            {
                return await _provider.GetOneTask(taskId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error on getting task with id '{taskId}': {ex.Message}");
            }
        }
    }
}
