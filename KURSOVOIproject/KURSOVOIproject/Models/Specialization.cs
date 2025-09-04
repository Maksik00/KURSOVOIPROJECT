using System;
using System.Collections.Generic;

namespace KURSOVOIproject.Models
{
    public class Specialization
    {
        public int Id { get; set; }

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Название специализации обязательно.");
                _name = value.Trim();
            }
        }

        public ICollection<Student> Students { get; set; } = new List<Student>();
        // <<--- вот это добавьте:
        public ICollection<Internship> Internships { get; set; } = new List<Internship>();
    }
}
