using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scheduller.Api.Domains.Entities
{
    [Table("process_components")]
    public class ProcessComponent
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("parts_id")]
        public int PartsId { get; set; }

        [Column("operation_number")]
        public int OperationNumber { get; set; }

        [Column("work_centers_id")]
        public int WorkCentersId { get; set; }

        [Column("work_center_category")]
        public int WorkCenterCategory { get; set; }

        [Column("base_quantity")]
        public int BaseQuantity { get; set; }

        [Column("setup")]
        public int Setup { get; set; }

        [Column("cycle_time")]
        public int CycleTime { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        public Part Part { get; set; } = null!;
        public WorkCenter WorkCenter { get; set; } = null!;
    }
}