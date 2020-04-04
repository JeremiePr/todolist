using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoList.Data.Entities
{
    [Table("task")]
    public class TaskDB
    {
        [Column("id"), Key]
        public int Id { get; set; }

        [Column("objective_id")]
        public int ObjectiveId { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("details")]
        public string Details { get; set; }

        [Column("priority")]
        public int Priority { get; set; }

        [Column("status_type_key")]
        public int StatusTypeKey { get; set; }

        [Column("last_update_date")]
        public DateTime LastUpdateDate { get; set; }

        [ForeignKey("ObjectiveId")]
        public ObjectiveDB Objective { get; set; }

        [ForeignKey("StatusTypeKey")]
        public StatusTypeDB StatusType { get; set;  }
    }
}
