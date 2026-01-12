using Scheduller.Api.Domains.DTOs;
using Scheduller.Api.Domains.Entities;

namespace Scheduller.Api.Services.Interfaces
{
    public interface IModelService
    {
        Task<IEnumerable<ModelResponseRelation>> GetAllModel();
        Task<ModelResponseRelation?> GetModelById(int id);
    }
}
