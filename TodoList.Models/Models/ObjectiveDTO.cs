using System;
using System.Collections.Generic;
using TodoList.Models.Enums;

namespace TodoList.Models.Models
{
    public class ObjectiveDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public int Priority { get; set; }
        public int StatusTypeId { get; set; }
        public DateTime? StatusDate { get; set; }
        public string StatusDetails { get; set; }
        public StatusTypes StatusType { get; set; }
        public IEnumerable<TaskDTO> Tasks { get; set; }
    }
}
