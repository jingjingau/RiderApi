using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RiderApi.Models;
using RiderApi.Services;

namespace RiderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RidersApiController : ControllerBase
    {
        private readonly IRiderService _service;

        private readonly ILogger<RidersApiController> _logger;

        public RidersApiController(IRiderService service, ILogger<RidersApiController> logger = null)
        {
            _service = service;
            _logger = logger;
        }

        // GET: api/RidersApi
        [HttpGet]
        public async Task<IEnumerable<Rider>> GetRiders()
        {
            return  await _service.GetAllRidersAsync();
        }

        // GET: api/RidersApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetRider(int id)
        {
            var rider = await _service.GetRiderAsync(id);

            if (rider == null)
            {
                return NotFound();
            }

            return Ok(rider);
        }

        // PUT: api/RidersApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRider(int id, Rider rider)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (rider == null || id != rider.Id)
            {
                return BadRequest();
            }

            string info;
            try
            {
                await _service.UpdateRiderAsync(rider);
            }
            catch (Exception e)
            {
                info = $"------\r\n PutRider: Following Exception Occured {e.Message} when Updating rider: Id:{rider.Id}, FirstName:{rider.FirstName}, LastName:{rider.LastName}\r\n------";
                LogInformation(info);

                throw;
            }
            info = $"------\r\n PutRider: Following Rider Updated: Id:{rider.Id}, FirstName:{rider.FirstName}, LastName:{rider.LastName}\r\n------";
            LogInformation(info);

            return NoContent();
        }

        // POST: api/RidersApi
        [HttpPost]
        public async Task<ActionResult> PostRider(Rider rider)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (rider == null)
            {
                return BadRequest();
            }

            string info;
            try
            {
                rider = await _service.AddRiderAsync(rider);
            }
            catch (Exception e)
            {
                info = $"------\r\n PostRider: Following Exception Occured {e.Message} when adding phone number: Id:{rider.Id}, FirstName:{rider.FirstName}, LastName:{rider.LastName}\r\n------";
                LogInformation(info);

                throw;
            }

            info = $"------\r\n PostRider: Following Rider Added:  Id:{rider.Id}, FirstName:{rider.FirstName}, LastName:{rider.LastName}\r\n------";
            LogInformation(info);

            return CreatedAtAction("GetRider", new { rider.Id }, rider);

        }

        // DELETE: api/RidersApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRider(int id)
        {
            var rider = await _service.DeleteRiderAsync(id);
            if (rider == null)
            {
                return NotFound();
            }

            return Ok(rider);
        }

        private void LogInformation(string info)
        {
            if (_logger != null)
                _logger.LogInformation(info);

        }
    }
}
