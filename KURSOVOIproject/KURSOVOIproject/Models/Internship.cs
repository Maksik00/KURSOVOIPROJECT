namespace KURSOVOIproject.Models
{
    public class Internship
    {
        public int Id { get; set; }

        private string _title = default!;
        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Название стажировки обязательно.");
                _title = value;
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

        private string _requirements = default!;
        public string Requirements
        {
            get => _requirements;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Требования обязательны.");
                _requirements = value;
            }
        }

        public int IdCompany { get; set; }

        // ← навигационное свойство на Company
        public Company Company { get; set; } = default!;

        // ← коллекция откликов
        public ICollection<ApplicationEntity> Applications { get; set; } = new List<ApplicationEntity>();
    }
}
