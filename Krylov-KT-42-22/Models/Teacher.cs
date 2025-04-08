using System.Text.RegularExpressions;

namespace Krylov_KT_42_22.Models
{
    public class Teacher
    {
        // Id
        public int Id { get; set; }
        
        //ФИО
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Ученая степень
        public int DegreeId { get; set; }
        public virtual Degree Degree { get; set; }

        // Должность
        public int PositionId { get; set; }
        public virtual Position Position { get; set; }
        
        // Кафедра
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public bool IsValidTeacherFirstName()
        {
            return !string.IsNullOrEmpty(FirstName) &&
                   Regex.IsMatch(FirstName, @"^[a-zA-Zа-яА-ЯёЁ]+$");
        }

        // Проверка фамилии
        public bool IsValidTeacherLastName()
        {
            return !string.IsNullOrEmpty(LastName) &&
                   Regex.IsMatch(LastName, @"^[a-zA-Zа-яА-ЯёЁ]+$");
        }

        // Проверка числовых идентификаторов
        public bool IsValidTeacherDegree()
        {
            return DegreeId > 0;
        }

        public bool IsValidTeacherDepartment()
        {
            return DepartmentId > 0;
        }

        public bool IsValidTeacherPosition()
        {
            return PositionId > 0;
        }

        // Проверка строки, можно ли её преобразовать в число
        public static bool IsValidNumberInput(string input)
        {
            return int.TryParse(input, out int result);
        }
    }
}