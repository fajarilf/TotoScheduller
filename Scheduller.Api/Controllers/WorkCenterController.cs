using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scheduller.Api.Services.Interfaces;

namespace Scheduller.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkCenterController : ControllerBase
    {
        private readonly IWorkCenterService _service;

        public WorkCenterController(IWorkCenterService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllWorkCenter();

            var response = new
            {
                status = "Success",
                data = result
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("{work_center_id:int}")]
        public async Task<IActionResult> GetById(int work_center_id)
        {
            var result = await _service.GetWorkCenterById(work_center_id);

            var response = new
            {
                status = "Success",
                data = result
            };

            return Ok(response);
        }
    }
}
