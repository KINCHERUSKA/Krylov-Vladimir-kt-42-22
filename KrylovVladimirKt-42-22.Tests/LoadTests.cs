using Krylov_KT_42_22.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrylovVladimirKt_42_22.Tests
{
    public class LoadTests
    {
        [Fact]
        public void IsValidLoadTeacherId_ReturnTrue_ForValidInt()
        {
            var testLoad = new Load
            {
                TeacherId = 1
            };

            var result = testLoad.IsValidLoadTeacher();

            Assert.True(result);
        }

        [Fact]
        public void IsValidLoadDisciplineId_ReturnTrue_ForValidInt()
        {
            var testLoad = new Load
            {
                DisciplineId = 1
            };

            var result = testLoad.IsValidLoadDiscipline();

            Assert.True(result);
        }

        [Fact]
        public void IsValidHour_ReturnTrue_ForValidInt()
        {
            var testLoad = new Load            {
                Hours = 15
            };

            var result = testLoad.IsValidHours();

            Assert.True(result);
        }
    }
}
