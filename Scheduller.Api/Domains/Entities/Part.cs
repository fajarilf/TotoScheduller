using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scheduller.Api.Domains.Entities
{
    [Table("parts")]
    public class Part
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = null!;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<ProcessDetail> ProcessDetails { get; set; } = [];
        public ICollection<ProcessComponent> ProcessComponents { get; set; } = [];
        public ICollection<ScheduleDetail> ScheduleDetails { get; set; } = [];
    }
}
