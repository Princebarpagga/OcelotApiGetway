using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.Model;
using WebApi.Service.Interface;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<StudentVm>> GetStudentById(int id)
        {
            var studentvm = await _studentService.GetStudentById(id);

            if (studentvm == null)
            {
                return NotFound();
            }

            return Ok(studentvm);
        }
        [HttpGet("StudentsList")]
       [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsList()
        {
            var studentsList = await _studentService.GetStudentsList();
            return Ok(studentsList);
        }
        [HttpPost("AddStudent")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<ServiceResponse>> AddStudent([FromBody] StudentVm studentDto)
        {
            var serviceResponse = await _studentService.AddStudent(studentDto);

            if (!serviceResponse.isSuccess)
            {
                return BadRequest();
            }

            return Ok(serviceResponse);
        }


        [HttpPost("AddDepartment")]
        public async Task<ActionResult<ServiceResponse>> AddDepartment(Department departmentDto)
        {
            var serviceResponse = await _studentService.AddDepartment(departmentDto);

            if (!serviceResponse.isSuccess)
            {
                return BadRequest(); 
            }

            return Ok(serviceResponse);
        }


        [HttpPost("AddQualifications")]
        public async Task<ActionResult<ServiceResponse>> AddQualifications(Qualification qualification)
        {
            var serviceResponse = await _studentService.AddQualifications(qualification);

            if (!serviceResponse.isSuccess)
            {
                return BadRequest();
            }

            return Ok(serviceResponse);
        }
    }
}

