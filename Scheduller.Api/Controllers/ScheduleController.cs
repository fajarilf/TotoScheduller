using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scheduller.Api.Domains.DTOs;
using Scheduller.Api.Services.Interfaces;

namespace Scheduller.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _service;

        public ScheduleController(IScheduleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllSchedule();

            var response = new
            {
                status = "Success",
                data = result
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("{schedule_id:int}")]
        public async Task<IActionResult> GetById(int schedule_id)
        {
            var result = await _service.GetScheduleById(schedule_id);

            var response = new
            {
                status = "Success",
                data = result
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchedule(ScheduleCreateRequest request)
        {
            var result = await _service.CreateSchedule(request);

            var response = new
            {
                status = "Success",
                data = result
            };

            return Ok(response);
        }
    }
}
