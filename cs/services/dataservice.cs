using JobFinder.Models;

namespace JobFinder.Services
{
    /// <summary>
    /// Reference implementation of the in-memory "database" used by the
    /// original dynamic (Razor Pages) version of JobFinder.
    ///
    /// NOTE: In this static-site version, this class is NOT executed by the
    /// HTML files — plain .html has no server-side runtime. It is included
    /// so the C# data model and business logic (search, filtering, saved/
    /// applied job tracking, login) are available as clean, compilable
    /// reference code, exactly as they were in the dynamic version.
    /// To make the site live again, wire this class back into an
    /// ASP.NET Core Razor Pages or MVC project and reference it from
    /// PageModel/Controller classes via dependency injection.
    /// </summary>
    public class DataService
    {
        public List<Job> Jobs { get; private set; }
        public List<Company> Companies { get; private set; }
        public List<Category> Categories { get; private set; }
        public List<User> Users { get; private set; }

        private readonly Dictionary<string, HashSet<int>> _savedJobsBySession = new();
        private readonly Dictionary<string, HashSet<int>> _appliedJobsBySession = new();

        public DataService()
        {
            Categories = SeedCategories();
            Companies = SeedCompanies();
            Jobs = SeedJobs();
            Users = SeedUsers();

            RecalculateCategoryCounts();
        }

        private List<Category> SeedCategories()
        {
            return new List<Category>
            {
                new() { Id = 1, Name = "Software Development", Icon = "💻", Description = "Engineering and programming roles." },
                new() { Id = 2, Name = "Design", Icon = "🎨", Description = "UI/UX and creative design roles." },
                new() { Id = 3, Name = "Marketing", Icon = "📈", Description = "Digital marketing and growth roles." },
                new() { Id = 4, Name = "Sales", Icon = "🤝", Description = "Business development and sales roles." },
                new() { Id = 5, Name = "Data Science", Icon = "📊", Description = "Analytics and machine learning roles." },
                new() { Id = 6, Name = "Human Resources", Icon = "🧑‍💼", Description = "Recruiting and people operations roles." },
                new() { Id = 7, Name = "Finance", Icon = "💰", Description = "Accounting and financial analysis roles." },
                new() { Id = 8, Name = "Customer Support", Icon = "🎧", Description = "Customer success and support roles." },
            };
        }

