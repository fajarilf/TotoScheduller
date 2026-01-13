using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scheduller.Api.Services.Interfaces;

namespace Scheduller.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartController : ControllerBase
    {
        private readonly IPartService _service;

        public PartController(IPartService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllPart();

            var response = new
            {
                status = "Success",
                data = result
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("{part_id:int}")]
        public async Task<IActionResult> GetById(int part_id)
        {
            var result = await _service.GetPartById(part_id);

            var response = new
            {
                status = "Success",
                data = result
            };

            return Ok(response);
        }

        [HttpDelete]
        [Route("{part_id:int}")]
        public async Task<IActionResult> Delete(int part_id)
        {
            await _service.Delete(part_id);

            var response = new
            {
                status = "Success",
                data = "OK"
            };

            return Ok(response);
        }
    }
}
