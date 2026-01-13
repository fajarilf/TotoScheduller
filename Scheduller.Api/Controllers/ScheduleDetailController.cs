using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scheduller.Api.Services.Interfaces;

namespace Scheduller.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleDetailController : ControllerBase
    {
        private readonly IScheduleDetailService _service;

        public ScheduleDetailController(IScheduleDetailService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllScheduleDetail();

            var response = new
            {
                status = "Success",
                data = result
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("{schedule_detail_id:int}")]
        public async Task<IActionResult> GetById(int schedule_detail_id)
        {
            var result = await _service.GetScheduleDetailById(schedule_detail_id);

            var response = new
            {
                status = "Success",
                data = result
            };

            return Ok(response);
        }
    }
}