        private List<Company> SeedCompanies()
        {
            return new List<Company>
            {
                new() { Id = 1, Name = "NimbusTech", Logo = "NT", Industry = "Cloud Computing", Description = "NimbusTech builds scalable cloud infrastructure trusted by startups and enterprises alike.", Website = "https://www.nimbustech.example.com", Location = "Bengaluru, India", FoundedYear = 2014, CompanySize = "500-1000", OpenPositions = 5 },
                new() { Id = 2, Name = "BrightWave Media", Logo = "BW", Industry = "Digital Marketing", Description = "BrightWave Media crafts data-driven marketing campaigns for global brands.", Website = "https://www.brightwave.example.com", Location = "Mumbai, India", FoundedYear = 2011, CompanySize = "100-500", OpenPositions = 3 },
                new() { Id = 3, Name = "PixelCraft Studio", Logo = "PC", Industry = "Design & Creative", Description = "PixelCraft Studio is a boutique product design agency focused on delightful user experiences.", Website = "https://www.pixelcraft.example.com", Location = "Pune, India", FoundedYear = 2017, CompanySize = "50-100", OpenPositions = 3 },
                new() { Id = 4, Name = "FinEdge Capital", Logo = "FE", Industry = "Financial Services", Description = "FinEdge Capital provides modern investment and banking technology solutions.", Website = "https://www.finedge.example.com", Location = "Gurugram, India", FoundedYear = 2009, CompanySize = "1000+", OpenPositions = 4 },
                new() { Id = 5, Name = "DataForge Analytics", Logo = "DF", Industry = "Data & Analytics", Description = "DataForge Analytics turns raw data into actionable insight through machine learning.", Website = "https://www.dataforge.example.com", Location = "Hyderabad, India", FoundedYear = 2016, CompanySize = "100-500", OpenPositions = 4 },
                new() { Id = 6, Name = "GreenLeaf Foods", Logo = "GL", Industry = "FMCG", Description = "GreenLeaf Foods is a fast-growing consumer goods company focused on sustainable products.", Website = "https://www.greenleaf.example.com", Location = "Delhi, India", FoundedYear = 2005, CompanySize = "1000+", OpenPositions = 3 },
                new() { Id = 7, Name = "Sunrise Health Group", Logo = "SH", Industry = "Healthcare", Description = "Sunrise Health Group operates a network of clinics and digital health services.", Website = "https://www.sunrisehealth.example.com", Location = "Chennai, India", FoundedYear = 2012, CompanySize = "500-1000", OpenPositions = 2 },
                new() { Id = 8, Name = "Vertex Systems", Logo = "VS", Industry = "Enterprise Software", Description = "Vertex Systems builds enterprise resource planning tools for manufacturing companies.", Website = "https://www.vertexsystems.example.com", Location = "Noida, India", FoundedYear = 2008, CompanySize = "500-1000", OpenPositions = 3 },
                new() { Id = 9, Name = "Skyline Logistics", Logo = "SL", Industry = "Logistics & Supply Chain", Description = "Skyline Logistics powers same-day delivery across major Indian cities.", Website = "https://www.skylinelogistics.example.com", Location = "Ahmedabad, India", FoundedYear = 2015, CompanySize = "1000+", OpenPositions = 2 },
                new() { Id = 10, Name = "Solstice Learning", Logo = "SO", Industry = "EdTech", Description = "Solstice Learning creates online learning experiences for students and professionals.", Website = "https://www.solsticelearning.example.com", Location = "Remote", FoundedYear = 2018, CompanySize = "50-100", OpenPositions = 3 },
            };
        }

