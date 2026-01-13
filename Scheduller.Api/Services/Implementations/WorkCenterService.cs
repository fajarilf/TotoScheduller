using Microsoft.EntityFrameworkCore;
using Scheduller.Api.Domains.DTOs;
using Scheduller.Api.Exceptions;
using Scheduller.Api.Repositories.Implementations;
using Scheduller.Api.Services.Interfaces;

namespace Scheduller.Api.Services.Implementations
{
    public class WorkCenterService : IWorkCenterService
    {
        private readonly WorkCenterRepository _repository;

        public WorkCenterService(WorkCenterRepository repository)
        {
            _repository = repository;
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

        public async Task<IEnumerable<WorkCenterResponseRelation>> GetAllWorkCenter()
        {
            var result = await _repository.GetAll();

            return [.. result.Select(WorkCenterDto.toWorkCenterResponseRelation)];
        }

        public async Task<WorkCenterResponseRelation?> GetWorkCenterById(int id)
        {
            var result = await _repository.GetById(id);
            if (result == null)
                throw new ResponseException(System.Net.HttpStatusCode.NotFound, "Work center not found");

            return WorkCenterDto.toWorkCenterResponseRelation(result);
        }
    }
}
