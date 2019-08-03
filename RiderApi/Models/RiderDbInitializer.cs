using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RiderApi.Models
{
    public class RiderDbInitializer
    {
        private static bool _databaseChecked = false;

        private readonly RiderContext _context;

        public RiderDbInitializer(RiderContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create and seed Database
        /// </summary>
        public async Task CreateAndSeedDatabase()
        {

            if (!_databaseChecked)
            {
                _databaseChecked = true;

                await _context.Database.MigrateAsync();

                if (!_context.Riders.Any())
                {
                    InputSampleDataForRiders();

                    InputSampleDataForJobs();

                    await _context.SaveChangesAsync();
                }

            }
        }

        /// <summary>
        /// Input Sample Data for Riders Table
        /// </summary>
        private void InputSampleDataForRiders()
        {
            var rider1 = new Rider
            {
                Id = 1,
                FirstName = "Tony",
                LastName = "Smith",
                PhoneNumber = "+(61)407-897-112",
                Email = "Tony.Smith@gmail.com",
                StartDate = DateTime.ParseExact("21/04/1999", "dd/MM/yyyy", CultureInfo.InvariantCulture)
            };
            var rider2 = new Rider
            {
                Id = 2,
                FirstName = "Kate",
                LastName = "Stone",
                PhoneNumber = "+(61)407-117-143",
                Email = "Kate61.Stone@gmail.com",
                StartDate = DateTime.ParseExact("11/04/2015", "dd/MM/yyyy", CultureInfo.InvariantCulture)
            };
            var rider3 = new Rider
            {
                Id = 3,
                FirstName = "Terry",
                LastName = "Cheung",
                PhoneNumber = "+(61)457-198-234",
                Email = "Cheung999@hotmail.com",
                StartDate = DateTime.ParseExact("25/11/2010", "dd/MM/yyyy", CultureInfo.InvariantCulture)
            };

            _context.Riders.AddRange(rider1, rider2, rider3);

        }

        /// <summary>
        /// Input Sample Data For PhoneNumbers Table
        /// </summary>
        private void InputSampleDataForJobs()
        {
            var job1 = new Job
            {
                Id = 1,
                JobDateTime = DateTime.ParseExact("11/04/2015 11:12:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                RiderId = 1,
                ReviewScore = 550,
                ReviewComment = "Very good",
                CompletedAt = "Flemington Racecourse"
            };
            var job2 = new Job
            {
                Id = 2,
                JobDateTime = DateTime.ParseExact("12/05/2016 15:20:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                RiderId = 1,
                ReviewScore = 780,
                ReviewComment = "Excellent",
                CompletedAt = "Flemington Racecourse"
            };
            var job3 = new Job
            {
                Id = 3,
                JobDateTime = DateTime.ParseExact("11/04/2017 14:12:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                RiderId = 1,
                ReviewScore = 850,
                ReviewComment = "Excellent",
                CompletedAt = "Flemington Racecourse"
            };
            var job4 = new Job
            {
                Id = 4,
                JobDateTime = DateTime.ParseExact("12/05/2016 13:20:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                RiderId = 2,
                ReviewScore = 580,
                ReviewComment = "Very Good",
                CompletedAt = "Flemington Racecourse"
            };
            var job5 = new Job
            {
                Id = 5,
                JobDateTime = DateTime.ParseExact("11/04/2017 15:20:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                RiderId = 2,
                ReviewScore = 680,
                ReviewComment = "Excellent",
                CompletedAt = "Flemington Racecourse"
            };
            var job6 = new Job
            {
                Id = 6,
                JobDateTime = DateTime.ParseExact("12/05/2016 14:20:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                RiderId = 3,
                ReviewScore = 780,
                ReviewComment = "Excellent",
                CompletedAt = "Flemington Racecourse"
            };
            var job7 = new Job
            {
                Id = 7,
                JobDateTime = DateTime.ParseExact("11/04/2017 14:40:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                RiderId = 3,
                ReviewScore = 750,
                ReviewComment = "Excellent",
                CompletedAt = "Flemington Racecourse"
            };
            _context.Jobs.AddRange(job1,job2,job3,job4,job5,job6,job7);
        }

    }
}
