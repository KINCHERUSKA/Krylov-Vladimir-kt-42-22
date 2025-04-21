using System.ComponentModel.DataAnnotations;

namespace Krylov_KT_42_22.Models.DTO
{
    public class DisciplineDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalHours { get; set; }
    }

    public class AddDisciplineDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }

    public class UpdateDisciplineDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }

    public class TeacherDisciplineInfoDto
    {
        public string DepartmentName { get; set; }
    }
}