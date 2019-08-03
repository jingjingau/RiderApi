using RiderApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiderApi.Services
{
    public interface IStatisticService
    {
        Task<IEnumerable<Statistic>> GetRidersStatisticsAsync();
    }
}
