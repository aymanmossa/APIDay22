using APIDay22.Core.Interfaces;
using APIDay22.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDay22.Core.ValidationAtributes
{
    public class UniqueDepartmentNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            var repo = validationContext.GetService(typeof(IBaseRepository<Department>)) as IBaseRepository<Department>;
            if (repo == null) 
                return new ValidationResult("Repository not found!");

            string name = value.ToString()!;
            var existing = repo.Find(d => d.Name.ToLower() == name.ToLower()).FirstOrDefault();
            if (existing != null)
                return new ValidationResult("Department already exists!");

            return ValidationResult.Success;
        }
    }
}
