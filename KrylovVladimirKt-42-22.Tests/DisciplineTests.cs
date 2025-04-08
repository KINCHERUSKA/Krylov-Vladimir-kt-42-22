using Krylov_KT_42_22.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrylovVladimirKt_42_22.Tests
{
    public class DisciplineTests
    {
        [Fact]
        public void IsValidDisciplineName_ReturnsTrue_ForValidNames()
        {
            var testDiscipline = new Discipline
            {
                Name = "Проектный практикум"
            };

            var result = testDiscipline.IsValidDisciplineName();

            Assert.True(result);
        }
    }
}
