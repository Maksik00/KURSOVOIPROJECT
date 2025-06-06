using System;
using System.Collections.Generic;

namespace KURSOVOIproject.Models
{
    public class Internship
    {
        public int Id { get; set; }

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Название стажировки обязательно.");
                _title = value.Trim();
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (value == default)
                    throw new ArgumentException("Дата начала обязательна.");
                _startDate = value;
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (value < _startDate)
                    throw new ArgumentException("Дата окончания не может быть раньше даты начала.");
                _endDate = value;
            }
        }

        private string _requirements = string.Empty;
        public string Requirements
        {
            get => _requirements;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Требования обязательны.");
                _requirements = value.Trim();
            }
        }

        public int IdCompany { get; set; }
        public Company Company { get; set; } = null!;
        // **Добавляем поле специализации**:
        public int IdSpecialization { get; set; }
        public Specialization Specialization { get; set; } = default!;

        public ICollection<ApplicationEntity> Applications { get; set; } = new List<ApplicationEntity>();
        public ICollection<Internship> Internships { get; set; } = new List<Internship>();
    }
}
