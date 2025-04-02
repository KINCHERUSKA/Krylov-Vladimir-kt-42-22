using System.ComponentModel.DataAnnotations;

namespace Krylov_KT_42_22.Models
{
    public class Degree
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}