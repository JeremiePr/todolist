using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TodoList.Application.Models;
using TodoList.Application.Models.Enums;
using TodoList.Application.Utils;

namespace TodoList.Application.Services
{
    public class ObjectiveService
    {
        private readonly HttpClient _http;
        private readonly string _baseUrl;

        public ObjectiveService(HttpClient http, AppSettings appSettings)
        {
            _http = http;
            _baseUrl = appSettings.BaseApiUrl + "/Objectives";
        }

        public async Task<IList<ObjectiveDTO>> GetObjectives(StatusTypes? statusType)
        {
            return await _http.GetJsonAsync<IList<ObjectiveDTO>>(HttpRequestUtils.FormatUrl(_baseUrl,
               ("statusTypeKey", (int?)statusType ?? 0) 
            ));
        }
    }
}
