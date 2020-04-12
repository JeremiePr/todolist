using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;
using TodoList.Application.Models;

namespace TodoList.Application.Services
{
    public class TaskService
    {
        private readonly HttpClient _http;
        private readonly string _baseUrl;

        public TaskService(HttpClient http, AppSettings appSettings)
        {
            _http = http;
            _baseUrl = appSettings.BaseApiUrl + "/Tasks";
        }

        public async Task<TaskDTO> UpdateTask(TaskDTO task)
        {
            return await _http.PutJsonAsync<TaskDTO>(_baseUrl, task);
        }
    }
}
