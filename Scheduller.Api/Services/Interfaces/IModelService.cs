using Scheduller.Api.Domains.DTOs;
using Scheduller.Api.Domains.Entities;

namespace Scheduller.Api.Services.Interfaces
{
    public interface IModelService
    {
        Task<IEnumerable<ModelResponseRelation>> GetAllModel();
        Task<ModelResponseRelation?> GetModelById(int id);
        Task<bool> Delete(int id);
        Task<ModelResponse> CreateModel (ModelCreateRequest request);
        Task<ModelResponse> UpdateModel (ModelUpdateRequest request, int id);
    }
}
