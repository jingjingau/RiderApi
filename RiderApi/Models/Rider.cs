using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RiderApi.Models
{
    public class Rider
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(20), Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(20), Required]
        public string LastName { get; set; }

        [Required,MaxLength(100)]
        [RegularExpression(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$", ErrorMessage = "Invalid Phone Number!")]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required,MaxLength(200)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Invalid Email!")]
        public string Email { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date), Required]
        public DateTime StartDate { get; set; }

        public virtual List<Job> Jobs { get; set; }
    }
}
