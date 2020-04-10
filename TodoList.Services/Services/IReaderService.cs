using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Models.Enums;
using TodoList.Models.Models;

namespace TodoList.Services.Services
{
    public interface IReaderService
    {
        Task<IEnumerable<ObjectiveHistoryDTO>> GetObjectiveHistoriesByObjectiveId(int objectiveId);
        Task<IEnumerable<ObjectiveDTO>> GetObjectives(StatusTypes? statusType);
        Task<ObjectiveDTO> GetOneObjective(int objectiveId);
        Task<TaskDTO> GetOneTask(int taskId);
        Task<IEnumerable<TaskDTO>> GetTasksByObjectiveId(int objectiveId, StatusTypes? statusType);
    }
}