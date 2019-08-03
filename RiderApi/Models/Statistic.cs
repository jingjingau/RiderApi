using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiderApi.Models
{
    public class Statistic
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double AvgReviewScore { get; set; }
        public double BestReviewScore { get; set; }
        public string ReviewComment { get; set; }
        public double TotalAvgReviewScore { get; set; } 

    }
}
