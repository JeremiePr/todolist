using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoList.Data.Entities
{
    [Table("objective")]
    public class ObjectiveDB
    {
        [Column("id"), Key]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("details")]
        public string Details { get; set; }

        [Column("priority")]
        public int Priority { get; set; }

        [Column("status_type_key")]
        public int StatusTypeKey { get; set; }

        [Column("status_date")]
        public DateTime? StatusDate { get; set; }

        [Column("status_details")]
        public string StatusDetails { get; set; }

        [Column("last_update_date")]
        public DateTime LastUpdateDate { get; set; }

        [ForeignKey("StatusTypeKey")]
        public StatusTypeDB StatusType { get; set; }
    }
}
