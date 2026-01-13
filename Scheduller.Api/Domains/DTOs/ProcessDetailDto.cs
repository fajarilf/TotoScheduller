using Scheduller.Api.Domains.Entities;

namespace Scheduller.Api.Domains.DTOs
{
    public static class ProcessDetailDto
    {
        public static ProcessDetailResponse toProcessDetailResponse (ProcessDetail entity)
        {
            return new ProcessDetailResponse
            {
                Id = entity.Id,
                ModelName = entity.Model.Name,
                PartName = entity.Part.Name,
                OperationNumber = entity.OperationNumber,
                Bom = entity.Bom,
                CreatedAt = entity.CreatedAt
            };
        }

        public static ProcessDetailResponseRelation toProcessDetailResponseRelation (ProcessDetail entity)
        {
            return new ProcessDetailResponseRelation
            {
                Id = entity.Id,
                Bom = entity.Bom,
                OperationNumber = entity.OperationNumber,
                CreatedAt = entity.CreatedAt,
                Model = ModelDto.toModelResponse(entity.Model),
                Part = PartDto.toPartResponse(entity.Part)
            };
        }
    }

    public class ProcessDetailResponseRelation
    {
        public int Id { get; set; }
        public int Bom { get; set; }
        public int OperationNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public ModelResponse Model { get; set; } = new ModelResponse();
        public PartResponse Part { get; set; } = new PartResponse();
    }

    public class ProcessDetailResponse
    {
        public int Id { get; set; }
        public string ModelName { get; set; } = string.Empty;
        public string PartName { get; set; } = string.Empty;
        public int OperationNumber { get; set; }
        public int Bom { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}