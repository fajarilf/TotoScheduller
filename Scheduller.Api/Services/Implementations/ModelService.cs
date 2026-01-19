using Scheduller.Api.Domains.DTOs;
using Scheduller.Api.Repositories.Implementations;
using Scheduller.Api.Services.Interfaces;
using Scheduller.Api.Exceptions;
using Microsoft.EntityFrameworkCore;
using Scheduller.Api.Domains.Entities;

namespace Scheduller.Api.Services.Implementations
{
    public class ModelService : IModelService
    {
        private readonly ModelRepository _repository;

        public ModelService(ModelRepository repository)
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
                throw new ResponseException(System.Net.HttpStatusCode.BadRequest, "This model already exist");
        }

        public async Task<ModelResponse> CreateModel(ModelCreateRequest request)
        {
            await CheckExist(request.Name);

            var model = new Model
            {
                Name = request.Name,
            };

            model  = await _repository.SaveChanges(model);

            return ModelDto.toModelResponse(model);
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _repository.DbSet
                .FirstOrDefaultAsync(d => d.Id == id);

            if (result == null)
                throw new ResponseException(System.Net.HttpStatusCode.NotFound, "Model not found");

            await _repository.Remove(result);

            return true;
        }

        public async Task<IEnumerable<ModelResponseRelation>> GetAllModel()
        {
            var result = await _repository.GetAll();

            return [.. result.Select(ModelDto.toModelResponseRelation)];
        }

        public async Task<ModelResponseRelation?> GetModelById(int id)
        {
            var result = await _repository.GetById(id);
            if (result == null)
                throw new ResponseException(System.Net.HttpStatusCode.NotFound, "Model not found");

            return ModelDto.toModelResponseRelation(result);
        }

        public async Task<ModelResponse> UpdateModel(ModelUpdateRequest request, int id)
        {
            var model = await _repository.DbSet
                .FirstOrDefaultAsync(m => m.Id == id);

            if (model == null)
                throw new ResponseException(System.Net.HttpStatusCode.NotFound, "Model not found");

            if (!string.IsNullOrEmpty(request.Name))
            {
                await CheckExist(request.Name, model.Id);

                model.Name = request.Name;
            }

            model = await _repository.Update(model);

            return ModelDto.toModelResponse(model);
        }
    }
}
