namespace KURSOVOIproject.Models
{
    public class Company
    {
        public int Id { get; set; }

        private string _name = default!;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Название компании обязательно.");
                _name = value;
            }
        }

        private string _city = default!;
        public string City
        {
            get => _city;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Город обязателен.");
                _city = value;
            }
        }

        private string _street = default!;
        public string Street
        {
            get => _street;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Улица обязательна.");
                _street = value;
            }
        }

        private string _building = default!;
        public string Building
        {
            get => _building;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Номер здания обязателен.");
                _building = value;
            }
        }

        private string _password = default!;
        public string Password
        {
            get => _password;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 6)
                    throw new ArgumentException("Пароль должен быть не менее 6 символов.");
                _password = value;
            }
        }

        // ← коллекция стажировок
        public ICollection<Internship> Internships { get; set; } = new List<Internship>();
    }
}
