namespace SFLAPI.Models
{
    public class Internship
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Requirements { get; set; }
        public int IdCompany { get; set; }

        // <-- навигационное свойство на Company
        public Company Company { get; set; }

        // <-- все отклики на эту стажировку
        public ICollection<Application> Applications { get; set; }
    }
}
