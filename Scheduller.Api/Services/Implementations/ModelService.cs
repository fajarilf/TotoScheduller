using Scheduller.Api.Domains.DTOs;
using Scheduller.Api.Repositories.Implementations;
using Scheduller.Api.Services.Interfaces;
using Scheduller.Api.Exceptions;

namespace Scheduller.Api.Services.Implementations
{
    public class ModelService : IModelService
    {
        private readonly ModelRepository _repository;

        public ModelService(ModelRepository repository)
        {
            _repository = repository;
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
    }
}
