using Krylov_KT_42_22.Database;
using Krylov_KT_42_22.Models;
using Krylov_KT_42_22.Filters.TeacherFilters;
using Microsoft.EntityFrameworkCore;


namespace Krylov_KT_42_22.Interfaces.TeacherInterfaces
{
    public interface ITeacherService
    {
        public Task<Teacher[]> GetTeachersByDepartmentAsync(TeacherDepartmentFilter filter, CancellationToken cancellationToken);
    }

    public class TeacherService : ITeacherService
    {
        private readonly UniversityDbContext _dbContext;
        public TeacherService(UniversityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Teacher[]> GetTeachersByDepartmentAsync(TeacherDepartmentFilter filter, CancellationToken cancellationToken = default)
        {
            var teachers = _dbContext.Set<Teacher>().Where(w => w.Department.Name == filter.Dep_Name && w.Position.Name == filter.Pos_Name && w.Degree.Name == filter.Deg_Name).ToArrayAsync(cancellationToken);
            return teachers;
        }
    }

    
}
