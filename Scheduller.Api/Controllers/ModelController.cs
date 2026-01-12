using Microsoft.AspNetCore.Mvc;
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
                status = "OK",
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
                status = "OK", 
                data = result 
            };

            return Ok(response);
        }
    }
}
