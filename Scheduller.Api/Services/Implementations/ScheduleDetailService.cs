using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Scheduller.Api.Domains.DTOs;
using Scheduller.Api.Domains.Entities;
using Scheduller.Api.Exceptions;
using Scheduller.Api.Repositories.Implementations;
using Scheduller.Api.Services.Interfaces;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
                OperationNumber = d.OperationNumber,
                WorkCenterId = d.WorkCenterId,
                StartTime = d.StartTime,
                FinishTime = d.FinishTime,
            }).ToList();

            var scheduleDetails = await _repository.SaveRangeChages(inputs);

            return [.. scheduleDetails.Select(ScheduleDetailDto.toScheduleDetailResponse)];
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _repository.DbSet
                .FirstOrDefaultAsync(d => d.Id == id);

            if (result == null)
                throw new ResponseException(System.Net.HttpStatusCode.NotFound, "Schedule detail not found");

            await _repository.Remove(result);

            return true;
        }

        public async Task<PagedResult<ScheduleDetailResponseTable>> GetAllScheduleDetail(DateTime date, int page, int pageSize)
        {
            var result = await _repository.GetAll(date, page, pageSize);
            var items = result.items.Select(ScheduleDetailDto.toscheduleDetailResponseTable);

            return Pagination.Paginante(items, page, pageSize, result.totalCount);
        }

        public async Task<PagedResult<ScheduleDetailResponseTable>> GetAllScheduleDetailForTableWithModelId(DateTime date, int model_id, int page, int pageSize)
        {
            var query = _repository.DbSet
                .Include(sd => sd.Part)
                .Include(sd => sd.WorkCenter)
                .Include(sd => sd.Schedule)
                    .ThenInclude(sc => sc.Model)
                .Where(sd => sd.Schedule.ModelId == model_id && sd.Schedule.CreatedAt!.Value.Date == date)
                .AsNoTracking();

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(sd => sd.StartTime)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var responseItems = items.Select(ScheduleDetailDto.toscheduleDetailResponseTable);

            return Pagination.Paginante(responseItems, page, pageSize, totalCount);
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
                    ScheduleDetails = [.. group.Select(ScheduleDetailDto.toScheduleDetailResponse).OrderBy(sd => sd.StartTime)]
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

        public async Task<IEnumerable<ScheduleDetailResponseWithWorkCenter>> GetScheduleDetailByWorkCenter()
        {
            var result = await _repository.GetAll();
            var grouped = result.GroupBy(d => d.WorkCenterId);

            var response = new List<ScheduleDetailResponseWithWorkCenter>();

            foreach (var group in grouped)
            {
                var item = group.FirstOrDefault();
                if (item == null)
                    continue;

                response.Add(new ScheduleDetailResponseWithWorkCenter
                {
                    WorkCenterName = item.WorkCenter.Name,
                    ScheduleDetails = [.. group.Select(ScheduleDetailDto.toScheduleDetailResponse)]
                });
            }

            return response;
        }

        public Task<ScheduleDetailResponseWithWorkCenter> GetScheduleDetailByWorkCenterId(int work_center_id)
        {
            throw new NotImplementedException();
        }
    }
}
