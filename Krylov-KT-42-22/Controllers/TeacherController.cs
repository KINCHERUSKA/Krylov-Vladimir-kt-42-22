using Microsoft.AspNetCore.Mvc;
using Krylov_KT_42_22.Interfaces;
using Krylov_KT_42_22.Filters.TeacherFilters;
using Krylov_KT_42_22.Interfaces.TeacherInterfaces;

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

        [HttpPost(Name = "GetTeachersByDepartment")]
        public async Task<IActionResult> GetTeachersByDepartmentAsync(TeacherDepartmentFilter filter, CancellationToken cancellationToken = default)
        {
            var teachers = await _teacherService.GetTeachersByDepartmentAsync(filter, cancellationToken);

            return Ok(teachers);
        }
    }
}
