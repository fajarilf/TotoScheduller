using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scheduller.Api.Services.Interfaces;

namespace Scheduller.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessDetailController : ControllerBase
    {
        private readonly IProcessDetailService _service;

        public ProcessDetailController(IProcessDetailService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllProcessDetail();

            var response = new
            {
                status = "Success",
                data = result
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("{process_detail_id:int}")]
        public async Task<IActionResult> GetById(int process_detail_id)
        {
            var result = await _service.GetProcessDetailById(process_detail_id);

            var response = new
            {
                status = "Success",
                data = result
            };

            return Ok(response);
        }
    }
}
