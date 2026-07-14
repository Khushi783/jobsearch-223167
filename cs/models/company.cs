namespace JobFinder.Models
{
    // Represents an employer/company profile.
    // Reference-only in this static-site version (see Job.cs note).
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Logo { get; set; } = string.Empty;
        public string Industry { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int FoundedYear { get; set; }
        public string CompanySize { get; set; } = string.Empty;
        public int OpenPositions { get; set; }
    }
}
