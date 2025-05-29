namespace SFLAPI.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string Password { get; set; }

        // <-- все стажировки этой компании
        public ICollection<Internship> Internships { get; set; }
    }
}
