using Scheduller.Api.Domains.Entities;

namespace Scheduller.Api.Domains.DTOs
{
    public static class WorkCenterDto
    {
        public static WorkCenterResponse toWorkCenterResponse (WorkCenter entity)
        {
            return new WorkCenterResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatedAt = entity.CreatedAt,
            };
        }

        public static WorkCenterResponseRelation toWorkCenterResponseRelation (WorkCenter entity)
        {
            return new WorkCenterResponseRelation
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatedAt = entity.CreatedAt,
                ProcessComponents = [.. entity.ProcessComponents.Select(ProcessComponentDto.toProcessComponentResponse)]
            };
        }
    }

    public class WorkCenterResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    public class WorkCenterResponseRelation
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<ProcessComponentResponse> ProcessComponents { get; set; } = []; 
    }
}
