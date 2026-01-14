using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scheduller.Api.Domains.Entities
{
    [Table("schedule_detail")]
    public class ScheduleDetail
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("schedule_id")]
        public int ScheduleId { get; set; }

        [Column("part_id")]
        public int PartId { get; set; }

        [Column("work_center_id")]
        public int WorkCenterId { get; set; }

        [Column("operation_number")]
        public int OperationNumber { get; set; }

        [Column("start_time")]
        public DateTime StartTime { get; set; }

        [Column("finish_time")]
        public DateTime FinishTime { get; set; }

        public Schedule Schedule { get; set; } = null!;
        public Part Part { get; set; } = null!;
        public WorkCenter WorkCenter { get; set; } = null!;
    }
}
