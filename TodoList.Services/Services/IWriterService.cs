using System.Threading.Tasks;
using TodoList.Models.Models;

namespace TodoList.Services.Services
{
    public interface IWriterService
    {
        Task<ObjectiveDTO> CreateObjective(ObjectiveDTO objective);
        Task<TaskDTO> CreateTask(TaskDTO task);
        Task DeleteObjective(int objectiveId);
        Task DeleteTask(int taskId);
        Task<ObjectiveDTO> UpdateObjective(ObjectiveDTO objective);
        Task<TaskDTO> UpdateTask(TaskDTO task);
    }
}