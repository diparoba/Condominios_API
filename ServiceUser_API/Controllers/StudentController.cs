using Microsoft.AspNetCore.Mvc;
using ServiceUser_API.Models;
using ServiceUser_API.Services;

namespace ServiceUser_API.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase{
        private readonly IServiceStudent _serviceStudent;
        public StudentController(IServiceStudent serviceStudent){
            _serviceStudent = serviceStudent;
        }
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetStudentsAsync(){
            return await _serviceStudent.GetStudentsAsync();
        }
        [HttpGet("{id:length(24)}", Name = "GetStudent")]
        public async Task<ActionResult<User>> GetStudentAsync(string id){
            var student = await _serviceStudent.GetStudentAsync(id);
            if (student == null){
                return NotFound();
            }
            return student;
        }
        [HttpPost]
        public async Task<ActionResult<User>> CreateStudentAsync(User student){
            await _serviceStudent.CreateStudentAsync(student);
            return CreatedAtRoute("GetStudent", new { id = student.Id.ToString() }, student);
        }
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateStudentAsync(string id, User studentIn){
            var student = await _serviceStudent.GetStudentAsync(id);
            if (student == null){
                return NotFound();
            }
            await _serviceStudent.UpdateStudentAsync(id, studentIn);
            return NoContent();
        }
    }
}