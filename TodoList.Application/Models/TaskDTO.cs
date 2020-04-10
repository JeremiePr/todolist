using System;
using TodoList.Application.Models.Enums;

namespace TodoList.Application.Models
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public int ObjectiveId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public int Priority { get; set; }
        public ObjectiveDTO Objective { get; set; }
        public StatusTypes StatusType { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
