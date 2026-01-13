using Scheduller.Api.Domains.DTOs;

namespace Scheduller.Api.Services.Interfaces
{
    public interface IScheduleService
    {
        Task<IEnumerable<ScheduleResponse>> GetAllSchedule();
        Task<ScheduleResponse> GetScheduleById(int id);
        Task<ScheduleResponse> CreateSchedule (ScheduleCreateRequest request);
        Task<ScheduleResponse> UpdateSchedule (ScheduleUpdateRequest request);
    }
}
