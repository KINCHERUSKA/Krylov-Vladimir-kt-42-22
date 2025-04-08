using System;
using System.Text.RegularExpressions;

namespace Krylov_KT_42_22.Models
{
    public class Discipline
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsValidDisciplineName()
        {
            return !string.IsNullOrEmpty(Name) &&
                   Regex.IsMatch(Name, @"^[a-zA-Zа-яА-ЯёЁ ]+$");
        }
    }
}
