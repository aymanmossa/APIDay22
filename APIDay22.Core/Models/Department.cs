using APIDay22.Core.ValidationAtributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APIDay22.Core.Models
{
    public class Department
    {
        [Key]
        public int DeptId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        [UniqueDepartmentName] 
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Manger is required")]
        [StringLength(100, ErrorMessage = "Manger cannot be longer than 100 characters")]
        [OnlyLettersAttributes] // allows letters + spaces only
        public string Manger { get; set; } = string.Empty;

        
        [Required(ErrorMessage = "Location is required")]
        [StringLength(50, ErrorMessage = "Location cannot be longer than 50 characters")]
        public string Location { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
