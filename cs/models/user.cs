namespace JobFinder.Models
{
    // Represents a registered account (Candidate or Employer).
    // Reference-only in this static-site version (see Job.cs note).
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "Candidate"; // "Candidate" or "Employer"
        public string Phone { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string ProfileTitle { get; set; } = string.Empty;
        public string Education { get; set; } = string.Empty;
        public string Experience { get; set; } = string.Empty;
        public List<string> Skills { get; set; } = new();
        public string Resume { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
    }
}
