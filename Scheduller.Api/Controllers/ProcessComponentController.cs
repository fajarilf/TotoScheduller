using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scheduller.Api.Services.Interfaces;

namespace Scheduller.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessComponentController : ControllerBase
    {
        private readonly IProcessComponentService _service;

        public ProcessComponentController(IProcessComponentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllProcessComponent();

            var response = new
            {
                status = "Success",
                data = result
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("{process_component_id:int}")]
        public async Task<IActionResult> GetById(int process_component_id)
        {
            var result = await _service.GetProcessComponentById(process_component_id);

            var response = new
            {
                status = "Success",
                data = result
            };

            return Ok(response);
        }
    }
}
