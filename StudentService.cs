using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Model;
using WebApi.Service.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApi.Service.Service
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse> AddDepartment(Department departments)
        {
            var department = new Department
            {
                DepId= departments.DepId,
                DepartmentName = departments.DepartmentName
            };

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            
            var response = new ServiceResponse
            {
                data = department, 
                statuscode= 200,
                isSuccess = true
            };

            return response;
        }

        public async Task<ServiceResponse> AddQualifications(Qualification qualifications)
        {
            var qualification = new Qualification
            {
                QualifId = qualifications.QualifId,
                QualificationName = qualifications.QualificationName
            };

            _context.Qualifications.Add(qualification);
            await _context.SaveChangesAsync();


            var response = new ServiceResponse
            {
                data = qualifications,
                statuscode= 200,
                isSuccess = true
            };

            return response;
        }

        public async Task<ServiceResponse> AddStudent(StudentVm students)
        {
            var student = new Student
            {
                FirstName = students.FirstName,
                LastName = students.LastName,
                DepId = students.DepId,
                QualifId = students.QualifId,
                Percentage = students.Percentage
            };

            _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();


            var response = new ServiceResponse
            { 
                dataInfo = "Students add successfully",
                data = student,
                isSuccess = true
            };

            return response;
        }

        public async Task<ServiceResponse> GetStudentById(int id)
        {
            try
            {
                var student = await _context.Students.FindAsync(id);

                if (student == null)
                {
                    return new ServiceResponse
                    {
                        data = null,
                        dataInfo = "Student not found",
                        isSuccess = false
                    };
                }

                var studentvm = new StudentVm
                {
                    StudentId = student.StudentId,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    DepId = student.DepId,
                    QualifId = student.QualifId,
                    Percentage = student.Percentage,
                    DepartmentName = _context.Departments.FirstOrDefault(d => d.DepId == student.DepId)?.DepartmentName,
                    QualificationsName = _context.Qualifications.FirstOrDefault(s => s.QualifId == student.QualifId)?.QualificationName
                };

                return new ServiceResponse
                {
                    data = studentvm,
                    statuscode = 200,
                    dataInfo = "Student found successfully",
                    isSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse
                {
                    data = null,
                    statuscode = 200,
                    dataInfo = "Student  not found successfully",
                    isSuccess = true
                };
            }
        }
  
    public async Task<IEnumerable<StudentVm>> GetStudentsList()
        {
            var students = await _context.Students.ToListAsync();

            var studentvmList = students.Select(student => new StudentVm
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                DepId = student.DepId,
                QualifId = student.QualifId,
                Percentage = student.Percentage,
                DepartmentName = _context.Departments.FirstOrDefault(d => d.DepId == student.DepId)?.DepartmentName,
                QualificationsName = _context.Qualifications.FirstOrDefault(s => s.QualifId == student.QualifId)?.QualificationName
            }).ToList();

            return studentvmList;
        }
    }


  
}


