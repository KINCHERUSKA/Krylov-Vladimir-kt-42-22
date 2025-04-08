using System.Text.RegularExpressions;

namespace Krylov_KT_42_22.Models
{
    public class Load
    {
        public int Id { get; set; }

        public int TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }

        public int DisciplineId { get; set; }

        public virtual Discipline Discipline { get; set; }

        public int Hours { get; set; }

        // Проверка числовых идентификаторов
        public bool IsValidLoadTeacher()
        {
            return TeacherId > 0;
        }

        public bool IsValidLoadDiscipline()
        {
            return DisciplineId > 0;
        }

        public bool IsValidHours()
        {
            return Hours >= 0;
        }

        // Проверка строки, можно ли её преобразовать в число
        public static bool IsValidNumberInput(string input)
        {
            return int.TryParse(input, out int result);
        }
    }
}