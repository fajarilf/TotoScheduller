using Scheduller.Api.Domains.Entities;

namespace Scheduller.Api.Domains.DTOs
{
    public static class ScheduleDetailDto
    {
        public static ScheduleDetailResponse toScheduleDetailResponse(ScheduleDetail entity)
        {
            return new ScheduleDetailResponse
            {
                Id = entity.Id,
                ScheduleId = entity.ScheduleId,
                PartName = entity.Part.Name,
                WorkCenterId = entity.WorkCenterId,
                WorkCenterName = $"{entity.WorkCenter.Name}-{entity.OperationNumber}",
                StartTime = entity.StartTime,
                FinishTime = entity.FinishTime,
            };
        }

        public static ScheduleDetailResponseTable toscheduleDetailResponseTable(ScheduleDetail entity)
        {
            return new ScheduleDetailResponseTable
            {
                Id = entity.Id,
                Schedule = ScheduleDto.toScheduleResponse(entity.Schedule),
                ModelName = entity.Schedule.Model.Name,
                PartName = entity.Part.Name,
                WorkCenterName = $"{entity.WorkCenter.Name}-{entity.OperationNumber}",
                StartTime = entity.StartTime,
                FinishTime = entity.FinishTime,
            };
        }
    }

    public class ScheduleDetailCreateRequest
    {
        public int ScheduleId { get; set; }
        public int PartId { get; set; }
        public int OperationNumber { get; set; }
        public int WorkCenterId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
    }

    public class ScheduleDetailResponseWithModel
    {
        public string ModelName { get; set; } = string.Empty;
        public List<ScheduleDetailResponse> ScheduleDetails { get; set; } = [];
    }

    public class ScheduleDetailResponseWithWorkCenter
    {
        public string WorkCenterName { get; set; } = string.Empty;
        public List<ScheduleDetailResponse> ScheduleDetails { get; set; } = [];
    }

    public class ScheduleDetailResponse
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public string PartName { get; set; } = string.Empty;
        public int WorkCenterId { get; set; }
        public string WorkCenterName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
    }

    public class ScheduleDetailResponseTable
    {
        public int Id { get; set; }
        public ScheduleResponse Schedule { get; set; } = new();
        public string ModelName { get; set; } = string.Empty;
        public string PartName { get; set; } = string.Empty;
        public string WorkCenterName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
    }
}
