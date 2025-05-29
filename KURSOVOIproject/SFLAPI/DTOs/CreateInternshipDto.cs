using System;

namespace SFLAPI.DTOs
{
    public class CreateInternshipDto
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Requirements { get; set; }
        public int IdCompany { get; set; }
    }
}

