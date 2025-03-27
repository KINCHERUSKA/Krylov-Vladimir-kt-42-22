namespace Krylov_KT_42_22.Models
{
    public class Teacher
    {
        // Id
        public int Id { get; set; }
        
        //ФИО
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string MiddleName { get; set; }

        // Ученая степень
        public int? DegreeId { get; set; }
        public virtual Degree Degree { get; set; }

        // Должность
        public int PositionId { get; set; }
        public virtual Position Position { get; set; }
        
        // Кафедра
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        //public virtual ICollection<Load> Loads { get; set; } = new List<Load>();
    }
}