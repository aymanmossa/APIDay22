using APIDay22.Core.ValidationAtributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDay22.Core.Models
{
    public class Department
    {
        [Key]
        public int DeptId { get; set; }

        // Name => Custom Validation [Unique]
        [Required, StringLength(100)]
        [UniqueDepartmentName] // checks DB for duplicates
        public string Name { get; set; } = string.Empty;

        // Manger => [ONLY Letters]
        [Required, StringLength(100)]
        [OnlyLettersAttributes] // allows letters + spaces only
        public string Manger { get; set; } = string.Empty;

        // Location => validated via Action Filter (see below)
        [Required, StringLength(50)]
        public string Location { get; set; } = string.Empty;
    }
}
