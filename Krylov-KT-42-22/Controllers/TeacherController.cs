using Microsoft.AspNetCore.Mvc;

using Krylov_KT_42_22.Filters.TeacherFilters;
using Krylov_KT_42_22.Interfaces.TeacherInterfaces;
using Krylov_KT_42_22.Models;
using Krylov_KT_42_22.Models.DTO;

namespace Krylov_KT_42_22.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeachersController : ControllerBase
    {

        private readonly ILogger<TeachersController> _logger;
        private readonly ITeacherService _teacherService;

        public TeachersController(ILogger<TeachersController> logger, ITeacherService teacherService)
        {
            _logger = logger;
            _teacherService = teacherService;
        }

        [HttpPost("filter", Name = "GetTeachers")]
        public async Task<IActionResult> GetTeachersAsync(TeacherFilter filter, CancellationToken cancellationToken = default)
        {
            var teachers = await _teacherService.GetTeachersAsync(filter, cancellationToken);

            if (teachers == null || teachers.Length == 0)
            {
                return NotFound("Преподаватели не найдены");
            }

            return Ok(teachers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacherByIdAsync(int id, CancellationToken cancellationToken)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id, cancellationToken);
            if (teacher == null) return NotFound();
            return Ok(teacher);
        }

        [HttpPost("add", Name = "AddTeachers")]
        public async Task<IActionResult> AddTeacherAsync([FromBody] AddTeacherDto teacherDto, CancellationToken cancellationToken)
        {
            var teacher = new Teacher
            {
                FirstName = teacherDto.FirstName,
                LastName = teacherDto.LastName,
                DegreeId = teacherDto.DegreeId,
                PositionId = teacherDto.PositionId,
                DepartmentId = teacherDto.DepartmentId
            };

            var createdTeacher = await _teacherService.AddTeacherAsync(teacher, cancellationToken);
            return Ok(createdTeacher);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateTeacherAsync(int id, [FromBody] Teacher teacher, CancellationToken cancellationToken)
        //{
        //    if (id != teacher.Id)
        //        return BadRequest("ID в пути и в теле запроса не совпадают");

        //    var updatedTeacher = await _teacherService.UpdateTeacherAsync(teacher, cancellationToken);
        //    return Ok(updatedTeacher);
        //}

        // [HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTeacherAsync(int id, CancellationToken cancellationToken)
        //{
        //    var result = await _teacherService.DeleteTeacherAsync(id, cancellationToken);
        //    if (!result) return NotFound();
        //    return NoContent();
        //}
    }
}
