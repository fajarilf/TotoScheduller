using Scheduller.Api.Domains.Entities;

namespace Scheduller.Api.Domains.DTOs
{
    public static class ProcessComponentDto
    {
        public static ProcessComponentResponse toProcessComponentResponse (ProcessComponent entity)
        {
            return new ProcessComponentResponse
            {
                Id = entity.Id,
                PartName = entity.Part.Name,
                OperationNumber = entity.OperationNumber,
                WorkCenterName = entity.WorkCenter.Name,
                WorkCenterCategory = entity.WorkCenterCategory,
            };
        }

        public static ProcessComponentResponseRelation toComponentResponseRelation (ProcessComponent entity)
        {
            return new ProcessComponentResponseRelation
            {
                Id = entity.Id,
                OperationNumber = entity.OperationNumber,
                WorkCenterCategory = entity.WorkCenterCategory,
                BaseQuantity = entity.BaseQuantity,
                Setup = entity.Setup,
                CycleTime = entity.CycleTime,
                Part = PartDto.toPartResponse(entity.Part),
                WorkCenter = WorkCenterDto.toWorkCenterResponse(entity.WorkCenter),
            };
        }
    }

    public class ProcessComponentResponse 
    {
        public int Id { get; set; }
        public string PartName { get; set; } = string.Empty;
        public int OperationNumber { get; set; }
        public string WorkCenterName { get; set; } = string.Empty;
        public int WorkCenterCategory {  get; set; }
        public int BaseQuantity { get; set; }
        public int Setup { get; set; }
        public int CycleTime { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ProcessComponentResponseRelation
    {
        public int Id { get; set; }
        public int OperationNumber { get; set; }
        public int WorkCenterCategory { get; set; }
        public int BaseQuantity { get; set; }
        public int Setup { get; set; }
        public int CycleTime { get; set; }
        public PartResponse? Part { get; set; }
        public WorkCenterResponse? WorkCenter { get; set; }
    }
}
