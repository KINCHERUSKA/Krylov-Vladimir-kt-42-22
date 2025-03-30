using Krylov_KT_42_22.Filters.DepartmentFilters;
using Krylov_KT_42_22.Interfaces.DepartmentInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Krylov_KT_42_22.Controllers
{
    [ApiController]
    [Route("[controller]")]
        public class DepartmentsController : ControllerBase
        {
            private readonly ILogger<DepartmentsController> _logger;
            private readonly IDepartmentServices _departmentService;

            public DepartmentsController(ILogger<DepartmentsController> logger, IDepartmentServices departmentService)
            {
                _logger = logger;
                _departmentService = departmentService;
            }

            [HttpPost(Name = "GetDepartmentsByDate_Teachers")]
            public async Task<IActionResult> GetDepartmentsByDate_Teachers_Async(DepartmentDateTeachersFilter filter, CancellationToken cancellationToken = default)
            {
                var departments = await _departmentService.GetDepartmentsByDate_Teachers_Async(filter, cancellationToken);

            if (departments == null || departments.Length == 0)
            {
                return NotFound("Кафедры не найдены");
            }

                return Ok(departments);
            }
        }
}
