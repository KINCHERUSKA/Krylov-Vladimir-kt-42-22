using Krylov_KT_42_22.Database;
using Krylov_KT_42_22.Filters.DepartmentFilters;
using Krylov_KT_42_22.Interfaces.DepartmentInterfaces;
using Krylov_KT_42_22.Models;
using Microsoft.EntityFrameworkCore;

namespace Krylov_KT_42_22.Interfaces.DepartmentInterfaces
{
    public interface IDepartmentServices
    {
        public Task<Department[]> GetDepartmentsByDate_Teachers_Async(DepartmentDateTeachersFilter filter, CancellationToken cancellationToken);
    }

    public class DepartmentService : IDepartmentServices
    {
        private readonly UniversityDbContext _dbContext;
        public DepartmentService(UniversityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Department[]> GetDepartmentsByDate_Teachers_Async(DepartmentDateTeachersFilter filter, CancellationToken cancellationToken = default)
        {
            var departments = _dbContext.Set<Department>().Where(w => w.FoundedDate == filter.FoundedDate)
                .Where(d => _dbContext.Teachers.Count(t => t.DepartmentId == d.Id) == filter.TeachersCount)
                .Include(d => d.Head)
                .Select(d => new Department
                {
                    Name = d.Name,
                    FoundedDate = d.FoundedDate,
                    Head = d.Head != null ? new Teacher
                    {
                        Id = d.Head.Id,
                        FirstName = d.Head.FirstName,
                        MiddleName = d.Head.MiddleName,
                        LastName = d.Head.LastName,
                    } : null
                })
                .ToArrayAsync(cancellationToken);
            return departments;
        }
    }
}
