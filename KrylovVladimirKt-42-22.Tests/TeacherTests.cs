using System;
using System.Text.RegularExpressions;
using Xunit;
using Krylov_KT_42_22.Models;

namespace KrylovVladimirKt_42_22.Tests
{
    public class TeacherTests
    {
        [Fact]
        public void IsValidTeacherName_ReturnsTrue_ForValidNames()
        {
            var testTeacher = new Teacher
            {
                FirstName = "Иван",
                LastName = "Иванович"
            };

            var firstNameResult = testTeacher.IsValidTeacherFirstName();
            var lastNameResult = testTeacher.IsValidTeacherLastName();

            Assert.True(firstNameResult);
            Assert.True(lastNameResult);
        }

        [Fact]
        public void IsValidTeacherDegreeId_ReturnTrue_ForValidInt()
        {
            var testTeacher = new Teacher
            {
                DegreeId = 1
            };

            var result = testTeacher.IsValidTeacherDegree();

            Assert.True(result);
        }

        [Fact]
        public void IsValidTeacherDepartmentId_ReturnTrue_ForValidInt()
        {
            var testTeacher = new Teacher
            {
                DepartmentId = 1
            };

            var result = testTeacher.IsValidTeacherDepartment();

            Assert.True(result);
        }

        [Fact]
        public void IsValidTeacherPositionId_ReturnTrue_ForValidInt()
        {
            var testTeacher = new Teacher
            {
                PositionId = 1
            };

            var result = testTeacher.IsValidTeacherPosition();

            Assert.True(result);
        }

        [Fact]
        public void IsValidDegreeInput_ReturnTrue_ForNumericString()
        {
            var result = Teacher.IsValidNumberInput("123");
            Assert.True(result);
        }

        [Fact]
        public void IsValidDegreeInput_ReturnFalse_ForNonNumericString()
        {
            var result = Teacher.IsValidNumberInput("abc");
            Assert.False(result);
        }
    }
}