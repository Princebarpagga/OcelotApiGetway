using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Model;

namespace WebApi.Service.Interface
{
    public interface IStudentService
    {
        Task<ServiceResponse> GetStudentById(int id);
        Task<IEnumerable<StudentVm>> GetStudentsList();
        Task<ServiceResponse> AddStudent(StudentVm students);

        Task<ServiceResponse> AddDepartment(Department departments);
        Task<ServiceResponse> AddQualifications(Qualification qualifications);
    }
}
