using RiderApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Clauses;

namespace RiderApi.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly RiderContext _context;

        public StatisticService(RiderContext context)
        {
            _context = context;
        }

        
        public async Task<IEnumerable<Job>> GetJobsByRiderIdAsync(int riderId)
        {
            return await _context.Jobs.Where(t => t.RiderId == riderId).ToListAsync();
        }

        //Get the statistics data for all riders
        public async Task<IEnumerable<Statistic>> GetRidersStatisticsAsync()
        {
            await EnsureDatabaseCreated();
            var totalAvgReviewScore = _context.Jobs.Average(job => job.ReviewScore);

            var avgReviewScoreTable = (from j in _context.Jobs
                                        group j by j.RiderId into g
                                        select new
                                        {
                                            RiderId = g.Key,
                                            AvgReviewScore = g.Average(s => s.ReviewScore)
                                        }).DefaultIfEmpty();
            var bestReviewScoreTable = (from j in _context.Jobs
                                        group j by j.RiderId into g
                                        select new
                                        {
                                            RiderId = g.Key,
                                            BestReviewScore = g.Max(s => s.ReviewScore)
                                        }).DefaultIfEmpty();
            var bestViewScoreCommentTable = (from j in _context.Jobs
                                        join best in bestReviewScoreTable on new {RiderId = j.RiderId, Score = j.ReviewScore} equals new { RiderId = best.RiderId, Score = best.BestReviewScore } 
                                        group j by j.RiderId into g
                                        from item in g
                                        select new
                                        {
                                            RiderId = g.Key,
                                            bestScore = item.ReviewScore,
                                            BestComment = item.ReviewComment
                                        }).DefaultIfEmpty();
            var avgBestScoreCommentJoinTable = (from a in avgReviewScoreTable
                                        join b in bestViewScoreCommentTable on a.RiderId equals b.RiderId
                                        select new
                                        {
                                            RiderId = a.RiderId,
                                            AvgReviewScore = a.AvgReviewScore,
                                            BestReViewScore = b.bestScore,
                                            BestViewComment = b.BestComment
                                        }).DefaultIfEmpty();

            var joinTable = (from r in _context.Riders
                            join s in avgBestScoreCommentJoinTable on r.Id equals s.RiderId into j
                            from item in j.DefaultIfEmpty()
                            select new Statistic
                            {
                                Id = r.Id,
                                FirstName = r.FirstName,
                                LastName = r.LastName,
                                AvgReviewScore = (item != null)? item.AvgReviewScore : 0,
                                BestReviewScore = (item != null)? item.BestReViewScore : 0,
                                ReviewComment = (item != null)? item.BestViewComment : "",
                                TotalAvgReviewScore = totalAvgReviewScore
                            }).DefaultIfEmpty();


            return await joinTable.ToListAsync();
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
