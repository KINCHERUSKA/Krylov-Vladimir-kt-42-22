using Krylov_KT_42_22.Filters.DisciplenFilters;
using Krylov_KT_42_22.Interfaces.DisciplineInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Krylov_KT_42_22.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DisciplinesController : ControllerBase
    {
        private readonly ILogger<DisciplinesController> _logger;
        private readonly IDisciplineService _disciplineService;

        public DisciplinesController(ILogger<DisciplinesController> logger, IDisciplineService disciplineService)
        {
            _logger = logger;
            _disciplineService = disciplineService;
        }

        [HttpPost(Name = "GetDisciplines")]
        public async Task<IActionResult> GetDisciplinesAsync(DisciplinesTeacherFilter filter, CancellationToken cancellationToken = default)
        {
            var disciplines = await _disciplineService.GetDisciplinesAsync(filter, cancellationToken);

            return Ok(disciplines);
        }
    }
}
