using Scheduller.Api.Domains.Entities;

namespace Scheduller.Api.Domains.DTOs
{
    public static class ModelDto
    {
        public static ModelResponseRelation toModelResponseRelation(Model entity)
        {
            return new ModelResponseRelation
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatedAt = entity.CreatedAt,
                ProcessDetails = [.. entity.ProcessDetails.Select(ProcessDetailDto.toProcessDetailResponse)]
            };
        }

        public static ModelResponse toModelResponse(Model entity)
        {
            return new ModelResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatedAt = entity.CreatedAt
            };
        }
    }

    public class ModelResponseRelation
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<ProcessDetailResponse> ProcessDetails { get; set; } = [];
    }

    public class ModelResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