        private List<Job> SeedJobs()
        {
            return new List<Job>
            {
                new() { Id = 1, Title = "Senior Software Engineer", CompanyName = "NimbusTech", CompanyLogo = "NT", Category = "Software Development", Location = "Bengaluru, India", Salary = "₹22,00,000 - ₹32,00,000", JobType = "Full-time", Experience = "5-8 years", Description = "Join NimbusTech's platform team to design and build the next generation of our cloud orchestration engine used by thousands of customers worldwide.", Responsibilities = new(){"Design and implement scalable backend services in C# and Go","Own the architecture for key platform components","Mentor junior engineers and conduct code reviews"}, Requirements = new(){"Bachelor's degree in Computer Science or related field","5+ years of backend development experience"}, Skills = new(){"C#","ASP.NET Core","Kubernetes","Azure","SQL"}, Benefits = new(){"Health insurance for family","Stock options","Flexible working hours"}, PostedDate = DateTime.Now.AddDays(-2) },
                new() { Id = 2, Title = "Frontend Developer", CompanyName = "NimbusTech", CompanyLogo = "NT", Category = "Software Development", Location = "Bengaluru, India", Salary = "₹10,00,000 - ₹16,00,000", JobType = "Full-time", Experience = "2-4 years", Description = "Build beautiful, responsive interfaces for our customer-facing dashboard used by enterprise clients.", Responsibilities = new(){"Develop UI components with modern CSS and semantic HTML","Work closely with designers on layout implementation"}, Requirements = new(){"2+ years building web interfaces","Strong grasp of HTML5 and CSS3"}, Skills = new(){"HTML5","CSS3","Responsive Design"}, Benefits = new(){"Health insurance","Remote-friendly","Gym membership"}, PostedDate = DateTime.Now.AddDays(-5) },
                new() { Id = 3, Title = "DevOps Engineer", CompanyName = "NimbusTech", CompanyLogo = "NT", Category = "Software Development", Location = "Bengaluru, India", Salary = "₹15,00,000 - ₹24,00,000", JobType = "Full-time", Experience = "3-6 years", Description = "Own and evolve our CI/CD pipelines and cloud infrastructure across multiple regions.", Responsibilities = new(){"Manage Kubernetes clusters in production","Automate deployment pipelines"}, Requirements = new(){"Experience with container orchestration","Familiarity with infrastructure as code"}, Skills = new(){"Kubernetes","Docker","Terraform","Azure"}, Benefits = new(){"Health insurance","Stock options"}, PostedDate = DateTime.Now.AddDays(-10) },
                new() { Id = 4, Title = "Backend Engineer (.NET)", CompanyName = "Vertex Systems", CompanyLogo = "VS", Category = "Software Development", Location = "Noida, India", Salary = "₹12,00,000 - ₹20,00,000", JobType = "Full-time", Experience = "3-5 years", Description = "Build robust APIs and services powering our enterprise resource planning suite.", Responsibilities = new(){"Design RESTful APIs using ASP.NET Core","Optimize database queries and schema design"}, Requirements = new(){"Strong C# fundamentals","Experience with relational databases"}, Skills = new(){"C#","ASP.NET Core","SQL Server"}, Benefits = new(){"Health insurance","Provident fund"}, PostedDate = DateTime.Now.AddDays(-8) },
                new() { Id = 5, Title = "Mobile App Developer", CompanyName = "Solstice Learning", CompanyLogo = "SO", Category = "Software Development", Location = "Remote", Salary = "₹14,00,000 - ₹22,00,000", JobType = "Remote", Experience = "3-5 years", Description = "Develop and maintain our cross-platform learning app used by over a million students.", Responsibilities = new(){"Build features for iOS and Android using a shared codebase","Fix bugs and improve app performance"}, Requirements = new(){"Experience with cross-platform frameworks"}, Skills = new(){".NET MAUI","C#","Mobile Development"}, Benefits = new(){"Fully remote","Flexible hours"}, PostedDate = DateTime.Now.AddDays(-3) },
                new() { Id = 6, Title = "Full Stack Developer", CompanyName = "FinEdge Capital", CompanyLogo = "FE", Category = "Software Development", Location = "Gurugram, India", Salary = "₹18,00,000 - ₹28,00,000", JobType = "Full-time", Experience = "4-7 years", Description = "Work across the stack to deliver secure, high-performance financial applications.", Responsibilities = new(){"Develop full stack features using ASP.NET Core and Razor","Ensure compliance with financial data security standards"}, Requirements = new(){"4+ years full stack experience"}, Skills = new(){"C#","ASP.NET Core","SQL"}, Benefits = new(){"Health insurance","Annual bonus"}, PostedDate = DateTime.Now.AddDays(-14) },

                new() { Id = 7, Title = "UI/UX Designer", CompanyName = "PixelCraft Studio", CompanyLogo = "PC", Category = "Design", Location = "Pune, India", Salary = "₹9,00,000 - ₹15,00,000", JobType = "Full-time", Experience = "2-4 years", Description = "Design intuitive, delightful user experiences for our clients' web and mobile products.", Responsibilities = new(){"Conduct user research and usability testing","Create wireframes, prototypes and high-fidelity designs"}, Requirements = new(){"Portfolio demonstrating UX process"}, Skills = new(){"Figma","User Research","Prototyping"}, Benefits = new(){"Creative freedom","Health insurance"}, PostedDate = DateTime.Now.AddDays(-4) },
                new() { Id = 8, Title = "Product Designer", CompanyName = "PixelCraft Studio", CompanyLogo = "PC", Category = "Design", Location = "Pune, India", Salary = "₹11,00,000 - ₹18,00,000", JobType = "Full-time", Experience = "3-6 years", Description = "Lead end-to-end design for new product lines from concept to launch.", Responsibilities = new(){"Own product design from discovery to delivery"}, Requirements = new(){"3+ years of product design experience"}, Skills = new(){"Figma","Interaction Design"}, Benefits = new(){"Health insurance","Stock options"}, PostedDate = DateTime.Now.AddDays(-9) },
                new() { Id = 9, Title = "Graphic Designer", CompanyName = "BrightWave Media", CompanyLogo = "BW", Category = "Design", Location = "Mumbai, India", Salary = "₹6,00,000 - ₹10,00,000", JobType = "Full-time", Experience = "1-3 years", Description = "Create visual content for digital campaigns across social, web and print.", Responsibilities = new(){"Design graphics for social media and ad campaigns"}, Requirements = new(){"Strong portfolio of graphic design work"}, Skills = new(){"Adobe Photoshop","Adobe Illustrator"}, Benefits = new(){"Health insurance","Creative environment"}, PostedDate = DateTime.Now.AddDays(-6) },

                new() { Id = 10, Title = "Digital Marketing Manager", CompanyName = "BrightWave Media", CompanyLogo = "BW", Category = "Marketing", Location = "Mumbai, India", Salary = "₹13,00,000 - ₹20,00,000", JobType = "Full-time", Experience = "4-7 years", Description = "Lead digital marketing strategy and execution for a portfolio of major client accounts.", Responsibilities = new(){"Develop and execute multi-channel marketing campaigns"}, Requirements = new(){"4+ years in digital marketing"}, Skills = new(){"SEO","SEM","Google Analytics"}, Benefits = new(){"Health insurance","Performance bonus"}, PostedDate = DateTime.Now.AddDays(-7) },
                new() { Id = 11, Title = "Content Marketing Specialist", CompanyName = "Solstice Learning", CompanyLogo = "SO", Category = "Marketing", Location = "Remote", Salary = "₹7,00,000 - ₹12,00,000", JobType = "Remote", Experience = "2-4 years", Description = "Create engaging content that helps learners discover and love our platform.", Responsibilities = new(){"Write blog posts, newsletters and landing page copy"}, Requirements = new(){"Excellent writing skills"}, Skills = new(){"Content Writing","SEO"}, Benefits = new(){"Fully remote","Flexible hours"}, PostedDate = DateTime.Now.AddDays(-1) },
                new() { Id = 12, Title = "Social Media Manager", CompanyName = "GreenLeaf Foods", CompanyLogo = "GL", Category = "Marketing", Location = "Delhi, India", Salary = "₹8,00,000 - ₹13,00,000", JobType = "Full-time", Experience = "2-5 years", Description = "Own our brand presence across social platforms and grow an engaged community.", Responsibilities = new(){"Plan and publish social media content calendars"}, Requirements = new(){"Experience managing brand social accounts"}, Skills = new(){"Social Media Strategy","Analytics"}, Benefits = new(){"Health insurance","Product discounts"}, PostedDate = DateTime.Now.AddDays(-11) },

                new() { Id = 13, Title = "Sales Executive", CompanyName = "FinEdge Capital", CompanyLogo = "FE", Category = "Sales", Location = "Gurugram, India", Salary = "₹6,00,000 - ₹11,00,000 + Incentives", JobType = "Full-time", Experience = "1-3 years", Description = "Drive new business growth by building relationships with prospective clients.", Responsibilities = new(){"Identify and pursue new sales opportunities"}, Requirements = new(){"1+ years of B2B sales experience"}, Skills = new(){"B2B Sales","Negotiation"}, Benefits = new(){"Uncapped incentives","Health insurance"}, PostedDate = DateTime.Now.AddDays(-13) },
                new() { Id = 14, Title = "Business Development Manager", CompanyName = "Skyline Logistics", CompanyLogo = "SL", Category = "Sales", Location = "Ahmedabad, India", Salary = "₹12,00,000 - ₹18,00,000", JobType = "Full-time", Experience = "4-6 years", Description = "Expand our enterprise client base across the western region.", Responsibilities = new(){"Develop and execute regional growth strategy"}, Requirements = new(){"4+ years in business development"}, Skills = new(){"Business Development","Account Management"}, Benefits = new(){"Health insurance","Company vehicle"}, PostedDate = DateTime.Now.AddDays(-15) },

                new() { Id = 15, Title = "Data Scientist", CompanyName = "DataForge Analytics", CompanyLogo = "DF", Category = "Data Science", Location = "Hyderabad, India", Salary = "₹16,00,000 - ₹26,00,000", JobType = "Full-time", Experience = "3-6 years", Description = "Build predictive models that power our clients' decision-making processes.", Responsibilities = new(){"Develop and validate machine learning models"}, Requirements = new(){"Strong background in statistics"}, Skills = new(){"Python","Machine Learning","SQL"}, Benefits = new(){"Health insurance","Research budget"}, PostedDate = DateTime.Now.AddDays(-2) },
                new() { Id = 16, Title = "Data Analyst", CompanyName = "DataForge Analytics", CompanyLogo = "DF", Category = "Data Science", Location = "Hyderabad, India", Salary = "₹9,00,000 - ₹14,00,000", JobType = "Full-time", Experience = "1-3 years", Description = "Turn business questions into data-driven answers using dashboards and reports.", Responsibilities = new(){"Build dashboards and reports for stakeholders"}, Requirements = new(){"Strong SQL skills"}, Skills = new(){"SQL","Excel","Power BI"}, Benefits = new(){"Health insurance","Hybrid work"}, PostedDate = DateTime.Now.AddDays(-6) },
                new() { Id = 17, Title = "Machine Learning Engineer", CompanyName = "NimbusTech", CompanyLogo = "NT", Category = "Data Science", Location = "Bengaluru, India", Salary = "₹20,00,000 - ₹30,00,000", JobType = "Full-time", Experience = "4-7 years", Description = "Deploy and scale machine learning systems that power intelligent product features.", Responsibilities = new(){"Build and maintain ML pipelines"}, Requirements = new(){"Experience deploying ML models to production"}, Skills = new(){"Python","MLOps","Kubernetes"}, Benefits = new(){"Health insurance","Stock options"}, PostedDate = DateTime.Now.AddDays(-4) },

                new() { Id = 18, Title = "HR Business Partner", CompanyName = "Vertex Systems", CompanyLogo = "VS", Category = "Human Resources", Location = "Noida, India", Salary = "₹10,00,000 - ₹16,00,000", JobType = "Full-time", Experience = "3-6 years", Description = "Partner with department leaders to build a thriving, engaged workforce.", Responsibilities = new(){"Advise managers on people-related decisions"}, Requirements = new(){"3+ years in HR business partnering"}, Skills = new(){"HR Policies","Employee Relations"}, Benefits = new(){"Health insurance","Paid parental leave"}, PostedDate = DateTime.Now.AddDays(-12) },
                new() { Id = 19, Title = "Talent Acquisition Specialist", CompanyName = "Sunrise Health Group", CompanyLogo = "SH", Category = "Human Resources", Location = "Chennai, India", Salary = "₹7,00,000 - ₹11,00,000", JobType = "Full-time", Experience = "2-4 years", Description = "Find and hire outstanding talent to support our growing healthcare network.", Responsibilities = new(){"Source and screen candidates for open roles"}, Requirements = new(){"2+ years of recruiting experience"}, Skills = new(){"Recruiting","Interviewing"}, Benefits = new(){"Health insurance","Paid time off"}, PostedDate = DateTime.Now.AddDays(-9) },

                new() { Id = 20, Title = "Financial Analyst", CompanyName = "FinEdge Capital", CompanyLogo = "FE", Category = "Finance", Location = "Gurugram, India", Salary = "₹9,00,000 - ₹15,00,000", JobType = "Full-time", Experience = "2-4 years", Description = "Support strategic decision-making through financial modeling and analysis.", Responsibilities = new(){"Build financial models to support business decisions"}, Requirements = new(){"Strong Excel and financial modeling skills"}, Skills = new(){"Financial Modeling","Excel"}, Benefits = new(){"Health insurance","Annual bonus"}, PostedDate = DateTime.Now.AddDays(-3) },
                new() { Id = 21, Title = "Accounts Manager", CompanyName = "GreenLeaf Foods", CompanyLogo = "GL", Category = "Finance", Location = "Delhi, India", Salary = "₹8,00,000 - ₹13,00,000", JobType = "Full-time", Experience = "3-5 years", Description = "Oversee day-to-day accounting operations and ensure regulatory compliance.", Responsibilities = new(){"Manage accounts payable and receivable"}, Requirements = new(){"3+ years in accounting"}, Skills = new(){"Accounting","Tally","GST Compliance"}, Benefits = new(){"Health insurance","Provident fund"}, PostedDate = DateTime.Now.AddDays(-16) },
                new() { Id = 22, Title = "Investment Associate", CompanyName = "FinEdge Capital", CompanyLogo = "FE", Category = "Finance", Location = "Gurugram, India", Salary = "₹14,00,000 - ₹22,00,000", JobType = "Full-time", Experience = "3-6 years", Description = "Evaluate investment opportunities and support portfolio management decisions.", Responsibilities = new(){"Perform due diligence on potential investments"}, Requirements = new(){"Strong valuation skills"}, Skills = new(){"Valuation","Financial Analysis"}, Benefits = new(){"Health insurance","Annual bonus"}, PostedDate = DateTime.Now.AddDays(-5) },

                new() { Id = 23, Title = "Customer Support Specialist", CompanyName = "Solstice Learning", CompanyLogo = "SO", Category = "Customer Support", Location = "Remote", Salary = "₹5,00,000 - ₹8,00,000", JobType = "Remote", Experience = "0-2 years", Description = "Help our learners get the most out of the platform through timely, friendly support.", Responsibilities = new(){"Respond to customer inquiries via chat and email"}, Requirements = new(){"Strong communication skills"}, Skills = new(){"Customer Service","Communication"}, Benefits = new(){"Fully remote","Flexible hours"}, PostedDate = DateTime.Now.AddDays(-1) },
                new() { Id = 24, Title = "Technical Support Engineer", CompanyName = "NimbusTech", CompanyLogo = "NT", Category = "Customer Support", Location = "Bengaluru, India", Salary = "₹7,00,000 - ₹12,00,000", JobType = "Full-time", Experience = "1-3 years", Description = "Provide technical support to enterprise customers using our cloud platform.", Responsibilities = new(){"Diagnose and resolve customer technical issues"}, Requirements = new(){"Basic networking knowledge"}, Skills = new(){"Troubleshooting","SQL"}, Benefits = new(){"Health insurance","On-call compensation"}, PostedDate = DateTime.Now.AddDays(-8) },
                new() { Id = 25, Title = "Customer Success Manager", CompanyName = "Vertex Systems", CompanyLogo = "VS", Category = "Customer Support", Location = "Noida, India", Salary = "₹11,00,000 - ₹17,00,000", JobType = "Full-time", Experience = "3-5 years", Description = "Ensure enterprise clients achieve success and grow their partnership with us.", Responsibilities = new(){"Own the post-sales relationship for key accounts"}, Requirements = new(){"3+ years in customer success or account management"}, Skills = new(){"Account Management","SaaS"}, Benefits = new(){"Health insurance","Performance bonus"}, PostedDate = DateTime.Now.AddDays(-10) },
            };
        }

