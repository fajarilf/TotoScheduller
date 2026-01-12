using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scheduller.Api.Domains.Entities
{
    [Table("models")]
    public class Model
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = null!;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<ProcessDetail> ProcessDetails { get; set; } = [];
        public ICollection<Schedule> Schedules { get; set; } = [];
    }
}
