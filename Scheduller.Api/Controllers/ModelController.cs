using Microsoft.AspNetCore.Mvc;
using Scheduller.Api.Domains.DTOs;
using Scheduller.Api.Services.Interfaces;

namespace Scheduller.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private readonly IModelService _service;

        public ModelController(IModelService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll ()
        {
            var result = await _service.GetAllModel();

            var response = new
            {
                status = "Success",
                data = result
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("{model_id:int}")]
        public async Task<IActionResult> GetById(int model_id)
        {
            var result = await _service.GetModelById(model_id);

            var response = new
            {
                status = "Success", 
                data = result 
            };

            return Ok(response);
        }

        [HttpDelete]
        [Route("{model_id:int}")]
        public async Task<IActionResult> Delete(int model_id)
        {
            await _service.Delete(model_id);

            var response = new
            {
                status = "Success",
                data = "OK"
            };

            return Ok(response);
        }

        [HttpPatch]
        [Route("{model_id:int}")]
        public async Task<IActionResult> Update(ModelUpdateRequest request, int model_id)
        {
            var result = await _service.UpdateModel(request, model_id);

            var response = new
            {
                status = "Success",
                data = result
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ModelCreateRequest request)
        {
            var result = await _service.CreateModel(request);

            var response = new
            {
                status = "Success",
                data = result
            };

            return Ok(response);
        }
    }
}
