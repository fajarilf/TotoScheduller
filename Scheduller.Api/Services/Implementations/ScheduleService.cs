using Microsoft.EntityFrameworkCore;
using Scheduller.Api.Domains.DTOs;
using Scheduller.Api.Domains.Entities;
using Scheduller.Api.Exceptions;
using Scheduller.Api.Repositories.Implementations;
using Scheduller.Api.Services.Interfaces;

namespace Scheduller.Api.Services.Implementations
{
    public class ScheduleService : IScheduleService
    {
        private readonly ScheduleRepository _repository;
        private readonly IModelService _modelService;
        private readonly IProcessDetailService _processDetailService;
        private readonly IProcessComponentService _processComponentService;
        private readonly IScheduleDetailService _scheduleDetailService;

        public ScheduleService(
            ScheduleRepository repository,
            IModelService modelService,
            IProcessDetailService processDetailService, 
            IProcessComponentService processComponentService, 
            IScheduleDetailService scheduleDetailService)
        {
            _repository = repository;
            _modelService = modelService;
            _processDetailService = processDetailService;
            _processComponentService = processComponentService;
            _scheduleDetailService = scheduleDetailService;
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _repository.DbSet
                .FirstOrDefaultAsync(d => d.Id == id);

            if (result == null)
                throw new ResponseException(System.Net.HttpStatusCode.NotFound, "Process component not found");

            await _repository.Remove(result);

            return true;
        }

        public async Task<ScheduleResponse> CreateSchedule(ScheduleCreateRequest request)
        {
            var model = await _modelService.GetModelById(request.ModelId);
            if (model == null)
                throw new ResponseException(System.Net.HttpStatusCode.NotFound, "Model not found");

            var processDetail = await _processDetailService.GetAllProcessDetailByModelId(request.ModelId);
            if (!processDetail.Any())
                return new ScheduleResponse();

            var schedule = new Schedule
            {
                ModelId = request.ModelId,
                Quantity = request.Quantity,
            };

            schedule = await _repository.SaveChanges(schedule);

            var scheduleDetails = new List<ScheduleDetailCreateRequest>();
            var nextStart = DateTime.Now;

            processDetail = processDetail.OrderByDescending(pd => pd.Bom);

            foreach (var item in processDetail)
            {
                var processComponentResponse = await _processComponentService.GetAllProcessComponentByPartId(item.Part.Id);
                if (!processComponentResponse.Any())
                    continue;

                foreach (var component in processComponentResponse)
                {
                    var start = nextStart;
                    var time = component.CycleTime * request.Quantity;
                    var finish = start.AddSeconds(time);

                    scheduleDetails.Add(new ScheduleDetailCreateRequest
                    {
                        ScheduleId = schedule.Id,
                        PartId = item.Part.Id,
                        WorkCenterId = component.WorkCenter!.Id,
                        StartTime = start,
                        FinishTime = finish,
                    });

                    nextStart = finish;
                }
            }

            await _scheduleDetailService.CreateScheduleDetail(scheduleDetails);

            return ScheduleDto.toScheduleResponse(schedule);
        }

        public async Task<IEnumerable<ScheduleResponse>> GetAllSchedule()
        {
            var result = await _repository.GetAll();

            return [.. result.Select(ScheduleDto.toScheduleResponse)];
        }

        public async Task<ScheduleResponse> GetScheduleById(int id)
        {
            var result = await _repository.GetById(id);
            if (result == null)
                throw new ResponseException(System.Net.HttpStatusCode.NotFound, "Schedule not found");

            return ScheduleDto.toScheduleResponse(result);
        }

        public Task<ScheduleResponse> UpdateSchedule(ScheduleUpdateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
