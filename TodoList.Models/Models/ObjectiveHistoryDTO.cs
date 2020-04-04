using System;
using TodoList.Models.Enums;

namespace TodoList.Models.Models
{
    public class ObjectiveHistoryDTO
    {
        public int Id { get; set; }
        public int ObjectiveId { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsNew { get; set; }
        public ObjectiveDTO Objective { get; set; }
        public StatusTypes? PreviousStatusType { get; set; }
        public StatusTypes CurrentStatusType { get; set;  }
    }
}
