namespace SFLAPI.Models
{
    public class Specialization
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // <-- все студенты этой специализации
        public ICollection<Student> Students { get; set; }
    }
}
