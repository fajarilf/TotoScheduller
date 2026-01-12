using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scheduller.Api.Domains.Entities
{
    [Table("schedule")]
    public class Schedule
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("model_id")]
        public int ModelId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public Model Model { get; set; } = null!;
        public ICollection<ScheduleDetail> ScheduleDetails { get; set; } = [];
    }
}
