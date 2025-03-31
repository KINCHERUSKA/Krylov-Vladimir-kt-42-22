using Krylov_KT_42_22.Database;
using Krylov_KT_42_22.Filters.LoadFilter;
using Krylov_KT_42_22.Interfaces.LoadInterfaces;
using Krylov_KT_42_22.Models;
using Microsoft.EntityFrameworkCore;

namespace Krylov_KT_42_22.Interfaces.LoadInterfaces
{
    public interface ILoadService
    {
        public Task<Load[]> GetLoads(LoadTeacherTimeFilter filter, CancellationToken cancellationToken);
    }

    public class LoadService : ILoadService
    {
        private readonly UniversityDbContext _dbContext;
        public LoadService(UniversityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Load[]> GetLoads(LoadTeacherTimeFilter filter, CancellationToken cancellationToken = default)
        {

            var loads = _dbContext.Set<Load>()
                .Where(w => w.Teacher.Id == filter.TeacherId 
                    && w.Discipline.Id == filter.DisciplineId 
                    && w.Teacher.Department.Id == filter.DepartmentId)
                .Include(u => u.Teacher) // Загружаем преподавателя
                .Include(l => l.Teacher.Department)
                .Include(l => l.Discipline) // Загружаем дисциплину
                .ToArrayAsync(cancellationToken);

            return loads;
        }
    }
}
