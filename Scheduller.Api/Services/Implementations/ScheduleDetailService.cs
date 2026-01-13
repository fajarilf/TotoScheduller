using Scheduller.Api.Domains.DTOs;
using Scheduller.Api.Domains.Entities;
using Scheduller.Api.Exceptions;
using Scheduller.Api.Repositories.Implementations;
using Scheduller.Api.Services.Interfaces;

namespace Scheduller.Api.Services.Implementations
{
    public class ScheduleDetailService : IScheduleDetailService
    {
        private readonly ScheduleDetailRepository _repository;

        public ScheduleDetailService(ScheduleDetailRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ScheduleDetailResponse>> CreateScheduleDetail(List<ScheduleDetailCreateRequest> request)
        {
            var inputs = request.Select(d => new ScheduleDetail
            {
                ScheduleId = d.ScheduleId,
                PartId = d.PartId,
                WorkCenterId = d.WorkCenterId,
                StartTime = d.StartTime,
                FinishTime = d.FinishTime,
            }).ToList();

            var scheduleDetails = await _repository.SaveRangeChages(inputs);

            return [.. scheduleDetails.Select(ScheduleDetailDto.toScheduleDetailResponse)];
        }

        public async Task<IEnumerable<ScheduleDetailResponse>> GetAllScheduleDetail()
        {
            var result = await _repository.GetAll();

            return [.. result.Select(ScheduleDetailDto.toScheduleDetailResponse)];
        }

        public async Task<ScheduleDetailResponse> GetScheduleDetailById(int id)
        {
            var result = await _repository.GetById(id);
            if (result == null)
                throw new ResponseException(System.Net.HttpStatusCode.NotFound, "ScheduleDetail not found");

            return ScheduleDetailDto.toScheduleDetailResponse(result);
        }
    }
}
