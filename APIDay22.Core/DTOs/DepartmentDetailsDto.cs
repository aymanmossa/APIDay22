using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDay22.Core.DTOs
{
    public class DepartmentDetailsDto
    {
        public string Name { get; set; } = "";
        public string Manager { get; set; } = "";
        public string Location { get; set; } = "";
        public int NumOfStudents { get; set; }
        public string Color { get; set; } = "";
        public string Message { get; set; } = "";

    }
}

