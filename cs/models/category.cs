namespace JobFinder.Models
{
    // Represents a job category used to group and filter job listings.
    // Reference-only in this static-site version (see Job.cs note).
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int JobCount { get; set; }
    }
}
