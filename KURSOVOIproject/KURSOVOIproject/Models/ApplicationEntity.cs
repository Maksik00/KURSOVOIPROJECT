using System;

namespace KURSOVOIproject.Models
{
    public class ApplicationEntity
    {
        public int Id { get; set; }

        private DateTime _submissionDate = DateTime.UtcNow;
        public DateTime SubmissionDate
        {
            get => _submissionDate;
            set => _submissionDate = value == default
                ? throw new ArgumentException("Дата подачи обязательна.")
                : value;
        }

        private DateTime? _responseDate;
        public DateTime? ResponseDate
        {
            get => _responseDate;
            set
            {
                if (value.HasValue && value.Value < SubmissionDate)
                    throw new ArgumentException("Дата ответа не может быть раньше даты подачи.");
                _responseDate = value;
            }
        }

        public bool IsAccepted { get; set; }

        public int IdStudent { get; set; }
        public Student Student { get; set; } = default!;

        public int IdInternship { get; set; }
        public Internship Internship { get; set; } = default!;
    }
}
