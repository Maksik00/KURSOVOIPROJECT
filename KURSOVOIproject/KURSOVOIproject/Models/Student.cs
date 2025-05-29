namespace KURSOVOIproject.Models
{
    public class Student
    {
        public int Id { get; set; }

        private string _name = default!;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !char.IsUpper(value[0]))
                    throw new ArgumentException("Имя обязательно и должно начинаться с заглавной буквы.");
                _name = value;
            }
        }

        private string _telNumber = default!;
        public string TelNumber
        {
            get => _telNumber;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length != 13 || !value.StartsWith("+"))
                    throw new ArgumentException("Телефон должен начинаться с '+' и содержать 13 символов.");
                _telNumber = value;
            }
        }

        private int _course;
        public int Course
        {
            get => _course;
            set
            {
                if (value < 1 || value > 6)
                    throw new ArgumentException("Курс должен быть от 1 до 6.");
                _course = value;
            }
        }

        public int IdSpecialization { get; set; }

        // ← навигационное свойство на Specialization
        public Specialization Specialization { get; set; } = default!;

        private string _password = default!;
        public string Password
        {
            get => _password;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 6)
                    throw new ArgumentException("Пароль должен содержать минимум 6 символов.");
                _password = value;
            }
        }

        // ← коллекция откликов
        public ICollection<ApplicationEntity> Applications { get; set; } = new List<ApplicationEntity>();
    }
}
