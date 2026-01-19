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
        public async Task<IActionResult> GetAll(
            [FromQuery] DateTime date,
            [FromQuery] int modelId = 0,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10
        )
        {
            var result = modelId == 0 ? 
                await _service.GetAllScheduleDetail(date, page, pageSize) : 
                await _service.GetAllScheduleDetailForTableWithModelId(date, modelId, page, pageSize);

            var response = new
            {
                status = "Success",
                data = result.Items,
                paging = new
                {
                    page = result.Page,
                    pageSize = result.PageSize,
                    totalCount = result.TotalCount
                }
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

        [HttpGet]
        [Route("models")]
        public async Task<IActionResult> GetByModelId()
        {
            var result =await _service.GetScheduleDetailByModel();

            var response = new
            {
                status = "Success",
                data = result
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("models/{model_id:int}")]
        public async Task<IActionResult> GetByModelId(int model_id)
        {
            var result = await _service.GetScheduleDetailByModelId(model_id);

            var response = new
            {
                status = "Success",
                data = result
            };

            return Ok(response);
        }

        [HttpDelete]
        [Route("{schedule_detail_id:int}")]
        public async Task<IActionResult> Delete(int schedule_detail_id)
        {
            await _service.Delete(schedule_detail_id);

            var response = new
            {
                status = "Success",
                data = "OK"
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("work_centers")]
        public async Task<IActionResult> GetByWorkCenter()
        {
            var result = await _service.GetScheduleDetailByWorkCenter();

            var response = new
            {
                status = "Success",
                data = result
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("work_centers/{work_center_id:int}")]
        public async Task<IActionResult> GetByWorkCenterId(int work_center_id)
        {
            var result = await _service.GetScheduleDetailByWorkCenterId(work_center_id);

            var response = new
            {
                status = "Success",
                data = result
            };

            return Ok(response);
        }
    }
}
