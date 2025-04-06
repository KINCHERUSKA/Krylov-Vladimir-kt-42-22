namespace Krylov_KT_42_22.Filters.DepartmentFilters
{
    public class DepartmentFilter
    {
        public DateTime? FoundedDateFrom { get; set; }
        public DateTime? FoundedDateTo { get; set; }
        public int? MinTeachersCount { get; set; }
        public int? MaxTeachersCount { get; set; }
    }
}