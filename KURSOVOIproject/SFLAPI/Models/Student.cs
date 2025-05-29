namespace SFLAPI.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TelNumber { get; set; }
        public int Course { get; set; }
        public int IdSpecialization { get; set; }
        public string Password { get; set; }

        // <-- навигационное свойство на Specialization
        public Specialization Specialization { get; set; }

        // <-- все отклики этого студента
        public ICollection<Application> Applications { get; set; }
    }
}
