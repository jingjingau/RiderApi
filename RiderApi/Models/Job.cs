using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RiderApi.Models
{
    public class Job
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy HH:mm:ss}")]
        [Display(Name = "Date Time")]
        [DataType(DataType.DateTime), Required]
        public DateTime JobDateTime { get; set; }

        [Required]
        public int RiderId { get; set; }

        [Required]
        [Display(Name = "Review Score")]
        public int ReviewScore { get; set; }

        [Required]
        [Display(Name = "Review Comment")]
        public string ReviewComment { get; set; }

        [Required, MaxLength(200)]
        [Display(Name = "Completed At")]
        public string CompletedAt { get; set; }

        public virtual Rider Rider { get; set; }
    }
}
