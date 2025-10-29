using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDay22.Core.Models
{
    public class Student
    {

        [Key]
        public int Ssn { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        [RegularExpression(@"[A-Za-z\s]+", ErrorMessage = "Name must contain only letters and spaces")]
        public string Name { get; set; } = "";
        [Required(ErrorMessage = "Age is required")]
        [Range(18, 24, ErrorMessage = "Age must be between 18 and 24")]
        public int Age { get; set; }

        [RegularExpression(@"[A-Za-z\s]+", ErrorMessage = "Address must contain only letters and spaces")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Image is required")]
        [RegularExpression(@".*\.(jpg|png)$", ErrorMessage = "Only .jpg or .png images are allowed")]
        public string Image { get; set; } = "";

        [Required(ErrorMessage = "Gender is required")]
        [RegularExpression("[MF]", ErrorMessage = "Gender must be either 'M' or 'F'")]
        public string Gender { get; set; } = "";
    }
}
