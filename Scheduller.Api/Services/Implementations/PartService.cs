using Scheduller.Api.Domains.DTOs;
using Scheduller.Api.Repositories.Implementations;
using Scheduller.Api.Services.Interfaces;
using Scheduller.Api.Exceptions;

namespace Scheduller.Api.Services.Implementations
{
    public class PartService : IPartService
    {
        private readonly PartRepository _repository;

        public PartService(PartRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PartResponseRelation>> GetAllPart()
        {
            var result = await _repository.GetAll();

            return [.. result.Select(PartDto.toPartResponseRelation)];
        }

        public async Task<PartResponseRelation> GetPartById(int id)
        {
            var result = await _repository.GetById(id);
            if (result == null)
                throw new ResponseException(System.Net.HttpStatusCode.NotFound, "Part not found");

            return PartDto.toPartResponseRelation(result);
        }
    }
}
