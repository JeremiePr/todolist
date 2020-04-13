using System;
using System.Collections.Generic;
using TodoList.Application.Models.Enums;

namespace TodoList.Application.Models
{
    public class ObjectiveDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public int Priority { get; set; }
        public DateTime? StatusDate { get; set; }
        public string StatusDetails { get; set; }
        public StatusTypes StatusType { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
