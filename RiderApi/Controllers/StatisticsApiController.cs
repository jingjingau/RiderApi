﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiderApi.Models;
using RiderApi.Services;

namespace RiderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticService _service;

        public StatisticsApiController(IStatisticService service)
        {
            _service = service;
        }

        // GET: api/StatisticsApi
        [HttpGet]
        public async Task<IEnumerable<Statistic>> GetStatistics()
        {
            return await _service.GetRidersStatisticsAsync();
        }

    }
}
