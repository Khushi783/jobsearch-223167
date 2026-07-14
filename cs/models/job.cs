namespace JobFinder.Models
{
    // Represents a single job listing.
    // NOTE: In this static-site version, this class is provided as a clean,
    // compilable reference implementation. It is NOT wired up to the HTML
    // files, since plain .html cannot execute server-side C#.
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string CompanyLogo { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Salary { get; set; } = string.Empty;
        public string JobType { get; set; } = string.Empty;
        public string Experience { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Responsibilities { get; set; } = new();
        public List<string> Requirements { get; set; } = new();
        public List<string> Skills { get; set; } = new();
        public List<string> Benefits { get; set; } = new();
        public DateTime PostedDate { get; set; }

        public string PostedAgo
        {
            get
            {
                var days = (DateTime.Now - PostedDate).Days;
                if (days <= 0) return "Today";
                if (days == 1) return "1 day ago";
                if (days < 30) return $"{days} days ago";
                var months = days / 30;
                return months == 1 ? "1 month ago" : $"{months} months ago";
            }
        }
    }
}
