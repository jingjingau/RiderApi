using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RiderApi.Models;

namespace RiderApi.Services
{
    public class RiderService : IRiderService
    {
        private readonly RiderContext _context;

        public RiderService(RiderContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rider>> GetAllRidersAsync()
        {
            await EnsureDatabaseCreated();
            var riderList = await _context.Riders.ToListAsync();
            return riderList.Select(x => { x.Jobs = null; return x; }).ToList();
        }

        public async Task<Rider> GetRiderAsync(int riderId)
        {
            return await _context.Riders.FindAsync(riderId); 
        }

        public async Task<Rider> AddRiderAsync(Rider rider)
        {
            rider.Id = _context.Riders.Max(c => c.Id) + 1;

            _context.Riders.Add(rider);

            await _context.SaveChangesAsync();

            return rider;
        }

        public async Task UpdateRiderAsync(Rider rider)
        {
            _context.Riders.Update(rider);
            await _context.SaveChangesAsync();
        }

        public async Task<Rider> DeleteRiderAsync(int riderId)
        {
            var rider = await _context.Riders.FindAsync(riderId);
            if (rider == null)
            {
                return null;
            }

            _context.Riders.Remove(rider);
            await _context.SaveChangesAsync();
            return rider;
        }

        /// <summary>
        ///  Ensure Database Created.
        /// </summary>
        /// <returns></returns>
        private async Task EnsureDatabaseCreated()
        {
            var init = new RiderDbInitializer(_context);
            await init.CreateAndSeedDatabase();
        }

    }
}
