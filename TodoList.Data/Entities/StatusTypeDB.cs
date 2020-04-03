using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoList.Data.Entities
{
    [Table("status_type")]
    public class StatusTypeDB
    {
        [Column("type_key"), Key]
        public int Key { get; set; }

        [Column("type_name")]
        public string Name { get; set; }
    }
}
