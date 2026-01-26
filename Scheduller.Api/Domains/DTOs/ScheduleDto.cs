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

        public static ScheduleResponseWithDetails toScheduleResponseWithDetails(Schedule entity)
        {
            return new ScheduleResponseWithDetails
            {
                Id = entity.Id,
                ModelName = entity.Model.Name,
                Quantity = entity.Quantity,
                CreatedAt = entity.CreatedAt,
                ScheduleDetails = [.. entity.ScheduleDetails.Select(sd => ScheduleDetailDto.toScheduleDetailResponse(sd))],
            };
        }
    }

    public class ScheduleCreateRequest
    {
        public required int ModelId { get; set; }
        public required int Quantity { get; set; }
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

    public class ScheduleResponseWithDetails
    {
        public int Id { get; set; }
        public string ModelName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public List<ScheduleDetailResponse> ScheduleDetails { get; set; } = [];
    }
}
