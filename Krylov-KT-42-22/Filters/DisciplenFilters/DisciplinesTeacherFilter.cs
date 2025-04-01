using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Krylov_KT_42_22.Filters.DisciplenFilters
{
    public class DisciplinesTeacherFilter
    {
        public int TeacherId { get; set; }

        public int MinHours { get; set; }
        public int MaxHours { get; set; }
    }
}
