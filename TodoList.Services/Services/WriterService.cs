using System;
using System.Threading.Tasks;
using TodoList.Data.Providers;
using TodoList.Models.Models;

namespace TodoList.Services.Services
{
    public class WriterService : IWriterService
    {
        private readonly IWriterProvider _provider;

        public WriterService(IWriterProvider provider)
        {
            _provider = provider;
        }

        public async Task<ObjectiveDTO> CreateObjective(ObjectiveDTO objective)
        {
            try
            {
                return await _provider.CreateObjective(objective);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error on creating objective: {ex.Message}");
            }
        }

        public async Task<ObjectiveDTO> UpdateObjective(ObjectiveDTO objective)
        {
            try
            {
                return await _provider.UpdateObjective(objective);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error on updating objective: {ex.Message}");
            }
        }

        public async Task<TaskDTO> CreateTask(TaskDTO task)
        {
            try
            {
                return await _provider.CreateTask(task);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error on creating task: {ex.Message}");
            }
        }

        public async Task<TaskDTO> UpdateTask(TaskDTO task)
        {
            try
            {
                return await _provider.UpdateTask(task);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error on updating task: {ex.Message}");
            }
        }

        public async Task DeleteTask(int taskId)
        {
            try
            {
                await _provider.DeleteTask(taskId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error on deleting task: {ex.Message}");
            }
        }
    }
}
