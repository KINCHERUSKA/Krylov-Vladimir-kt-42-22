using Krylov_KT_42_22.Database;
using Krylov_KT_42_22.Filters.LoadFilters;
using Krylov_KT_42_22.Filters.TeacherFilters;
using Krylov_KT_42_22.Interfaces.LoadInterfaces;
using Krylov_KT_42_22.Interfaces.TeacherInterfaces;
using Krylov_KT_42_22.Models;
using Krylov_KT_42_22.Models.DTO;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrylovVladimirKt_42_22.Tests
{
    public class LoadIntegrationTests
    {
        private readonly DbContextOptions<UniversityDbContext> _dbContextOptions;

        public LoadIntegrationTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<UniversityDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task GetLoadById_ReturnsCorrectResult()
        {
            var ctx = new UniversityDbContext(_dbContextOptions);
            var loadService = new LoadService(ctx);

            var Dep = new Department { Id = 1, Name = "ИВТ" };
            await ctx.Departments.AddAsync(Dep);

            var Degree = new Degree { Id = 1, Name = "Доктор наук" };
            await ctx.Degrees.AddAsync(Degree);

            var Position = new Position { Id = 1, Name = "Полотер" };
            await ctx.Positions.AddAsync(Position);

            var teachers = new Teacher { Id = 1, FirstName = "Иван", LastName = "Иванов", DegreeId = 1, PositionId = 1, DepartmentId = 1 };
            await ctx.Teachers.AddAsync(teachers);

            var dis = new Discipline { Id = 1, Name = "Проектный практикум" };
            await ctx.Disciplines.AddAsync(dis);

            var load = new Load {Id = 1, DisciplineId = 1, TeacherId = 1, Hours = 20 };
            await ctx.Loads.AddAsync(load);
            await ctx.SaveChangesAsync();

            var LoadId = 1;

            var loadResult = await loadService.GetLoadByIdAsync(LoadId, CancellationToken.None);

            Assert.NotNull(loadResult); // Проверяем, что объект не null
            Assert.Equal(1, loadResult.Id); // Проверяем ID
        }


        [Fact]
        public async Task AddLoadCorrectly()
        {
            var ctx = new UniversityDbContext(_dbContextOptions);
            var loadService = new LoadService(ctx);

            var Departments = new List<Department> {
                new Department { Id = 1, Name = "ИВТ" },
                new Department { Id = 2, Name = "РЭА" }
            };
            await ctx.Departments.AddRangeAsync(Departments);

            var Degree = new Degree { Id = 1, Name = "Доктор наук" };
            await ctx.Degrees.AddAsync(Degree);

            var teachers = new Teacher
            {
                Id = 1,
                FirstName = "Иван",
                LastName = "Иванов",
                DegreeId = 1,
                PositionId = 1,
                DepartmentId = 2
            };

            await ctx.Teachers.AddAsync(teachers);

            var dis = new Discipline { Id = 1, Name = "Проектный практикум" };
            await ctx.Disciplines.AddAsync(dis);

            var load = new AddLoadDto { DisciplineId = 1, TeacherId = 1, Hours = 20 };

            var result = await loadService.AddLoadAsync(load, CancellationToken.None);

            Assert.NotNull(load);
        }

        [Fact]
        public async Task UpdateLoad_WorksCorrectly()
        {
            var ctx = new UniversityDbContext(_dbContextOptions);
            var loadService = new LoadService(ctx);

            var Dep = new Department { Id = 1, Name = "ИВТ" };
            await ctx.Departments.AddAsync(Dep);

            var Degree = new Degree { Id = 1, Name = "Доктор наук" };
            await ctx.Degrees.AddAsync(Degree);

            var Position = new Position { Id = 1, Name = "Полотер" };
            await ctx.Positions.AddAsync(Position);

            var teachers = new Teacher { Id = 1, FirstName = "Иван", LastName = "Иванов", DegreeId = 1, PositionId = 1, DepartmentId = 1 };
            await ctx.Teachers.AddAsync(teachers);

            var dis = new Discipline { Id = 1, Name = "Проектный практикум" };
            await ctx.Disciplines.AddAsync(dis);

            var load = new Load { Id = 1, DisciplineId = 1, TeacherId = 1, Hours = 20 };
            await ctx.Loads.AddAsync(load);
            await ctx.SaveChangesAsync();

            var updatedLoad = new UpdateLoadDto { Id = 1, DisciplineId = 1, TeacherId = 1, Hours = 100 };

            // Act
            var result = await loadService.UpdateLoadAsync(updatedLoad, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(100, result.Hours);
        }

        [Fact]
        public async Task FilterByDepartment_ReturnsExpected()
        {
            var ctx = new UniversityDbContext(_dbContextOptions);
            var loadService = new LoadService(ctx);

            var Departments = new List<Department> {
                new Department { Id = 1, Name = "ИВТ" },
                new Department { Id = 2, Name = "РЭА" }
            };
            await ctx.Departments.AddRangeAsync(Departments);

            var Degree = new Degree { Id = 1, Name = "Доктор наук" };
            await ctx.Degrees.AddAsync(Degree);

            var Position = new Position { Id = 1, Name = "Полотер" };
            await ctx.Positions.AddAsync(Position);

            var teachers = new List<Teacher>
            {
                 new Teacher { Id = 1, FirstName = "Иван", LastName = "Иванов",
                    DegreeId = 1, PositionId = 1, DepartmentId = 1 },
                 new Teacher { Id = 2, FirstName = "Петр", LastName = "Петров",
                    DegreeId = 1, PositionId = 1, DepartmentId = 2 },
                 new Teacher { Id = 3, FirstName = "Сергей", LastName = "Сергеев",
                    DegreeId = 1, PositionId = 1, DepartmentId = 1 }
            };

            await ctx.Set<Teacher>().AddRangeAsync(teachers);

            var dis = new List<Discipline>
            {
                new Discipline { Id = 1, Name = "Проектный практикум"},
                new Discipline { Id = 2, Name= "Гумунитарный бред"}
            };

            await ctx.Set<Discipline>().AddRangeAsync(dis);

            var loads = new List<Load>
            {
                new Load { Id = 1, DisciplineId = 1, TeacherId = 1, Hours = 20},
                new Load { Id = 2, DisciplineId = 1, TeacherId = 2, Hours = 30},
                new Load { Id = 3, DisciplineId = 2, TeacherId = 2, Hours = 50},
                new Load { Id = 4, DisciplineId = 2, TeacherId = 3, Hours = 10}
            };
            await ctx.Set<Load>().AddRangeAsync(loads);
            await ctx.SaveChangesAsync();

            var filter = new LoadFilter { TeacherId = 1, DepartmentId = 1, DisciplineId = 1, MinHours = 10, MaxHours = 100 };
            var result = await loadService.GetLoadsAsync(filter, CancellationToken.None);

            var loadList = Assert.IsAssignableFrom<LoadDto[]>(result);
            Assert.Single(loadList);

        }
    }
}
