using Krylov_KT_42_22.Database;
using Krylov_KT_42_22.Filters.DisciplenFilters;
using Krylov_KT_42_22.Models;
using Microsoft.EntityFrameworkCore;

namespace Krylov_KT_42_22.Interfaces.DisciplineInterfaces
{
    public interface IDisciplineService
    {
        public Task<Load[]> GetDisciplinesAsync(DisciplinesTeacherFilter filter, CancellationToken cancellationToken);
    }

    public class DisciplineService : IDisciplineService
    {
        private readonly UniversityDbContext _dbContext;
        public DisciplineService(UniversityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Load[]> GetDisciplinesAsync(DisciplinesTeacherFilter filter, CancellationToken cancellationToken = default)
        {
            var disciplines = _dbContext.Set<Load>()
                .Where(w => w.TeacherId == filter.TeacherId)
                .Include(l => l.Teacher)
                 .Include(c => c.Teacher.Department)
                 .Include(c => c.Teacher.Position)
                 .Include(c => c.Teacher.Degree)

                 .Include(l => l.Discipline)
                 .Where(l => l.Hours >= filter.MinHours && l.Hours <= filter.MaxHours)
                 .ToArrayAsync(cancellationToken);
            return disciplines;
        }
    }
}