        private List<User> SeedUsers()
        {
            return new List<User>
            {
                new() { Id = 1, FullName = "Demo Candidate", Email = "candidate@example.com", Password = "password123", Role = "Candidate", Phone = "+91 98765 43210", Location = "Bengaluru, India", ProfileTitle = "Software Engineer", Education = "B.Tech in Computer Science, 2020", Experience = "3 years as a Software Engineer at a mid-size product company.", Skills = new() { "C#", "ASP.NET Core", "SQL", "Communication" }, Resume = "demo_resume.pdf" },
                new() { Id = 2, FullName = "Demo Employer", Email = "employer@example.com", Password = "password123", Role = "Employer", Phone = "+91 91234 56789", Location = "Bengaluru, India", CompanyName = "NimbusTech" }
            };
        }

        public void RecalculateCategoryCounts()
        {
            foreach (var category in Categories)
            {
                category.JobCount = Jobs.Count(j => j.Category == category.Name);
            }
        }

        public Job? GetJobById(int id) => Jobs.FirstOrDefault(j => j.Id == id);

        public List<Job> SearchJobs(string? keyword, string? location, string? category, string? jobType, string? experience)
        {
            var query = Jobs.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(keyword))
                query = query.Where(j => j.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                          j.CompanyName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                          j.Skills.Any(s => s.Contains(keyword, StringComparison.OrdinalIgnoreCase)));

