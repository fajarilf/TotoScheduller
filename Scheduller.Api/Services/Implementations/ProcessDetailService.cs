using Microsoft.EntityFrameworkCore;
using Scheduller.Api.Domains.DTOs;
using Scheduller.Api.Exceptions;
using Scheduller.Api.Repositories.Implementations;
using Scheduller.Api.Services.Interfaces;

namespace Scheduller.Api.Services.Implementations
{
    public class ProcessDetailService : IProcessDetailService
    {
        private readonly ProcessDetailRepository _repository;

        public ProcessDetailService(ProcessDetailRepository repository)
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

        public async Task<IEnumerable<ProcessDetailResponseRelation>> GetAllProcessDetail()
        {
            var result = await _repository.GetAll();

            return [.. result.Select(ProcessDetailDto.toProcessDetailResponseRelation)];
        }

        public async Task<IEnumerable<ProcessDetailResponseRelation>> GetAllProcessDetailByModelId(int model_id)
        {
            var result = await _repository.DbSet
                .Where(d => d.ModelsId == model_id)
                .Include(d => d.Model)
                .Include(d => d.Part)
                .ToListAsync();

            return [.. result.Select(ProcessDetailDto.toProcessDetailResponseRelation)];
        }

        public async Task<ProcessDetailResponseRelation> GetProcessDetailById(int id)
        {
            var result = await _repository.GetById(id);
            if (result == null)
                throw new ResponseException(System.Net.HttpStatusCode.NotFound, "Process detail not found");

            return ProcessDetailDto.toProcessDetailResponseRelation(result);
        }
    }
}
