using Microsoft.AspNetCore.Mvc;
using Krylov_KT_42_22.Filters.DisciplineFilters;
using Krylov_KT_42_22.Interfaces.DisciplineInterfaces;
using Krylov_KT_42_22.Models;
using Krylov_KT_42_22.Models.DTO;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Krylov_KT_42_22.Database;

namespace Krylov_KT_42_22.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisciplinesController : ControllerBase
    {
        private readonly ILogger<DisciplinesController> _logger;
        private readonly IDisciplineService _disciplineService;
        private readonly UniversityDbContext _dbContext;

        public DisciplinesController(
            ILogger<DisciplinesController> logger,
            IDisciplineService disciplineService,
            UniversityDbContext dbContext)
        {
            _logger = logger;
            _disciplineService = disciplineService;
            _dbContext = dbContext;
        }

        [HttpPost("filter")]
        public async Task<IActionResult> GetFilteredDisciplines(
            [FromBody] DisciplineFilter filter,
            CancellationToken cancellationToken)
        {
            var disciplines = await _disciplineService.GetDisciplinesAsync(filter, cancellationToken);
            return Ok(disciplines);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDisciplineById(int id, CancellationToken cancellationToken)
        {
            var discipline = await _disciplineService.GetDisciplineByIdAsync(id, cancellationToken);
            if (discipline == null) return NotFound();
            return Ok(discipline);
        }

        [HttpGet("departments-by-discipline")]
        public async Task<IActionResult> GetDepartmentsByDiscipline(
            [FromQuery] string disciplineName,
            [FromQuery] int minHours,
            [FromQuery] int maxHours,
            CancellationToken cancellationToken)
            {
                if (string.IsNullOrWhiteSpace(disciplineName))
                    return BadRequest("Название дисциплины обязательно");

                if (minHours < 0 || maxHours < 0 || minHours > maxHours)
                    return BadRequest("Некорректный диапазон часов");

                var departments = await _disciplineService.GetUniqueDepartmentsByDisciplineAndHoursAsync(
                    disciplineName, minHours, maxHours, cancellationToken);

                if (!departments.Any())
                    return NotFound("Факультеты по указанным критериям не найдены");

                return Ok(departments);
            }

        [HttpPost]
        public async Task<IActionResult> CreateDiscipline(
            [FromBody] AddDisciplineDto disciplineDto,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var discipline = new Discipline
            {
                Name = disciplineDto.Name
            };

            var createdDiscipline = await _disciplineService.AddDisciplineAsync(discipline, cancellationToken);
            return CreatedAtAction(nameof(GetDisciplineById), new { id = createdDiscipline.Id }, createdDiscipline);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiscipline(
            int id,
            [FromBody] UpdateDisciplineDto disciplineDto,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != disciplineDto.Id)
                return BadRequest("ID в пути и в теле запроса не совпадают");

            var discipline = new Discipline
            {
                Id = disciplineDto.Id,
                Name = disciplineDto.Name
            };

            var updatedDiscipline = await _disciplineService.UpdateDisciplineAsync(discipline, cancellationToken);
            return Ok(updatedDiscipline);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscipline(int id, CancellationToken cancellationToken)
        {
            var result = await _disciplineService.DeleteDisciplineAsync(id, cancellationToken);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}