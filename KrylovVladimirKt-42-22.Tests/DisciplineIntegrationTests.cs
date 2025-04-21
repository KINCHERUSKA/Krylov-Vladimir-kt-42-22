using Krylov_KT_42_22.Database;
using Krylov_KT_42_22.Filters.DisciplineFilters;
using Krylov_KT_42_22.Filters.TeacherFilters;
using Krylov_KT_42_22.Interfaces.DisciplineInterfaces;
using Krylov_KT_42_22.Interfaces.TeacherInterfaces;
using Krylov_KT_42_22.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrylovVladimirKt_42_22.Tests
{
    public class DisciplineIntegrationTests
    {
        private readonly DbContextOptions<UniversityDbContext> _dbContextOptions;

        public DisciplineIntegrationTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<UniversityDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task GetDisciplineById_ReturnsCorrectResult()
        {
            var ctx = new UniversityDbContext(_dbContextOptions);
            var disciplineService = new DisciplineService(ctx);

            var disciplines = new List<Discipline>
            {
                 new Discipline { Id = 1, Name = "Русский"},
                 new Discipline { Id = 2, Name = "Матан"},
                 new Discipline { Id = 3, Name = "Физикагориматьтвою"}
            };

            await ctx.Set<Discipline>().AddRangeAsync(disciplines);
            await ctx.SaveChangesAsync();

            var disId = 2;

            var disResult = await disciplineService.GetDisciplineByIdAsync(disId, CancellationToken.None);

            Assert.NotNull(disResult); // Проверяем, что объект не null
            Assert.Equal(2, disResult.Id); // Проверяем ID
            Assert.Equal("Матан", disResult.Name); // Проверяем имя
        }

        [Fact]
        public async Task AddDiscipline_WorksCorrectly()
        {
            var ctx = new UniversityDbContext(_dbContextOptions);
            var disService = new DisciplineService(ctx);

            var dis = new Discipline
            {
                Id = 1,
                Name = "Матан"
            };

            var result = await disService.AddDisciplineAsync(dis, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal("Матан", result.Name);
        }

        [Fact]
        public async Task UpdateDiscipline_WorksCorrectly()
        {
            var ctx = new UniversityDbContext(_dbContextOptions);
            var disService = new DisciplineService(ctx);

            var dis = new Discipline
            {
                Id = 1,
                Name = "Старый",
            };

            await ctx.Disciplines.AddAsync(dis);
            await ctx.SaveChangesAsync();

            var updatedDis = new Discipline
            {
                Id = 1, // Тот же ID, что и у originalTeacher
                Name = "Обновлённый"
            };

            // Act
            var result = await disService.UpdateDisciplineAsync(updatedDis, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal("Обновлённый", result.Name);
        }

        [Fact]
        public async Task DeleteTeacher_WorksCorrectly()
        {
            var ctx = new UniversityDbContext(_dbContextOptions);
            var disService = new DisciplineService(ctx);

            var dis = new Discipline
            {
                Id = 1,
                Name = "Старый",
            };

            await ctx.Disciplines.AddAsync(dis);
            await ctx.SaveChangesAsync();

            var result = await disService.DeleteDisciplineAsync(1, CancellationToken.None);
            Assert.True(result);

            var deleted = await ctx.Teachers.FindAsync(1);
            Assert.Null(deleted);
        }

        [Fact]
        public async Task FilterByteachers_ReturnsExpected()
        {
            var ctx = new UniversityDbContext(_dbContextOptions);
            var disService = new DisciplineService(ctx);

            var Departments = new List<Department> {
                new Department { Id = 1, Name = "ИВТ" },
                    new Department { Id = 2, Name = "РЭА" }
            };
            await ctx.Departments.AddRangeAsync(Departments);

            var ivtPosition = new Position { Id = 1, Name = "Полотер" };
            await ctx.Positions.AddAsync(ivtPosition);

            var ivtDegree = new Degree { Id = 1, Name = "Доктор наук" };
            await ctx.Degrees.AddAsync(ivtDegree);

            var teachers = new List<Teacher>
            {
                 new Teacher { Id = 1, FirstName = "Иван", LastName = "Иванов",
                    DegreeId = 1, PositionId = 1, DepartmentId = 2 },
                 new Teacher { Id = 2, FirstName = "Петр", LastName = "Петров",
                    DegreeId = 1, PositionId = 1, DepartmentId = 1 },
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

            var filter = new DisciplineFilter { TeacherId = null, MinHours = 25, MaxHours = 60 }; ;
            var result = await disService.GetDisciplinesAsync(filter, CancellationToken.None);

            var disList = Assert.IsAssignableFrom<Discipline[]>(result);
            Assert.Equal(2, disList.Count());
        }

        [Fact]
        public async Task GetUniqueDepartmentsByDisciplineAndHoursAsync_ReturnsCorrectDepartments()
        {
            // Arrange
            var ctx = new UniversityDbContext(_dbContextOptions);
            var disciplineService = new DisciplineService(ctx);

            // Создаем тестовые данные
            var departments = new List<Department>
            {
                new Department { Id = 1, Name = "ИВТ" },
                new Department { Id = 2, Name = "РЭА" },
                new Department { Id = 3, Name = "ФУиСТ" }
            };
            await ctx.Departments.AddRangeAsync(departments);

            var position = new Position { Id = 1, Name = "Профессор" };
            await ctx.Positions.AddAsync(position);

            var degree = new Degree { Id = 1, Name = "Доктор наук" };
            await ctx.Degrees.AddAsync(degree);

            var teachers = new List<Teacher>
            {
                new Teacher { Id = 1, FirstName = "Иван", LastName = "Иванов",
                     DegreeId = 1, PositionId = 1, DepartmentId = 1 },
                new Teacher { Id = 2, FirstName = "Петр", LastName = "Петров",
                     DegreeId = 1, PositionId = 1, DepartmentId = 2 },
                new Teacher { Id = 3, FirstName = "Сергей", LastName = "Сергеев",
                     DegreeId = 1, PositionId = 1, DepartmentId = 1 }
            };
            await ctx.Teachers.AddRangeAsync(teachers);

            var disciplines = new List<Discipline>
            {
                new Discipline { Id = 1, Name = "Анализ математический" },
                new Discipline { Id = 2, Name = "Линейная алгебра" },
                new Discipline { Id = 3, Name = "Дискретная математика" }
            };
            await ctx.Disciplines.AddRangeAsync(disciplines);

            var loads = new List<Load>
            {
                new Load { Id = 1, DisciplineId = 1, TeacherId = 1, Hours = 30 },
                new Load { Id = 2, DisciplineId = 1, TeacherId = 2, Hours = 40 }, // Петров Петр - Лин 
                new Load { Id = 3, DisciplineId = 2, TeacherId = 1, Hours = 20 },
                new Load { Id = 4, DisciplineId = 3, TeacherId = 3, Hours = 50 }
            };
            await ctx.Loads.AddRangeAsync(loads);
            await ctx.SaveChangesAsync();

            var result = await disciplineService.GetUniqueDepartmentsByDisciplineAndHoursAsync("Анализ",20,50,CancellationToken.None);
            Assert.NotNull(result);
            Assert.Equal(2, result.Count); // Ожидаем 2 факультета (ИВТ и РЭА)
            Assert.Contains("ИВТ", result);
            Assert.Contains("РЭА", result);
            Assert.DoesNotContain("ФУиСТ", result); // Этот факультет не должен быть в результатах
        }

        [Fact]
        public async Task GetUniqueDepartmentsByDisciplineAndHoursAsync_ReturnsEmpty_WhenNoMatches()
        {
            // Arrange
            var ctx = new UniversityDbContext(_dbContextOptions);
            var disciplineService = new DisciplineService(ctx);

            // Act
            // Ищем несуществующую дисциплину
            var result = await disciplineService.GetUniqueDepartmentsByDisciplineAndHoursAsync(
                "Физика",
                10,
                100,
                CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
