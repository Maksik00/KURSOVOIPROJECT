using System;

namespace SFLAPI.DTOs
{
    public class ApplicationDto
    {
        public int Id { get; set; }
        public DateTime SubmissionDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public bool IsAccepted { get; set; }
        public int IdStudent { get; set; }
        public int IdInternship { get; set; }
    }
}

