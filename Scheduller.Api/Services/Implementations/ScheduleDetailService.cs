using Microsoft.EntityFrameworkCore;
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
        private readonly IProcessDetailService _processDetailService;

        public ScheduleDetailService(
            ScheduleDetailRepository repository,
            IProcessDetailService processDetailService
        )
        {
            _repository = repository;
            _processDetailService = processDetailService;
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

        public async Task<IEnumerable<ScheduleDetailResponseWithModel>> GetScheduleDetailByModel()
        {
            var result = await _repository.GetAll();
            var grouped = result.GroupBy(d => d.Schedule.ModelId);

            var response = new List<ScheduleDetailResponseWithModel>();

            foreach (var group in grouped)
            {
                var item = group.FirstOrDefault();
                if (item == null)
                    continue;

                response.Add(new ScheduleDetailResponseWithModel
                {
                    ModelName = item.Schedule.Model.Name,
                    ScheduleDetails = [.. group.Select(ScheduleDetailDto.toScheduleDetailResponse)]
                });
            }

            return response;
        }

        public async Task<ScheduleDetailResponseWithModel> GetScheduleDetailByModelId(int model_id)
        {
            var result = await _repository.DbSet
                .Include(sd => sd.Part)
                .Include(sd => sd.WorkCenter)
                .Include(sd => sd.Schedule)
                    .ThenInclude(sc => sc.Model)
                .Where(sd => sd.Schedule.ModelId == model_id)
                .ToListAsync();

            if (result == null)
                throw new ResponseException(System.Net.HttpStatusCode.NotFound, "Schedule detail not found");

            var first = result.FirstOrDefault();
            if (first == null)
                throw new ResponseException(System.Net.HttpStatusCode.NotFound, "Schedule detail not found");

            return new ScheduleDetailResponseWithModel
            {
                ModelName = first.Schedule.Model.Name,
                ScheduleDetails = [.. result.Select(ScheduleDetailDto.toScheduleDetailResponse)]
            };
        }

        public Task<IEnumerable<ScheduleDetailResponseWithModel>> GetScheduleDetailByWorkCenter()
        {
            throw new NotImplementedException();
        }

        public Task<ScheduleDetailResponseWithModel> GetScheduleDetailByWorkCenterId(int work_center_id)
        {
            throw new NotImplementedException();
        }
    }
}
