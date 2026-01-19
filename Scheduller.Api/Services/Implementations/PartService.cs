using Microsoft.EntityFrameworkCore;
using Scheduller.Api.Domains.DTOs;
using Scheduller.Api.Domains.Entities;
using Scheduller.Api.Exceptions;
using Scheduller.Api.Repositories.Implementations;
using Scheduller.Api.Services.Interfaces;

namespace Scheduller.Api.Services.Implementations
{
    public class PartService : IPartService
    {
        private readonly PartRepository _repository;

        public PartService(PartRepository repository)
        {
            _repository = repository;
        }

        private async Task CheckExist(string name, int? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;

            var normalized = name.Trim().ToLowerInvariant();

            var exist = await _repository.DbSet
                .FirstOrDefaultAsync(m => m.Name.ToLower() == normalized && (!excludeId.HasValue || m.Id != excludeId.Value));

            if (exist != null)
                throw new ResponseException(System.Net.HttpStatusCode.BadRequest, "This part already exist");
        }

        public async Task<PartResponse> CreatePart(PartCreateRequest request)
        {
            await CheckExist(request.Name);

            var part = new Part
            {
                Name = request.Name,
            };

            part = await _repository.SaveChanges(part);

            return PartDto.toPartResponse(part);
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _repository.DbSet
                .FirstOrDefaultAsync(d => d.Id == id);

            if (result == null)
                throw new ResponseException(System.Net.HttpStatusCode.NotFound, "Part not found");

            await _repository.Remove(result);

            return true;
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

        public Task<PartResponse> UpdatePart(PartUpdateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