            if (!string.IsNullOrWhiteSpace(location))
                query = query.Where(j => j.Location.Contains(location, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(j => j.Category == category);

            if (!string.IsNullOrWhiteSpace(jobType))
                query = query.Where(j => j.JobType == jobType);

            if (!string.IsNullOrWhiteSpace(experience))
                query = query.Where(j => j.Experience == experience);

            return query.OrderByDescending(j => j.PostedDate).ToList();
        }

        public List<Job> GetJobsByCompany(string companyName) => Jobs.Where(j => j.CompanyName == companyName).ToList();
        public List<Job> GetRelatedJobs(Job job, int count = 3) => Jobs.Where(j => j.Category == job.Category && j.Id != job.Id).Take(count).ToList();
        public List<Job> GetFeaturedJobs(int count = 6) => Jobs.OrderByDescending(j => j.PostedDate).Take(count).ToList();

        public Company? GetCompanyById(int id) => Companies.FirstOrDefault(c => c.Id == id);

        public List<Company> SearchCompanies(string? keyword, string? industry)
        {
            var query = Companies.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(keyword))
                query = query.Where(c => c.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrWhiteSpace(industry))
                query = query.Where(c => c.Industry == industry);
            return query.OrderByDescending(c => c.OpenPositions).ToList();
        }

