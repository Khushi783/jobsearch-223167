# JobFinder — Static HTML/CSS Version (+ minimal JS for login) & C# Reference Code

This is a **static** conversion of the JobFinder site: plain `.html` pages you can
open directly in any browser, one shared `.css` file, one small `.js` file that
makes login/register actually show your name, and the original C# model/data
classes kept as clean, compilable reference code.

## ⚠️ Important: what changed from the dynamic version

ASP.NET Core **Razor Pages requires the `.cshtml` extension** — that's how the
framework recognizes and compiles a page with embedded C#. Renaming those files
to `.html` means the server-side engine never touches them, so most of the
site is now plain, static HTML with no server-side logic behind it.

**One exception:** you asked for login/register to actually display your name
afterward. Static HTML alone cannot do that — there's no server to remember
what you typed. So `js/auth.js` was added: a small, plain JavaScript file
(no frameworks) that stores your name/email/role in the browser's
`localStorage` on submit, and fills it into the navbar, dashboard, and
profile pages. This is the **only** JavaScript in the project, added
specifically to solve that one problem — everything else stays static.

What still doesn't work without a real backend:
- Passwords aren't checked against anything real — any email/password combo
  "logs you in" and remembers the name you typed (or a name derived from the
  demo accounts / email prefix if you leave the name field blank on login).
- Job/company detail pages, search filters, and the dashboard's saved/applied
  jobs are still **static example data** — only your identity (name, email,
  role) is dynamic now.
- Refreshing normally keeps you logged in (thanks to `localStorage`), but
  clearing browser data / using a different browser resets it.
- Pages that were originally **dynamic per-item** (`JobDetails?id=X`,
  `Companies?id=X`) still exist as **one representative example page each**
  (`job-details.html` shows Job #1, `company-details.html` shows Company #1).
- The `.cs` files (`Job.cs`, `Company.cs`, `Category.cs`, `User.cs`,
  `DataService.cs`) are valid, compilable, standalone C# — but **they are not
  executed by the HTML**. They're included so the original data model and
  business logic (search, filtering, save/apply tracking, login validation)
  are available as reference code. To make the site fully live and secure
  again (real password checks, real per-user data), drop these back into an
  ASP.NET Core Razor Pages or MVC project and wire them up via dependency
  injection — the `js/auth.js` localStorage approach is a demo convenience,
  not real authentication.

## How to view it

No build step, no install required:

1. Open `html/index.html` directly in your browser (double-click it, or
   right-click → Open With → your browser).
2. Click **Login** or **Register**, enter a name (or one of the demo emails
   below) and any password, and submit — you'll land on the Dashboard with
   your name shown in the sidebar, greeting, and navbar.
3. Click around — all internal links between the static pages work normally.

Demo accounts (email is checked, password is not):
- `candidate@example.com` → shows as "Demo Candidate"
- `employer@example.com` → shows as "Demo Employer"
- Any other email/name → shows exactly what you typed

If you'd prefer to serve it over `http://` instead of `file://` (recommended
for consistent relative-path behavior), you can use any static file server,
for example from the project root:

```bash
cd html
python3 -m http.server 8000
```

Then visit `http://localhost:8000`.

## Folder structure

```
JobFinder-Static/
├── html/
│   ├── index.html
│   ├── about.html
│   ├── jobs.html                (all 25 jobs as cards)
│   ├── job-details.html         (example detail page — Job #1)
│   ├── companies.html           (all 10 companies as cards)
│   ├── company-details.html     (example detail page — Company #1)
│   ├── categories.html          (all 8 categories)
│   ├── login.html               (captures name via auth.js)
│   ├── register.html            (captures name via auth.js)
│   ├── dashboard.html           (shows your real name/role; requires login)
│   ├── contact.html
│   ├── profile.html             (shows/edits your real name; requires login)
│   ├── saved-jobs.html          (example saved jobs)
│   ├── applied-jobs.html        (example applied jobs)
│   └── 404.html
├── css/
│   └── style.css                (shared by every page)
├── js/
│   └── auth.js                  (the only JS in the project — see above)
└── cs/
    ├── Models/
    │   ├── Job.cs
    │   ├── Company.cs
    │   ├── Category.cs
    │   └── User.cs
    └── Services/
        └── DataService.cs
```

## Design

Same visual system as the dynamic version: blue-and-white professional theme,
sticky glassmorphism navbar, gradient hero, custom CSS Grid/Flexbox layout,
hover animations, rounded cards and buttons — all hand-written CSS, no
frameworks.

