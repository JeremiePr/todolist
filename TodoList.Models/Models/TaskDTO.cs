using System;
using TodoList.Models.Enums;

namespace TodoList.Models.Models
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