        public List<string> GetAllIndustries() => Companies.Select(c => c.Industry).Distinct().OrderBy(i => i).ToList();
        public Category? GetCategoryByName(string name) => Categories.FirstOrDefault(c => c.Name == name);

        public User? ValidateLogin(string email, string password) =>
            Users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && u.Password == password);

        public bool EmailExists(string email) => Users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        public User? GetUserById(int id) => Users.FirstOrDefault(u => u.Id == id);

        public User RegisterUser(string fullName, string email, string password, string role, string companyName)
        {
            var newUser = new User
            {
                Id = Users.Count > 0 ? Users.Max(u => u.Id) + 1 : 1,
                FullName = fullName,
                Email = email,
                Password = password,
                Role = role,
                CompanyName = companyName
            };
            Users.Add(newUser);
            return newUser;
        }

        public void SaveJob(string sessionKey, int jobId)
        {
            if (!_savedJobsBySession.ContainsKey(sessionKey))
                _savedJobsBySession[sessionKey] = new HashSet<int>();
            _savedJobsBySession[sessionKey].Add(jobId);
        }

        public void UnsaveJob(string sessionKey, int jobId)
        {
            if (_savedJobsBySession.TryGetValue(sessionKey, out var set))
                set.Remove(jobId);
        }

        public bool IsJobSaved(string sessionKey, int jobId) =>
            _savedJobsBySession.TryGetValue(sessionKey, out var set) && set.Contains(jobId);

        public List<Job> GetSavedJobs(string sessionKey)
        {
            if (!_savedJobsBySession.TryGetValue(sessionKey, out var set)) return new List<Job>();
            return Jobs.Where(j => set.Contains(j.Id)).ToList();
        }

        public void ApplyToJob(string sessionKey, int jobId)
        {
            if (!_appliedJobsBySession.ContainsKey(sessionKey))
                _appliedJobsBySession[sessionKey] = new HashSet<int>();
            _appliedJobsBySession[sessionKey].Add(jobId);
        }

        public bool HasApplied(string sessionKey, int jobId) =>
            _appliedJobsBySession.TryGetValue(sessionKey, out var set) && set.Contains(jobId);

        public List<Job> GetAppliedJobs(string sessionKey)
        {
            if (!_appliedJobsBySession.TryGetValue(sessionKey, out var set)) return new List<Job>();
            return Jobs.Where(j => set.Contains(j.Id)).ToList();
        }
    }
}
