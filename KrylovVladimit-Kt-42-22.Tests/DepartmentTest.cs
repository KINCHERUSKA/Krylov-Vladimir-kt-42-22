using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrylovVladimit_Kt_42_22.Tests
{
    public class DepartmentTest
    {
        [Fact]
        public void IsValidDepartmentDate_ReturnsTrue_WhenDateIsAfter1967()
        {
            // Arrange
            var validDate = new DateTime(1970, 1, 1);
            var department = new Department { FoundedDate = validDate };

            // Act
            var result = department.IsValidDepartmentDate();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidDepartmentDate_ReturnsTrue_WhenDateIsExactly1967()
        {
            // Arrange
            var boundaryDate = new DateTime(1967, 9, 1);
            var department = new Department { FoundedDate = boundaryDate };

            // Act
            var result = department.IsValidDepartmentDate();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidDepartmentDate_ReturnsFalse_WhenDateIsBefore1967()
        {
            // Arrange
            var invalidDate = new DateTime(1960, 1, 1);
            var department = new Department { FoundedDate = invalidDate };

            // Act
            var result = department.IsValidDepartmentDate();

            // Assert
            Assert.False(result);
        }

        public class Department
        {
            public int DepartmentId { get; set; }
            public DateTime FoundedDate { get; set; }

            public bool IsValidDepartmentDate()
            {
                // Дата основания должна быть не раньше 1 сентября 1967 года
                var cutoffDate = new DateTime(1967, 9, 1);
                return FoundedDate >= cutoffDate;
            }
        }
    }
}