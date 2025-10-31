using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDay22.Core.DTOs
{
    public class StudentDetailsDto
    {
        public string Name { get; set; } = "";
        public string Dept { get; set; } = "";
        public string Manager { get; set; } = "";
        public DateTime Date { get; set; } = DateTime.Now;
        public string Address { get; set; } = "";
    }
}
