using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Domain.Model
{
    public class Student
    {
        public int StudentId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public int DepId { get; set; }

        public int QualifId { get; set; }

        public decimal Percentage { get; set; }
       
    }
    public class StudentVm
    {
        public int StudentId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public int DepId { get; set; }

        public int QualifId { get; set; }

        public decimal Percentage { get; set; }
        public string? DepartmentName { get; set; }
        public string? QualificationsName { get; set; }
    }
   
}
