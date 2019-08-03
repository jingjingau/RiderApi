using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RiderApi.Models;

namespace RiderApi.Services
{
    public interface IRiderService
    {
        Task<IEnumerable<Rider>> GetAllRidersAsync();

        Task<Rider> GetRiderAsync(int riderId);

        Task<Rider> AddRiderAsync(Rider rider);

        Task UpdateRiderAsync(Rider rider);

        Task<Rider> DeleteRiderAsync(int riderId);

    }
}
