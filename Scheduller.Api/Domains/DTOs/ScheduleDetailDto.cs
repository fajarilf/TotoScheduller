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
                WorkCenterName = entity.WorkCenter.Name,
                TimeLine = [.. Helper.MakeDateRange(entity.StartTime, entity.FinishTime)]
            };
        }
    }

    public class ScheduleDetailResponse
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public string PartName { get; set; } = string.Empty;
        public string WorkCenterName { get; set; } = string.Empty;
        public List<DateTime> TimeLine { get; set; } = [];
    }
}
