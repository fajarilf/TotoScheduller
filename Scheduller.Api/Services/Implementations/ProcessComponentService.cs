using Scheduller.Api.Domains.DTOs;
using Scheduller.Api.Repositories.Implementations;
using Scheduller.Api.Services.Interfaces;
using Scheduller.Api.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Scheduller.Api.Services.Implementations
{
    public class ProcessComponentService : IProcessComponentService
    {
        private readonly ProcessComponentRepository _repository;

        public ProcessComponentService(ProcessComponentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProcessComponentResponseRelation>> GetAllProcessComponent()
        {
            var result = await _repository.GetAll();

            return [.. result.Select(ProcessComponentDto.toComponentResponseRelation)];
        }

        public async Task<IEnumerable<ProcessComponentResponseRelation>> GetAllProcessComponentByPartId(int part_id)
        {
            var result = await _repository.DbSet
                .Where(d => d.PartsId == part_id)
                .Include(d => d.Part)
                .Include(d => d.WorkCenter)
                .ToListAsync();

            return [.. result.Select(ProcessComponentDto.toComponentResponseRelation)];
        }

        public async Task<ProcessComponentResponseRelation> GetProcessComponentById(int id)
        {
            var result = await _repository.GetById(id);
            if (result == null)
                throw new ResponseException(System.Net.HttpStatusCode.NotFound, "Process component not found");

            return ProcessComponentDto.toComponentResponseRelation(result);
        }
    }
}
