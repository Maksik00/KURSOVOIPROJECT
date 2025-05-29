namespace SFLAPI.Models
{
    public class Application
    {
        public int Id { get; set; }
        public DateTime SubmissionDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public bool IsAccepted { get; set; }

        public int IdStudent { get; set; }
        public int IdInternship { get; set; }

        // <-- навигационные свойства
        public Student Student { get; set; }
        public Internship Internship { get; set; }
    }
}
