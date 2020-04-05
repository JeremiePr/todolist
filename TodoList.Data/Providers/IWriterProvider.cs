using System.Threading.Tasks;
using TodoList.Models.Models;

namespace TodoList.Data.Providers
{
    public interface IWriterProvider
    {
        Task<ObjectiveDTO> CreateObjective(ObjectiveDTO dto);
        Task<TaskDTO> CreateTask(TaskDTO dto);
        Task DeleteTask(int taskId);
        Task<ObjectiveDTO> UpdateObjective(ObjectiveDTO dto);
        Task<TaskDTO> UpdateTask(TaskDTO dto);
    }
}