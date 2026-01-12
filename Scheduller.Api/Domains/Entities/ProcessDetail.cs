using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scheduller.Api.Domains.Entities
{
    [Table("process_details")]
    public class ProcessDetail
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("models_id")]
        public int ModelsId { get; set; }

        [Column("parts_id")]
        public int PartsId { get; set; }

        [Column("operation_number")]
        public int OperationNumber { get; set; }

        [Column("bom")]
        public int Bom { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Model Model { get; set; } = null!;
        public Part Part { get; set; } = null!;
    }

}