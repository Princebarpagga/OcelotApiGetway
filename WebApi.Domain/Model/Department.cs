using System.ComponentModel.DataAnnotations;

namespace WebApi.Domain.Model
{
    public class Department
    {
        [Key]
        public int DepId { get; set; }

        public string DepartmentName { get; set; } = null!;
    }
   
}
