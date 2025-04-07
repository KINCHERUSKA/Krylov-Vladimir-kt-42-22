using Krylov_KT_42_22.Database;
using Krylov_KT_42_22.Filters.TeacherFilters;
using Krylov_KT_42_22.Interfaces.TeacherInterfaces;
using Krylov_KT_42_22.Models;
using Krylov_KT_42_22.Models.DTO;
using Krylov_KT_42_22.Filters.TeacherFilters;


using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using NLog.Filters;

namespace KrylovVladimirKt_42_22.Tests
{
    public class TeachersIntegrationTests
    {
        private readonly DbContextOptions<UniversityDbContext> _dbContextOptions;

        public TeachersIntegrationTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<UniversityDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task GetTeacherById_ReturnsCorrectResult()
        {
            var ctx = new UniversityDbContext(_dbContextOptions);
            var teacherService = new TeacherService(ctx);

            var ivtDepartment = new Department { Id = 1, Name = "ИВТ" };
            await ctx.Departments.AddAsync(ivtDepartment);

            var ivtPosition = new Position { Id = 1, Name = "Полотер" };
            await ctx.Positions.AddAsync(ivtPosition);

            var ivtDegree = new Degree { Id = 1, Name = "Доктор наук" };
            await ctx.Degrees.AddAsync(ivtDegree);

            var teachers = new List<Teacher>
            {
                 new Teacher { Id = 1, FirstName = "Иван", LastName = "Иванов",
                    DegreeId = 1, PositionId = 1, DepartmentId = 1 },
                 new Teacher { Id = 2, FirstName = "Петр", LastName = "Петров",
                    DegreeId = 1, PositionId = 1, DepartmentId = 1 },
                 new Teacher { Id = 3, FirstName = "Сергей", LastName = "Сергеев",
                    DegreeId = 1, PositionId = 1, DepartmentId = 1 }
            };

            await ctx.Set<Teacher>().AddRangeAsync(teachers);
            await ctx.SaveChangesAsync();

            var teacherId = 1;

            var teachersResult = await teacherService.GetTeacherByIdAsync(teacherId, CancellationToken.None);

            Assert.NotNull(teachersResult); // Проверяем, что объект не null
            Assert.Equal(1, teachersResult.Id); // Проверяем ID
            Assert.Equal("Иван", teachersResult.FirstName); // Проверяем имя
        }

        [Fact]
        public async Task AddTeacher_WorksCorrectly()
        {
            var ctx = new UniversityDbContext(_dbContextOptions);
            var teacherService = new TeacherService(ctx);

            var ivtDepartment = new Department { Id = 1, Name = "ИВТ" };
            await ctx.Departments.AddAsync(ivtDepartment);

            var ivtPosition = new Position { Id = 1, Name = "Полотер" };
            await ctx.Positions.AddAsync(ivtPosition);

            var ivtDegree = new Degree { Id = 1, Name = "Доктор наук" };
            await ctx.Degrees.AddAsync(ivtDegree);

            var teacher = new Teacher
            {
                FirstName = "Николай",
                LastName = "Новиков",
                DegreeId = 1,
                PositionId = 1,
                DepartmentId = 1
            };

            var result = await teacherService.AddTeacherAsync(teacher, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal("Николай", result.FirstName);
        }

        [Fact]
        public async Task UpdateTeacher_WorksCorrectly()
        {
            var ctx = new UniversityDbContext(_dbContextOptions);
            var teacherService = new TeacherService(ctx);

            var ivtDepartment = new Department { Id = 1, Name = "ИВТ" };
            await ctx.Departments.AddAsync(ivtDepartment);

            var ivtPosition = new Position { Id = 1, Name = "Полотер" };
            await ctx.Positions.AddAsync(ivtPosition);

            var ivtDegree = new Degree { Id = 1, Name = "Доктор наук" };
            await ctx.Degrees.AddAsync(ivtDegree);

            var teacher = new Teacher
            {
                FirstName = "Обновлённый",
                LastName = "Преподаватель",
                DegreeId = 1,
                PositionId = 1,
                DepartmentId = 1
            };


            var result = await teacherService.UpdateTeacherAsync(teacher, CancellationToken.None);
            Assert.NotNull(result);
            Assert.Equal("Обновлённый", result.FirstName);
        }

        [Fact]
        public async Task DeleteTeacher_WorksCorrectly()
        {
            var ctx = new UniversityDbContext(_dbContextOptions);
            var teacherService = new TeacherService(ctx);

            var ivtDepartment = new Department { Id = 1, Name = "ИВТ" };
            await ctx.Departments.AddAsync(ivtDepartment);

            var ivtPosition = new Position { Id = 1, Name = "Полотер" };
            await ctx.Positions.AddAsync(ivtPosition);

            var ivtDegree = new Degree { Id = 1, Name = "Доктор наук" };
            await ctx.Degrees.AddAsync(ivtDegree);

            var teacher = new Teacher
            {
                FirstName = "Обновлённый",
                LastName = "Преподаватель",
                DegreeId = 1,
                PositionId = 1,
                DepartmentId = 1
            };

            await ctx.Set<Teacher>().AddRangeAsync(teacher);
            await ctx.SaveChangesAsync();

            var result = await teacherService.DeleteTeacherAsync(1, CancellationToken.None);
            Assert.True(result);

            var deleted = await ctx.Teachers.FindAsync(1);
            Assert.Null(deleted);
        }

        [Fact]
        public async Task FilterByDepartment_ReturnsExpected()
        {
            var ctx = new UniversityDbContext(_dbContextOptions);
            var teacherService = new TeacherService(ctx);

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
            await ctx.SaveChangesAsync();

            var filter = new TeacherFilter { Department = "РЭА", Position = "Полотер", Degree = "Доктор наук"};
            var result = await teacherService.GetTeachersAsync(filter, CancellationToken.None);

            var teacherList = Assert.IsAssignableFrom<Teacher[]>(result);
            Assert.Single(teacherList); // Только один преподаватель на кафедре ИВТ
        }
    }
}