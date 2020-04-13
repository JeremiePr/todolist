using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TodoList.Application.Models;
using TodoList.Application.Models.Enums;
using TodoList.Application.Utils;

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

        public async Task<IList<TaskDTO>> GetTasks(int objectiveId, StatusTypes? statusType)
        {
            return await _http.GetJsonAsync<IList<TaskDTO>>(HttpRequestUtils.FormatUrl(_baseUrl,
                ("objectiveId", objectiveId),
                ("statusTypeKey", (int?)statusType ?? 0)
            ));
        }

        public async Task<TaskDTO> UpdateTask(TaskDTO task)
        {
            return await _http.PutJsonAsync<TaskDTO>(_baseUrl, task);
        }
    }
}
