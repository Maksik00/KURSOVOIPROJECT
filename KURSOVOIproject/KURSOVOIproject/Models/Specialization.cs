namespace KURSOVOIproject.Models
{
    public class Specialization
    {
        public int Id { get; set; }

        private string _name = default!;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Название специализации обязательно.");
                _name = value;
            }
        }

        // ← коллекция студентов
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
