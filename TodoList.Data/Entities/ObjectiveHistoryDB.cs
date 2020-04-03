using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoList.Data.Entities
{
    [Table("objective_history")]
    public class ObjectiveHistoryDB
    {
        [Column("id"), Key]
        public int Id { get; set; }

        [Column("objective_id")]
        public int ObjectiveId { get; set; }

        [Column("previous_status_type_key")]
        public int? PreviousStatusTypeKey { get; set; }

        [Column("current_status_type_key")]
        public int CurrentStatusTypeKey { get; set; }

        [Column("update_date")]
        public DateTime UpdateDate { get; set; }

        [Column("is_new")]
        public bool IsNew { get; set; }

        [ForeignKey("ObjectiveId")]
        public ObjectiveDB Objective { get; set; }

        [ForeignKey("PreviousStatusTypeKey")]
        public StatusTypeDB PreviousStatusType { get; set; }

        [ForeignKey("CurrentStatusTypeKey")]
        public StatusTypeDB CurrentStatusType { get; set; }
    }
}
