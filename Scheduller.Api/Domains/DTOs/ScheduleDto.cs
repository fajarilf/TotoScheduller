using Scheduller.Api.Domains.Entities;
using System.Runtime.CompilerServices;

namespace Scheduller.Api.Domains.DTOs
{
    public static class ScheduleDto
    {
        public static ScheduleResponse toScheduleResponse (Schedule entity)
        {
            return new ScheduleResponse
            {
                Id = entity.Id,
                ModelName = entity.Model.Name,
                Quantity = entity.Quantity,
                CreatedAt = entity.CreatedAt,
            };
        }
    }

    public class ScheduleCreateRequest
    {
        public int ModelId { get; set; }
        public int Quantity { get; set; }
    }

    public class ScheduleUpdateRequest
    {
        public int Id { get; set; }
        public int? ModelId { get; set; }
        public int? Quantity { get; set; }
    }

    public class ScheduleResponse
    {
        public int Id { get; set; }
        public string ModelName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
