using System.ComponentModel.DataAnnotations;

namespace Krylov_KT_42_22.Models.DTO.TeachersDTO
{
    public class UpdateTeacherDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        public int DegreeId { get; set; }

        [Required]
        public int PositionId { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }
}