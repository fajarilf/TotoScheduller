using Scheduller.Api.Domains.Entities;

namespace Scheduller.Api.Domains.DTOs
{
    public static class PartDto
    {
        public static PartResponse toPartResponse (Part entity)
        {
            return new PartResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatedAt = entity.CreatedAt
            };
        }

        public static PartResponseRelation toPartResponseRelation (Part entity)
        {
            return new PartResponseRelation
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatedAt = entity.CreatedAt,
                ProcessDetails = [.. entity.ProcessDetails.Select(ProcessDetailDto.toProcessDetailResponse)],
                ProcessComponents = [.. entity.ProcessComponents.Select(ProcessComponentDto.toProcessComponentResponse)]
            };
        }
    }

    public class PartCreateRequest
    {
        public required string Name { get; set; }
    }

    public class PartUpdateRequest
    {
        public required int Id { get; set; }
        public string? Name { get; set; }
    }

    public class PartResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    public class PartResponseRelation
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<ProcessDetailResponse> ProcessDetails { get; set; } = [];
        public List<ProcessComponentResponse> ProcessComponents { get; set; } = [];
    }
}
