document.addEventListener("DOMContentLoaded", function () {

    const buttons = document.querySelectorAll(".save-job");

    buttons.forEach(function (button) {

        button.addEventListener("click", function () {

            const job = {
                id: this.dataset.id,
                title: this.dataset.title,
                company: this.dataset.company,
                location: this.dataset.location
            };

            let savedJobs = JSON.parse(localStorage.getItem("savedJobs")) || [];

            const exists = savedJobs.some(j => j.id === job.id);

            if (exists) {
                alert("Job already saved.");
                return;
            }

            savedJobs.push(job);

            localStorage.setItem("savedJobs", JSON.stringify(savedJobs));

            this.textContent = "Saved ✓";
            this.disabled = true;

            alert("Job saved successfully!");

        });

    });

});