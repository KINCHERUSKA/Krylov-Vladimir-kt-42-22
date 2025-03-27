namespace Krylov_KT_42_22.Models
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime FoundedDate { get; set; }

        public int HeadId { get; set; }
        public virtual Teacher Head { get; set; }

        //public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}