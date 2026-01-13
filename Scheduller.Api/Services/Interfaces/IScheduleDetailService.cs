using Scheduller.Api.Domains.DTOs;

namespace Scheduller.Api.Services.Interfaces
{
    public interface IScheduleDetailService
    {
        Task<IEnumerable<ScheduleDetailResponse>> GetAllScheduleDetail();
        Task<ScheduleDetailResponse> GetScheduleDetailById(int id);
        Task<IEnumerable<ScheduleDetailResponse>> CreateScheduleDetail(List<ScheduleDetailCreateRequest> request);
    }
}
